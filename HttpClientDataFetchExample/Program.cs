using Newtonsoft.Json.Linq;
static async Task Main()
    {
    HttpClient httpClient = new HttpClient();
    try
    {
            string url = "https://jsonplaceholder.typicode.com/posts";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            JArray posts = JArray.Parse(responseBody);

            var filteredPosts = posts.Where(post =>
                post["id"].ToObject<int>() == 1 ||
                post["id"].ToObject<int>() == 10 ||
                post["id"].ToObject<int>() == 100);

            using (StreamWriter file = new StreamWriter("test.txt"))
            {
                foreach (var post in filteredPosts)
                {
                    await file.WriteLineAsync(post.ToString());
                }
            }

            Console.WriteLine("Data has been written to test.txt");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

