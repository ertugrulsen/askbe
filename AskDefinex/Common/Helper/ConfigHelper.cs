﻿using DefineXwork.Library.Configuration;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace AskDefinex.Common.Helper
{
    public class ConfigHelper : IConfigManager
    {
        static readonly Dictionary<string, string> configurations = new Dictionary<string, string>();


        public static void LoadConfig(IConfiguration configuration)
        {

            configurations.Add("DBConnection", configuration.GetConnectionString("DBConnection"));

            //Veritabanından çekilecek
            configurations.Add("AppSettings:PasswordHashKey", configuration.GetValue<string>("AppSettings:PasswordHashKey"));

            configurations.Add("JwtOptions:Audience", configuration.GetValue<string>("JwtOptions:Audience"));
            configurations.Add("JwtOptions:Issuer", configuration.GetValue<string>("JwtOptions:Issuer"));
            configurations.Add("JwtOptions:TokenExpiration", configuration.GetValue<string>("JwtOptions:TokenExpiration"));
            configurations.Add("JwtOptions:SecurityKey", configuration.GetValue<string>("JwtOptions:SecurityKey"));
            configurations.Add("JwtOptions:RefreshTokenExpiration", configuration.GetValue<string>("JwtOptions:RefreshTokenExpiration"));
        }

        public string GetConfig(string configName)
        {
            if (configurations.ContainsKey(configName))
                return configurations[configName];
            return null;
        }
    }
}
