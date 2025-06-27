using System.Text.Json;
using ConsumerGitHubUser.Models;

namespace ConsumerGitHubUser.Client;

public static class Client
{
    private static readonly HttpClient client = new HttpClient();
    private const string BaseUrl = "https://api.github.com/user/";

    public static async Task ConsumirApi()
    {
        Console.WriteLine("--- GitHub User Consumer ---");
        Console.Write("Enter GitHub user ID: ");
        string? userId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userId))
        {
            Console.WriteLine("User ID cannot be empty. Exiting.");
            return;
        }

        try
        {
            client.DefaultRequestHeaders.Add("User-Agent", "ConsumerGitHubUserApp");
            string url = $"{BaseUrl}{userId}";
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            GitHubUser? user = JsonSerializer.Deserialize<GitHubUser>(responseBody);

            if (user != null)
            {
                Console.WriteLine("\n--- User Information ---");
                Console.WriteLine($"Name: {user.Name ?? "N/A"}");
                Console.WriteLine($"Company: {user.Company ?? "N/A"}");
                Console.WriteLine($"Location: {user.Location ?? "N/A"}");
                Console.WriteLine($"Login: {user.Login ?? "N/A"}");
            }
            else
            {
                Console.WriteLine("Could not deserialize user data.");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"\nRequest Error: {e.Message}");
            Console.WriteLine("Please check the user ID and your network connection.");
            Console.WriteLine("Note: The GitHub API has rate limiting. If you've made too many requests recently, you might need to wait.");
        }
        catch (JsonException e)
        {
            Console.WriteLine($"\nJSON Deserialization Error: {e.Message}");
            Console.WriteLine("The API response format might have changed or was unexpected.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nAn unexpected error occurred: {e.Message}");
        }

        Console.WriteLine("\nPress any key to exit.");
        Console.ReadKey();
    }
}
