using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client;

namespace StringDetector.API.Client
{
    public static class ClientHelper
    {
        // static helpers
         public static async Task<HttpResponseMessage>  HandleResponseMessageAsync<TResult>(Task<HttpApiResponseMessage<TResult>> responseTask)
        {

            using (var apiResponse = await responseTask)
            {

                if (apiResponse.IsSuccess)
                {

                    return apiResponse.Response;
                }

                throw GetHttpApiRequestException(apiResponse);
            }
        }

        public static  async Task HandleResponseAsync(Task<HttpApiResponseMessage> responseTask)
        {

            using (var apiResponse = await responseTask)
            {

                if (!apiResponse.IsSuccess)
                {

                    throw GetHttpApiRequestException(apiResponse);
                }
            }
        }

        

        public static async Task<TResult> HandleResponseAsync<TResult>(Task<HttpApiResponseMessage<TResult>> responseTask)
        {

            using (var apiResponse = await responseTask)
            {

                if (apiResponse.IsSuccess)
                {

                    return apiResponse.Model;
                }

                throw ClientHelper.GetHttpApiRequestException(apiResponse);
            }
        }

        public static HttpApiRequestException GetHttpApiRequestException(HttpApiResponseMessage apiResponse)
        {

            return new HttpApiRequestException(
                string.Format(ErrorMessages.HttpRequestErrorFormat, (int)apiResponse.Response.StatusCode, apiResponse.Response.ReasonPhrase),
                apiResponse.Response.StatusCode, apiResponse.HttpError);
        }

    }
}
