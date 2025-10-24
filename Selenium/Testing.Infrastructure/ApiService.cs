using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Testing.Infrastructure
{

    public class ApiResponse<T>
    {
        public string requestContent { get; set; }
        public HttpWebResponse httpWebResponse { get; set; }
        public string responseContent { get; set; }
        public T responseObject { get; set; }
        public dynamic responseException { get; set; }

        public ApiResponse(string requestContent, HttpWebResponse response, string responseContent)
        {
            this.requestContent = requestContent;
            this.httpWebResponse = response;
            this.responseContent = responseContent;

            if (response != null && (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created))
            {
                if (typeof(T) == typeof(String))
                {
                    responseObject = (T)(object)responseContent;
                }
                else
                {

                    dynamic wrapperT = JsonConvert.DeserializeObject<T>(responseContent,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    responseObject = (T)wrapperT;
                }
            }
        }
    }
    static public class ApiService
    {
        static public string UriBuilder(String baseUrl, List<string> routes, Dictionary<string, string> terms)
        {
            string url = baseUrl;

            if (routes != null && routes.Count > 0)
            {
                foreach (string route in routes)
                {
                    url = url + "/" + route;
                }
            }


            if (terms != null && terms.Count > 0)
            {
                url = url + "?";

                foreach (var term in terms.Where(c => c.Value != null))
                {
                    url = url + term.Key + "=" + term.Value + "&";
                }
            }


            return url;
        }

        static HttpWebRequest _webRequest;
        static HttpWebResponse _webResponse;
        static string _webResponseContent;



        static public void AddAuthentication(string token)
        {
            if (token != null && token.Length > 0)
            {
                _webRequest.PreAuthenticate = true;
                _webRequest.Headers.Add("Authorization", "Bearer " + token);
            }
        }

        static void SendWebRequest()
        {
            try
            {
                int retry = 10;
                do
                {
                    Thread.Sleep(1000 * (11 - retry));
                    retry--;
                    //var res = _webRequest.GetResponse();
                    _webResponse = (HttpWebResponse)_webRequest.GetResponse();

                } while (_webResponse == null && retry > 0);

                using (Stream responseData = _webResponse.GetResponseStream())
                using (var reader = new StreamReader(responseData))
                {
                    _webResponseContent = reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                _webResponse = (HttpWebResponse)e.Response;
                if (_webResponse != null)
                {
                    using (var reader = new StreamReader(_webResponse.GetResponseStream()))
                    {
                        _webResponseContent = reader.ReadToEnd();
                    }
                }

                //_webResponseContent = e.Message;
            }
        }

        static public ApiResponse<T> Get<T>(string url, string token)
        {
            _webRequest = WebRequest.CreateHttp(url);

            _webRequest.Method = "GET";

            AddAuthentication(token);

            SendWebRequest();

            return new ApiResponse<T>(url, _webResponse, _webResponseContent);
        }

        static public ApiResponse<T> Post<T>(string url, string body, string token)
        {
            _webRequest = WebRequest.CreateHttp(url);


            var data = Encoding.UTF8.GetBytes(body);

            _webRequest.Method = "POST";
            _webRequest.ContentType = "application/json";
            _webRequest.ContentLength = data.Length;

            AddAuthentication(token);

            using (var stream = _webRequest.GetRequestStream())
            {
                stream.Write(data, 0, body.Length);
            }

            SendWebRequest();

            return new ApiResponse<T>($"{url};{body}", _webResponse, _webResponseContent);
        }

        static public ApiResponse<T> Put<T>(string url, string body, string token)
        {
            _webRequest = WebRequest.CreateHttp(url);

            var data = Encoding.UTF8.GetBytes(body);

            _webRequest.Method = "PUT";
            _webRequest.ContentType = "application/json";
            _webRequest.ContentLength = data.Length;

            AddAuthentication(token);

            using (var stream = _webRequest.GetRequestStream())
            {
                stream.Write(data, 0, body.Length);
            }

            SendWebRequest();

            return new ApiResponse<T>($"{url};{body}", _webResponse, _webResponseContent);
        }
    }
}
