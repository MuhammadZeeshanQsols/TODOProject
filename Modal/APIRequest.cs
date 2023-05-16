using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TODOProject.EventArgs;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System;
using System.Web.ModelBinding;
using System.Net.Http.Headers;
using RestSharp;
using System.Text;

namespace TODOProject.Modal
{
    public class APIRequest
    {
        string taskAPIUrl = "https://localhost:44314/Task/GetAllTaskLsts";
         HttpClient client = new HttpClient();
        public async Task<List<TaskEventArgs>> GetAsync()
        {
            List<TaskEventArgs> lst_task = new List<TaskEventArgs>();
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                {
                    return true;
                }

                if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
                {
                    // If you're accessing the server by IP address, you may need to check the certificate's Subject Alternative Name (SAN) list for the IP address
                    return true; // Return true to ignore the name mismatch error
                }

                return false;
            };
                HttpResponseMessage response = await client.GetAsync("https://localhost:44314/Task/GetAllTaskLsts");
                if (response.IsSuccessStatusCode)
                {
                    lst_task = await response.Content.ReadAsAsync<List<TaskEventArgs>>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst_task;
        }
        public  string JsonSave(TaskEventArgs taskEventArgs)
        {

            HttpClient client = new HttpClient();
            var response = client.PostAsJsonAsync<TaskEventArgs>("api/Save", taskEventArgs);

            if (response.IsCompleted && response.Status==TaskStatus.RanToCompletion)
            {
               return  "";

                //use it
            }
            return "";
        }
            public  string Save(TaskEventArgs taskEventArgs)
        {
            HttpClient client = new HttpClient();
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    if (sslPolicyErrors == SslPolicyErrors.None)
                    {
                        return true;
                    }

                    if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
                    {
                        // If you're accessing the server by IP address, you may need to check the certificate's Subject Alternative Name (SAN) list for the IP address
                        return true; // Return true to ignore the name mismatch error
                    }

                    return false;
                };
                //StringBuilder stringBuilder = new StringBuilder();
                //stringBuilder.Append($"id={taskEventArgs.ID}");
                //stringBuilder.Append($"taskname='{taskEventArgs.TaskName}'");
                //stringBuilder.Append($"taskcolor='{taskEventArgs.TaskColor}'");
                //stringBuilder.Append($"taskorder={taskEventArgs.TaskOrder}");
                //stringBuilder.Append($"_query={taskEventArgs.query}");
                //stringBuilder.Append($"_isdelete={taskEventArgs.IsDeleted}");
                //stringBuilder.Append($"_ispending={taskEventArgs.Ispending}");
                client.BaseAddress = new Uri("https://localhost:44314/Task/Save");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<TaskEventArgs>("api/Save", taskEventArgs);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return "save";
                }
                else
                { return "error"; }


               
            }
            catch (Exception ex)
            {
                return "error: "+ex.Message;
            }
           
        }

      


   
   


        // ...// declare a static HttpClient instance
        private static readonly HttpClient httpclient = new HttpClient();

        public async Task<List<TaskEventArgs>> GetAllTaskAsync()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                {
                    return (sslPolicyErrors == SslPolicyErrors.None || sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch
                        ?true:false);
                };
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072 |
                (SecurityProtocolType)768;
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create("https://localhost:44314/api/GetAllTaskLsts");
                http.Method = "Get";
                http.ContentType = "application/json";
                http.KeepAlive = false;
               httpclient.BaseAddress = new Uri("https://localhost:44314/");
                httpclient.DefaultRequestHeaders.Accept.Clear();
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpclient.Timeout = TimeSpan.FromMinutes(5);
                HttpResponseMessage response = await httpclient.GetAsync("api/GetAllTaskLsts");
                if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<TaskEventArgs>>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
             return new List<TaskEventArgs>();
        }


        public List<TaskEventArgs> GetResult()
        {
            HttpClient client = new HttpClient();
            var responce = client.GetStringAsync("https://localhost:44314/api/GetAllTaskLsts");
          var Sunrise = JsonConvert.DeserializeObject<List<TaskEventArgs>>(responce.Result.ToString());
           
            return Sunrise;
        }
        public string StringGetResult()
        {
            HttpClient client = new HttpClient();
            var responce = client.GetStringAsync("https://localhost:44314/api/GetAllTaskLsts");
            string res = responce.Result.ToString();
            return res;
        }

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Implement your validation logic here
            // Return true if the certificate is valid, false otherwise
            return true;
        }


        public async Task<List<TaskEventArgs>> Get<TaskEventArgs>()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44314/");
                var result = await client.GetAsync("api/GetAllTaskLsts");
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                List<TaskEventArgs> resultContent = JsonConvert.DeserializeObject<List<TaskEventArgs>>(resultContentString);
                return resultContent;
            }
        }



        public List<TaskEventArgs> GetTask()
        {
            //List<TaskEventArgs> obj =  GetAllTaskAsync();
            //return obj;
          //  var d = StringGetResult();
           return GetResult();
           

    
        }
    }
}