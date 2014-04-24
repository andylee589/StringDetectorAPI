using StringDetector.API.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringDetector.API.Connector
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ApiClientContextExtensions
    {


        public static ITJobClient GetTJobClient(this ApiClientContext apiClientContext)
        {

            return apiClientContext.GetClient<ITJobClient>(() => new TJobClient(apiClientContext.HttpClient));
        }

        internal static TClient GetClient<TClient>(this ApiClientContext apiClientContext, Func<TClient> valueFactory)
        {

            return (TClient)apiClientContext.Clients.GetOrAdd(typeof(TClient), k => valueFactory());
        }



    }
}
