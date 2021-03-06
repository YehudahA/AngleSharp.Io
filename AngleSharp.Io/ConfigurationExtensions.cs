﻿namespace AngleSharp
{
    using AngleSharp.Io.Network;
    using AngleSharp.Io.Services;
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using AngleSharp.Services;
    using System.Linq;
    using System.Net.Http;

    /// <summary>
    /// Additional extensions for using requesters.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Adds a loader service that uses HttpClient Requester.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <returns>The new configuration.</returns>
        public static IConfiguration WithHttpClientRequesters(this IConfiguration configuration)
        {
            var httpClient = new HttpClient();
            return configuration.WithHttpClientRequesters(httpClient);
        }


        /// <summary>
        /// Adds a loader service that uses HttpClient Requester.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="httpClient">The HTTP client to use for requests.</param>
        /// <returns>The new configuration.</returns>
        public static IConfiguration WithHttpClientRequesters(this IConfiguration configuration, HttpClient httpClient)
        {
            if (!configuration.Services.OfType<ILoaderService>().Any())
            {
                var requesters = new IRequester[] { new HttpClientRequester(httpClient), new DataRequester() };
                var service = new LoaderService(requesters);
                return configuration.With(service);
            }

            return configuration;
        }
    }
}