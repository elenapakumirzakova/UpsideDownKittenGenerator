﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using UpsideDownKittenGenerator.Shared;

namespace UdsideDownKittenGenerator.Services
{
    public static class ServicesExtensions
    {
        public static void AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string uri = configuration[Constants.KittenEndpoint];
            services.RegisterHttpClient<IKittenService, KittenService>(uri);
        }

        private static void RegisterHttpClient<TInterface, TClient>(this IServiceCollection services, string baseUrl)
            where TClient : class, TInterface
            where TInterface : class
        {
            services.AddHttpClient<TInterface, TClient>(client => client.Configure(baseUrl))
                .ConfigurePrimaryHttpMessageHandler(handler => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip,
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                });
        }

        private static void Configure(this HttpClient client, string baseUrl)
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.Timeout = TimeSpan.FromSeconds(60);
        }
    }
}
