using System;
using System.Net.Http;
using IdentityModel.Client;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ThirdPartyDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            {
                //从原数据发现端点
                var client = new HttpClient();

                var disco = await client.GetDiscoveryDocumentAsync("http://localhost:6001");

                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    return;
                }


                //请求token
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "client",
                    ClientSecret = "secret",
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);
                Console.WriteLine("\n\n");

                //请求api
                var apiClient = new HttpClient();
                apiClient.SetBearerToken(tokenResponse.AccessToken);
                var response = await apiClient.GetAsync("http://localhost:52921/api/identity");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }
            }


            {
                //从原数据发现端点
                var client = new HttpClient();

                var disco = await client.GetDiscoveryDocumentAsync("http://localhost:6001");

                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    return;
                }


                //请求token
                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "pwdClient",
                    //ClientSecret = "secret",
                    UserName = "jesse",
                    Password = "123456"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);
                Console.WriteLine("\n\n");

                //请求api
                var apiClient = new HttpClient();
                apiClient.SetBearerToken(tokenResponse.AccessToken);
                var response = await apiClient.GetAsync("http://localhost:52921/api/identity");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(JArray.Parse(content));
                }
            }





        }
    }
}
