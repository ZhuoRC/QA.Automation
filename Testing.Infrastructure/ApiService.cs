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

        static void AddAuthentication()
        {
            _webRequest.PreAuthenticate = true;
            _webRequest.Headers.Add("Authorization", "Bearer " + "eyJhbGciOiJSUzI1NiIsImtpZCI6IkpfYnNaNXBUY0xpT0dVTHNad04zV0EiLCJ0eXAiOiJhdCtqd3QifQ.eyJuYmYiOjE1OTA2Mzg3NDUsImV4cCI6MTU5MzIzMDc0NSwiaXNzIjoiaHR0cHM6Ly9zdGFnaW5nLXBheWF3YXJlLWlkZW50aXR5c2VydmVyLmxhdmFzb2Z0Lm5ldCIsImF1ZCI6InBheWF3YXJlLmFwaSIsImNsaWVudF9pZCI6InBheWF3YXJlX2FwaV9hdXRvX3Rlc3Rfc3RhZ2luZyIsInN1YiI6IjA0ODVkMDQzLTkzN2UtNGM3My05OThmLTRmZDIxNjViZDNiZSIsImF1dGhfdGltZSI6MTU5MDYzODc0MSwiaWRwIjoibG9jYWwiLCJyb2xlIjoiUFNUU0FkbWluaXN0cmF0b3IiLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJ6aHVvcnVpLmNoZW5AYXZhbnF1ZXN0LmNvbSIsInNjb3BlIjpbInBheWF3YXJlLmFwaS5hY2NvdW50LnJlYWQiLCJwYXlhd2FyZS5hcGkuY2FyZC5yZWFkIiwicGF5YXdhcmUuYXBpLmZ1bmRpbmcucmVhZCIsInBheWF3YXJlLmFwaS5hY2NvdW50LndyaXRlIiwicGF5YXdhcmUuYXBpLmNhcmQud3JpdGUiLCJwYXlhd2FyZS5hcGkuZnVuZGluZy53cml0ZSJdLCJhbXIiOlsicHdkIl19.GXtcYtqEgpHxNijQvVCsq7wTrIhgmLFaFJE3QtF4JR4TuCA1BSI2ffWigMj_0XGQW93TupcDIWairTh8Qq6ea6T92B6sYZPa9EPxKvHjPWaTpJG8iVfv-tdnXWu5srGDaKvb7XKl08gxClYm8gPdo1_WMMOQFLLCwQgYnAbS7fV3HpiqeT7lQavDHOsj467YocnUfRdKfx9PM9FNKafuKdfCpUUEDY1nF6DU5eyQu9GkvBS1U6wffQx4sr5XR42L2l7x7Wd9wfDsWOxNMAXQOXyocs6EIgeoE4ZDoyW98yaOWbuc3vKxjwZYJtRHVfTxvI_GERy5oBA51LEEpovWrw");

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
