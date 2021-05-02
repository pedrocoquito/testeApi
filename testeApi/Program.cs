using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace testeApi
{
    class Program
    {
        public static void Main(string[] args)
        {
            string filePath = "Data.txt";

            APIDCredentials ac = new APIDCredentials("EMAIL", "SENHA"); //Adicionar email e senha cadastrados

            Task<AcessToken> tokenResult = GetTokenAsync(ac);
            AcessToken acessToken = tokenResult.Result;

            string term = "\"Dimas Soares Lima\""; //O termo de busca deve ser enviado neste formato para que seja realizada a busca exata e não contendo LIKE na query

            Task<Person> personResult = GetPersonAsync(acessToken, term);
            Person p = personResult.Result;

            if(!p.HasResume)
            {
                Console.WriteLine("Pessoa buscada não possui Lattes");
            }
            else
            {
                Task<string> lattesResult = GetLattesAsync(acessToken, p.ResumeLink);
                string lattes = lattesResult.Result.ToString();
                p.Resume = lattes;
                WriteCSV(p, filePath);
            }
        }
        //Função responsável pela requisição que gera o token de autenticação para API
        private static async Task<AcessToken> GetTokenAsync(APIDCredentials ac)
        {
            AcessToken acessToken = new AcessToken("");
            using (var client = new HttpClient())
            {
                APIDCredentials content = ac;
                var result = await client.PostAsJsonAsync("https://api.escavador.com/api/v1/request-token", content);

                string resultContent = await result.Content.ReadAsStringAsync();
                acessToken = JsonConvert.DeserializeObject<AcessToken>(resultContent);

                return acessToken;
            }
        }
        //Função que realiza a busca pelo nome informado e retorna algumas informações a respeito da mesma
        private static async Task<Person> GetPersonAsync(AcessToken token, string term)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                var search = new { q = term, qo = "p" };

                var url = $"https://api.escavador.com/api/v1/busca?q={search.q}&qo={search.qo}";

                var result = await client.GetAsync(url);

                string resultContent = await result.Content.ReadAsStringAsync();
                APIData listOfPersons = JsonConvert.DeserializeObject<APIData>(resultContent);
                string name = listOfPersons.Items[0].Name;
                return listOfPersons.Items[0];
            }
        }
        //Função que realiza a requisição e retorna o Lattes da pessoa 
        private static async Task<string> GetLattesAsync(AcessToken token, string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                var result = await client.GetAsync(url);

                string resultContent = await result.Content.ReadAsStringAsync();

                return resultContent;
            }
        }
        //Método que escreve o arquivo CSV (ESTE ESTARÁ NA PASTA BIN DO PROJETO)
        public static void WriteCSV(Person p, string filePath)
        {
            using(System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
            {
                string id = Convert.ToString(p.Id);
                file.WriteLine( id+ ',' + p.Name + ',' + p.ResumeLink + ',' + p.Resume);
                Console.WriteLine("Terminou");
            }
        }
    }
}
