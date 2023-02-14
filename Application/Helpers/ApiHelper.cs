using Application.Helpers.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class ApiHelper : IApiHelper
    {
        public enum HttpMethods
        {
            Get, Post, Put, Patch, Delete, Option
        }
        public async Task<ApiReturnResult> Call(HttpMethods method, string actionUrl, StringContent content = null)
        {
            var result = new ApiReturnResult();
            switch (method)
            {
                case HttpMethods.Get:
                    using (var client = new HttpClient())
                    {
                        var response = client.GetAsync(actionUrl);
                        result.ResultCode = response.Result.StatusCode;
                        result.Content = await response.Result.Content.ReadAsStringAsync();
                    }
                    break;
                case HttpMethods.Post:
                    using (var client = new HttpClient())
                    {
                        var response = client.PostAsync(actionUrl, content);
                        result.ResultCode = response.Result.StatusCode;
                        result.Content = await response.Result.Content.ReadAsStringAsync();
                    }
                    break;
                case HttpMethods.Put:
                    using (var client = new HttpClient())
                    {
                        var response = client.PutAsync(actionUrl, content);
                        result.ResultCode = response.Result.StatusCode;
                        result.Content = await response.Result.Content.ReadAsStringAsync();
                    }
                    break;
                case HttpMethods.Patch:
                    using (var client = new HttpClient())
                    {
                        var response = client.PatchAsync(actionUrl, content);
                        result.ResultCode = response.Result.StatusCode;
                        result.Content = await response.Result.Content.ReadAsStringAsync();
                    }
                    break;
                case HttpMethods.Delete:
                    using (var client = new HttpClient())
                    {
                        var response = client.DeleteAsync(actionUrl);
                        result.ResultCode = response.Result.StatusCode;
                        result.Content = await response.Result.Content.ReadAsStringAsync();
                    }
                    break;
                case HttpMethods.Option:
                    using (var client = new HttpClient())
                    {
                        var response = client.GetAsync(actionUrl);
                        result.ResultCode = response.Result.StatusCode;
                        result.Content = await response.Result.Content.ReadAsStringAsync();
                    }
                    break;
            }
            return result;
        }

        public StringContent Serialize(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
        public T Deserialize<T>(string obj) 
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
    public class ApiReturnResult
    {
        public string Content { get; set; } = null!;
        public HttpStatusCode ResultCode { get; set; }
        public bool IsSuccessfulResult()
        {
            if ((int)ResultCode >= 200 && (int)ResultCode < 300)
                return true;
            else
                return false;
        }
    }
}
