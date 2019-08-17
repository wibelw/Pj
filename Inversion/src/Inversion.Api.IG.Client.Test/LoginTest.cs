using dto.endpoint.auth.session.v2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using Inversion.Api.IG.Client;
using Microsoft.Extensions.Configuration;
using System;

namespace Inversion.Api.IG.Client.Test
{
    [TestClass]
    public class LoginTest
    {

        // Realiza una llamada al método de login y obtiene la información de las cuentas existentes
        [TestMethod]
        public   void Login()

        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("IGSettings.json").Build();
                string env = config["environment"];
                string userName = config["username"];
                string password = config["password"];
                string apiKey = config["apikey"];
                IgRestApiClient igRestApiClient=new IgRestApiClient(env);
                var ar = new AuthenticationRequest { identifier = userName, password = password,encryptedPassword=false };
                AuthenticationResponse response  =  igRestApiClient.SecureAuthenticate(ar, apiKey).Result;
                if ((response != null) && (response.accounts.Count > 0))
                {                  
                    foreach (var account in response.accounts)
                     {                     
                        //Accounts.Add(igAccount);
                    }
                }                
            }
            catch(Exception ex)
            {
                string a= ex.Message;
            }
        
        }
    }
}