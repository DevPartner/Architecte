// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

// discover endpoints from metadata
var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5002");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}


// call api
var getClient = new HttpClient();

var response = await getClient.GetAsync("https://localhost:5001/api/Category?PageNumber=1&PageSize=10");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
    Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
}

await CreateCategoryAsync(disco.TokenEndpoint!);

static async Task CreateCategoryAsync(string endpoint)
{
    try
    {
        using (var client = new HttpClient())
        {
            // request token
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = endpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",
                UserName = "manager",
                Password = "password",
                Scope = "API"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.WriteLine(tokenResponse.ErrorDescription);
                return;
            }

            Console.WriteLine(tokenResponse.AccessToken);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            var content = new StringContent("{\"name\": \"testauth\",\"image\": \"string\",\"parentCategoryId\": null}", Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:5001/api/Category", content);
            response.EnsureSuccessStatusCode();

            Console.WriteLine("CreateCategoryAsync response: " + await response.Content.ReadAsStringAsync());
        }
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("HttpRequestException: " + e.Message);
        if (e.InnerException != null)
        {
            Console.WriteLine("Inner Exception: " + e.InnerException.Message);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception: " + ex.Message);
        if (ex.InnerException != null)
        {
            Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
        }
    }
}