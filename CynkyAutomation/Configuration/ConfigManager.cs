using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CynkyAutomation.Configuration
{
    public class ConfigManager
    {
        static IConfiguration _Configuration { get; set; }
        public static Dictionary<string, string> ServicesXBasicAuth;
        public static Dictionary<string, string> AzureCosmosDBURL;
        public static Dictionary<string, string> AzureCosmosDBSecretKey;

        static ConfigManager()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<ConfigManager>(true, reloadOnChange: true);
            _Configuration = builder.Build();
            ServicesXBasicAuth = new Dictionary<string, string>()
            {
              {"Application", _Configuration["ServicesXBasicAuth:Application"]}
            };
            AzureCosmosDBURL = new Dictionary<string, string>()
            {
              {"Notifications", _Configuration["AzureCosmosDB:Notifications:URL"]}
            };
            AzureCosmosDBSecretKey = new Dictionary<string, string>()
            {
              {"Notifications", _Configuration["AzureCosmosDB:Notifications:SecretKey"]}
            };
        }

        public static string ZephyrServiceUrl => _Configuration["ZephyrServiceUrl"];
        public static string ZephyrProjectKey => _Configuration["ZephyrProjectKey"];
        public static string PublishToZephyr => _Configuration["PublishToZephyr"];
        public static string RS_User => _Configuration["RS_User"];
        public static string RS_Key => _Configuration["RS_Key"];
        public static string EmailUsername => _Configuration["EmailUsername"];
        public static string EmailPassword => _Configuration["EmailPassword"];
        public static string ClientId => _Configuration["ClientId"];
        public static string TenantId => _Configuration["TenantId"];
        public static string ClientSecretId => _Configuration["ClientSecretId"];
        public static string ClientSecretIdValue => _Configuration["ClientSecretIdValue"];
        public static string ZephyrBearerToken => _Configuration["ZephyrBearerToken"];

    }
}