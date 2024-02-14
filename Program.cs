using UserSync.Services;
using UserSync.Storage;

Console.WriteLine("Enter the folder path:");
var path = Console.ReadLine();

Console.WriteLine("Select the file format (json/csv):");
var format = Console.ReadLine();

var apis = new List<(string url, int sourceId)>
        {
            ("https://randomuser.me/api/?results=500", 1),
            ("https://jsonplaceholder.typicode.com/users", 2),
            ("https://reqres.in/api/users", 3),
            ("https://dummyjson.com/users", 4),
        };

var apiService = new ApiService();
var tasks = apis.Select(api => apiService.FetchUsersAsync(api.url, api.sourceId));
var userResults = await Task.WhenAll(tasks);

var users = userResults.SelectMany(x => x).ToList();

IStorageService storageService = format == "json" ? new JsonStorageService() : new CsvStorageService() as IStorageService;
await storageService.SaveAsync(users, $"{path}/users.{format}");

Console.WriteLine($"Saved {users.Count} users to {path}/users.{format}");