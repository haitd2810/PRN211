class Program
{
    static readonly HttpClient client = new HttpClient();
    static async Task Main()
    {
        string uri = "http://www.contoso.com/";
        try
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}