﻿using CommonTypes.Configuration;

using DataAccess.Executors;
using DataAccess.Helpers;
using DataAccess.Repositories;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SyncGateway.Processors;

namespace SyncGateway.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddTransient<ISqlExecutor, PgSqlExecutor>();

            services.AddTransient<ISqlTransactionScope, SqlTransactionScope>();
            services.AddTransient<ITransactionFactory, SqlTransactionFactory>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFilePartRepository, FilePartRepository>();

            return services;
        }

        public static IServiceCollection AddConfigurationTypes(
            this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConnectionSettings = new DatabaseConnectionSettings();
            
            configuration.Bind("DatabaseConnection", databaseConnectionSettings);
            services.AddSingleton(databaseConnectionSettings);

            return services;
        }

        public static IServiceCollection AddProcessors(this IServiceCollection services)
        {
            services.AddTransient<IFileProcessor, FileProcessor>();

            return services;
        }
    }
}