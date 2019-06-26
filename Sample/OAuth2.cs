﻿using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Sample
{
    public static class OAuth2
    {
        public static async Task<string> GetAccessToken()
        {

            var headers = new NameValueCollection();

            headers.Add("cache-control", "no-cache");

            var httpClientParameters = new HttpClientParameters()
            {
                EndPoint = string.Format(@"https://" + Portal.Domain + Portal.OAuth2URL),
                Body = Util.CreateHttpRequestDataJSON(Util.BuildRequestParameters()),
                Method = WebRequestMethods.Http.Post,
                EncodingType = Encoding.UTF8,
                Headers = headers
            };

            HttpClientHelper httpClientHelper = new HttpClientHelper(httpClientParameters);
            
            
            await httpClientHelper.CallService();
            Console.WriteLine(httpClientHelper.Result);

            var token = ((JObject)JsonConvert.DeserializeObject<dynamic>(httpClientHelper.Result))["access_token"].ToString();
            httpClientHelper = null;
            return token;
        }

    }
}
