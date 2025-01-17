﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Simple_REST_API.Business.DbOptions;
using Simple_REST_API.Business.Services;

namespace Simple_REST_API.Business.Installers
{
    public static class CustomServicesInstaller
    {
        
        
        public static void InstallCustomServices(this IServiceCollection services,ConfigurationManager configuration)
        {
            InstallMvcServices(services);
            
            services.AddScoped<INoteService, NoteService>();

            configuration.AddJsonFile("credentials.json", true, true);

            var mongoDBSettings = new MongoDbOptions();

            configuration.GetSection("databases:MongoDB").Bind(mongoDBSettings);

            services.AddSingleton<IMongoDatabase>(m => new MongoClient(mongoDBSettings.API_KEY).GetDatabase(mongoDBSettings.dbName));
            
        }

        private static void InstallMvcServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddControllers();
        }
    }
}
