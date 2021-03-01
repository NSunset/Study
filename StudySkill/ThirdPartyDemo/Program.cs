using System;
using System.Net.Http;
using IdentityModel.Client;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace ThirdPartyDemo
{
    public class CustomTest
    {
        public object GetService(System.Type type)
        {
            return type;
        }
    }

    public interface IDbContextOptions
    {
       
    }

    public class MySqlDbContextOptions : IDbContextOptions
    {
       
    }


    class Program
    {
        static async Task Main(string[] args)
        {

            {
                var url = "https://localhost:5001/weatherforecast";
                var client = new HttpClient();

                for (int i = 0; i < 300; i++)
                {
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"服务器错误:{content};Error:{response.StatusCode}");
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(content);
                    }
                }
                
            }


            {
                var constructor = typeof(CustomTest).GetConstructor(System.Type.EmptyTypes);

                var method = typeof(CustomTest).GetMethod("GetService");


                var customTest = Expression.Parameter(typeof(CustomTest));

                var type = Expression.Parameter(typeof(Type));
                //var call = Expression.Call(Expression.New(constructor), typeof(CustomTest).GetMethod("GetService"), type);

                var call = Expression.Call(Expression.New(constructor), typeof(CustomTest).GetMethod("GetService"), Expression.Convert(Expression.Constant(typeof(MySqlDbContextOptions)), typeof(Type))); 
                UnaryExpression testExpression = Expression.Convert(Expression.New(constructor), typeof(CustomTest));

                var lambda = Expression.Lambda<Func<object>>(call).Compile();

                
                var aa = lambda();

                
            }

            {
                //lambda表达式学习
                //左侧没有输入参数,没有输出
                Action lambda1 = () =>
                {
                    int a = 3;
                    a = 100;
                };

                //左侧没有输入参数，有输出参数
                Func<int> lambda2 = () => 100;

                //左侧有输入参数，没有输出参数
                Action<int[]> lambda3 = x => x[0] += 2;



                //左侧有输入参数，有输出参数
                Func<int, string> lambda4 = x => x.ToString();



                string[] companies = { "Consolidated Messenger", "Alpine Ski House", "Southridge Video", "City Power & Light",
                   "Coho Winery", "Wide World Importers", "Graphic Design Institute", "Adventure Works",
                   "Humongous Insurance", "Woodgrove Bank", "Margie's Travel", "Northwind Traders",
                   "Blue Yonder Airlines", "Trey Research", "The Phone Company",
                   "Wingtip Toys", "Lucerne Publishing", "Fourth Coffee" };

                //构建
                //companies.Where(a => a.ToLower() == "coho winery" || a.Length > 16).OrderBy(a => a);
                IQueryable<string> queryableData = companies.AsQueryable();

                //构建参数
                ParameterExpression pe = Expression.Parameter(typeof(string), "parameter");


                //构建方法 a.ToLower()
                MethodCallExpression left = Expression.Call(pe, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));

                //构建常量 "coho winery"
                ConstantExpression right = Expression.Constant("coho winery");

                //构建运算符 a.ToLower() == "coho winery"
                BinaryExpression binaryExpression = Expression.Equal(left, right);

                //构建属性 a.Length
                MemberExpression left1 = Expression.Property(pe, typeof(string).GetProperty("Length"));

                //构建int常量 16
                ConstantExpression right1 = Expression.Constant(16, typeof(int));

                //构建 a.Length > 16
                BinaryExpression binaryExpression1 = Expression.GreaterThan(left1, right1);

                //构建 a.ToLower() == "coho winery" || a.Length > 16
                BinaryExpression binaryExpression2 = Expression.OrElse(binaryExpression, binaryExpression1);

                //构建 Queryable.Where(a => a.ToLower() == "coho winery" || a.Length > 16)
                MethodCallExpression whereCallExpression = Expression.Call(
                    typeof(Queryable)
                    , "Where"
                    , new Type[] { queryableData.ElementType }
                    , queryableData.Expression
                    , Expression.Lambda<Func<string, bool>>(binaryExpression2, new ParameterExpression[] { pe })
                    );

                //构建 Queryable.Where(a => a.ToLower() == "coho winery" || a.Length > 16).OrderBy(a => a)
                MethodCallExpression orderByCallExpression = Expression.Call(
                    typeof(Queryable)
                    , "OrderBy"
                    , new Type[] { queryableData.ElementType, queryableData.ElementType }
                    , whereCallExpression
                    , Expression.Lambda<Func<string, string>>(pe, new ParameterExpression[] { pe })
                    );
                IQueryable<string> results = queryableData.Provider.CreateQuery<string>(orderByCallExpression);

                List<string> test = results.ToList();



                ParameterExpression numExpression = Expression.Parameter(typeof(int), "num");

                ConstantExpression five = Expression.Constant(5, typeof(int));

                BinaryExpression numLessThanFiveExpression = Expression.LessThan(numExpression, five);

                Expression<Func<int, bool>> expression = Expression.Lambda<Func<int, bool>>(numLessThanFiveExpression, new ParameterExpression[] { numExpression });


                var flage = expression.Compile()(4);

                Expression.Label(typeof(int));

                Expression.Block();
                Expression.Assign(Expression.Parameter(typeof(int)), Expression.Constant(1, typeof(int)));


                ParameterExpression value = Expression.Parameter(typeof(int), "value");
                ParameterExpression result = Expression.Parameter(typeof(int), "result");
                LabelTarget label = Expression.Label(typeof(int));
                Expression.Block(
                    new[] { result }
                    , Expression.Assign(result, Expression.Constant(1, typeof(int)))
                    , Expression.Loop(
                        Expression.IfThenElse(
                            Expression.GreaterThan(value, Expression.Constant(10, typeof(int)))
                            , Expression.MultiplyAssign(result, Expression.PostDecrementAssign(value))
                            , Expression.Break(label, result)

                        ),
                        label
                      )
                );




            }



            {
                //管道中间件学习
                Func<RequestDelegate, RequestDelegate> one = (RequestDelegate next) =>
                {
                    RequestDelegate @delegate = context =>
                    {
                        Console.WriteLine("第一步");
                        //context...
                        return next(context);
                    };
                    return @delegate;
                };

                Func<RequestDelegate, RequestDelegate> two = (RequestDelegate next) =>
                {
                    RequestDelegate @delegate = context =>
                    {
                        Console.WriteLine("第二步");
                        //context...
                        return next(context);
                    };
                    return @delegate;
                };

                RequestDelegate endapp = context =>
                {
                    Console.WriteLine("最后一步");
                    return Task.CompletedTask;
                };

                endapp = two(endapp);
                endapp = one(endapp);

                await endapp(new CustomHttpContext());

                var application = new CustomApplicationBuilder();
                application.Use((RequestDelegate next) =>
                {
                    RequestDelegate @delegate = context =>
                    {
                        Console.WriteLine("第一个中间件");
                        return next(context);
                    };
                    return @delegate;

                });
                application.Use((RequestDelegate next) =>
                {
                    RequestDelegate @delegate = context =>
                    {
                        Console.WriteLine("第二个中间件");
                        return next(context);
                    };
                    return @delegate;

                });
                var build = application.Build();
                await build(new CustomHttpContext());



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

    public class Student
    {
        public int ID { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        public List<int> Scores { get; set; }
    }
}
