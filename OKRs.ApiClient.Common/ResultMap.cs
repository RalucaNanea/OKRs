using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OKRs.ApiClient.Common
{
    public class ResultMap<T>
    {
        public ResultMap(HttpStatusCode status, T response)
        {
            this.StatusCode = status;
            this.Response = response;
        }

        /// <summary>
        /// Status code the result map applies to.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// The response object to return in the event of success
        /// </summary>
        public T Response { get; private set; }
    }
}
