using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.API.Client
{
    public sealed class ApiClientContext
    {

        private ApiClientContext() { }

        private static readonly Lazy<ConcurrentDictionary<Type, object>> _clients =
            new Lazy<ConcurrentDictionary<Type, object>>(() =>
                new ConcurrentDictionary<Type, object>(), isThreadSafe: true);

        private static readonly Lazy<HttpClient> _httpClient =
                    new Lazy<HttpClient>(
                        () =>
                        {

                            Assembly assembly = Assembly.GetExecutingAssembly();
                            HttpClient httpClient = HttpClientFactory.Create(innerHandler: new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip });

                            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                            httpClient.DefaultRequestHeaders.Add("X-UserAgent",
                                string.Concat(assembly.FullName, "( ", FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion, ")"));

                            return httpClient;

                        }, isThreadSafe: true);

        public ConcurrentDictionary<Type, object> Clients
        {

            get { return _clients.Value; }
        }

        internal Uri BaseUri { get; set; }
       // internal string AuthorizationValue { get; set; }
       // internal string AffiliateKey { get; set; }

        public HttpClient HttpClient
        {

            get
            {

                if (!_httpClient.IsValueCreated)
                {

                    InitializeHttpClient();
                }

                return _httpClient.Value;
            }
        }

        public static ApiClientContext Create(Action<ApiClientConfigurationExpression> action)
        {

            var apiClientContext = new ApiClientContext();
            var configurationExpression = new ApiClientConfigurationExpression(apiClientContext);

            action(configurationExpression);

            return apiClientContext;
        }

        public static ApiClientContext Create(string baseUrl)
        {
            var apiClientContext = new ApiClientContext();
            apiClientContext.BaseUri = new Uri(baseUrl);
            return apiClientContext;
        }

        private void InitializeHttpClient()
        {

            if (BaseUri == null)
            {
                throw new ArgumentNullException("BaseUri");
            }

            //if (string.IsNullOrEmpty(AuthorizationValue))
            //{
            //    throw new ArgumentNullException("AuthorizationValue");
            //}

            // Set BaseUri
            _httpClient.Value.BaseAddress = BaseUri;
            

            // Set default headers
            //_httpClient.Value.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AuthorizationValue);
        }
    }
}
