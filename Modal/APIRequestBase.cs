namespace TODOProject.Modal
{
    public class APIRequestBase
    {

        //public  async Task<List<TaskEventArgs>> GetAllTaskAsync()
        //{
        //    List<TaskEventArgs> lst_task = new List<TaskEventArgs>();
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44314/");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        // New code:
        //        HttpResponseMessage response = await client.GetAsync("api/task/1");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string result = response.Content.ReadAsStringAsync().Result;
        //            lst_task = JsonConvert.DeserializeObject<List<TaskEventArgs>>(result);
        //        }
        //    }



        //    return lst_task;
        //}
        public async Task<ActionResult> GetAllTaskAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44314/Task/");

            HttpResponseMessage response = await client.GetAsync("GetAllTaskLsts");
            string result = await response.Content.ReadAsStringAsync();

            List<Task> tasks = JsonConvert.DeserializeObject<List<Task>>(result);

            return tasks;
        }
    }
}