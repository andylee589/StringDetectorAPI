using StringDetector.API.Client.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.API.Client
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ApiClientContextExtensions
    {

        public static IJobClient GetJobClient(this ApiClientContext apiClientContext)
        {

            return apiClientContext.GetClient<IJobClient>(() => new JobClient(apiClientContext.HttpClient));
        }

        public static IJobConfigurationClient GetJobConfigurationClient(this ApiClientContext apiClientContext)
        {

            return apiClientContext.GetClient<IJobConfigurationClient>(() => new JobConfigurationClient(apiClientContext.HttpClient));
        }

        public static IJobReportClient GetJobReportClient(this ApiClientContext apiClientContext)
        {

            return apiClientContext.GetClient<IJobReportClient>(() => new JobReportClient(apiClientContext.HttpClient));
        }

        public static IJobStateClient GetJobStateClient(this ApiClientContext apiClientContext)
        {

            return apiClientContext.GetClient<IJobStateClient>(() => new JobStateClient(apiClientContext.HttpClient));
        }

        internal static TClient GetClient<TClient>(this ApiClientContext apiClientContext, Func<TClient> valueFactory)
        {

            return (TClient)apiClientContext.Clients.GetOrAdd(typeof(TClient), k => valueFactory());
        }


       
    }


   
}
