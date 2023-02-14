using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Helpers.ApiHelper;

namespace Application.Helpers.Contract
{
    public interface IApiHelper
    {
        StringContent Serialize(Object obj);
        T Deserialize<T>(string obj);
        Task<ApiReturnResult> Call(HttpMethods method, string actionUrl, StringContent content = null);
    }
}
