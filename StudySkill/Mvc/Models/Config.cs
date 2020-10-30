using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Mvc.Models
{
    public class Config
    {
        public static IEnumerable<Client> GetClients()
        {

            return new List<Client>
            {
                new Client
                {
                    ClientId="mvc",
                    ClientName="Mvc Client",
                    ClientUri="http://localhost:5000",
                    LogoUri="https://cdn2.codingdojo.com/new_design_image/global/icon/stacks/big/net_core_stack.png",
                    AllowRememberConsent=true,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent=true,
                    AllowedGrantTypes=GrantTypes.Implicit,
                    RedirectUris={"http://localhost:5000/signin-oidc"},
                    PostLogoutRedirectUris={ "http://localhost:5000/signout-callback-oidc" },
                    //RequireClientSecret=true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api.read"
                    },
                    AlwaysIncludeUserClaimsInIdToken=true,
                    AllowOfflineAccess=true, //启动对刷新令牌的支持
                }
            };
        }

        

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("api.read","My API Read"),
                new ApiScope("api.manage","My API Manage"),
                new ApiScope("api1.manage","My API Manage")
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api","API All")
                {
                    Scopes={ "api.read", "api.manage" }
                },
                new ApiResource("api1","API1 Manage")
                {
                    Scopes={ "api1.manage" }
                }
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    Username="admin",
                    Password="123456",
                    SubjectId="1000",
                    
                }
            };
        }
    }
}
