using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Testing.Infrastructure
{

    public class ApiResponse<T>
    {
        public string requestContent { get; set; }
        public HttpWebResponse httpWebResponse { get; set; }
        public string responseContent { get; set; }
        public T responseObject { get; set; }

        public ApiResponse (string requestContent, HttpWebResponse response, string responseContent)
        {
            this.requestContent = requestContent;
            this.httpWebResponse = response;
            this.responseContent = responseContent;

            if (response!=null && (response.StatusCode == HttpStatusCode.OK || response.StatusCode==HttpStatusCode.Created))
            {
                this.responseObject = JsonConvert.DeserializeObject<T>(responseContent);
            }
            
        }
    }
    static public class ApiService
    {
        static public string UriBuilder (String baseUrl, List<string> routes, Dictionary<string,string> terms)
        {
            string url = baseUrl;

            if (routes.Count > 0)
            {
                foreach (string route in routes)
                {
                    url = url + "/" + route;
                }
            }


            if (terms.Count > 0)
            {
                url = url + "?";

                foreach (var term in terms.Where(c=>c.Value!=null))
                {
                    url = url + term.Key + "=" + term.Value + "&";
                }
            }


            return url;
        }

        static HttpWebRequest _webRequest;
        static HttpWebResponse _webResponse;
        static string _webResponseContent;


        static public string _authToken { get; set; }

        static void AddAuthentication()
        {

            if (_authToken.Length > 0)
            {
                _webRequest.PreAuthenticate = true;
                _webRequest.Headers.Add("Authorization", "Bearer " + _authToken);
            }

        }

        static void SendWebRequest()
        {
            try
            {
                int retry = 3;
                do
                {
                    Thread.Sleep(2000);
                    retry--;
                    _webResponse = (HttpWebResponse)_webRequest.GetResponse();

                } while (_webResponse == null && retry >0);
                
                using (Stream responseData = _webResponse.GetResponseStream())
                using (var reader = new StreamReader(responseData))
                {
                    _webResponseContent = reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                _webResponse = (HttpWebResponse)e.Response;
                _webResponseContent = e.Message;
            }
        }


        static public ApiResponse<T> Get<T>(string url)
        {
            _webRequest = WebRequest.CreateHttp(url);

            _webRequest.Method = "GET";

            AddAuthentication();

            SendWebRequest();

            return new ApiResponse<T>(url, _webResponse, _webResponseContent);
        }

        static public ApiResponse<T> Post<T>(string url,string json)
        {
            _webRequest = WebRequest.CreateHttp(url);


            var data = Encoding.UTF8.GetBytes(json);

            _webRequest.Method = "POST";
            _webRequest.ContentType = "application/json";
            _webRequest.ContentLength = data.Length;


            AddAuthentication();

            using (var stream = _webRequest.GetRequestStream())
            {
                stream.Write(data, 0, json.Length);
            }

            SendWebRequest();

            return new ApiResponse<T>($"{url};{json}", _webResponse, _webResponseContent);
        }

        static public ApiResponse<T>  Put<T>(string url, string json)
        {
            _webRequest = WebRequest.CreateHttp(url);
   
            var data = Encoding.UTF8.GetBytes(json);

            _webRequest.Method = "PUT";
            _webRequest.ContentType = "application/json";
            _webRequest.ContentLength = data.Length;


            AddAuthentication();

            using (var stream = _webRequest.GetRequestStream())
            {
                stream.Write(data, 0, json.Length);
            }

            SendWebRequest();

            return new ApiResponse<T>($"{url};{json}", _webResponse, _webResponseContent);
        }
    }
}
