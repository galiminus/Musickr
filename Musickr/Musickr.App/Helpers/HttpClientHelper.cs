using Microsoft.AspNetCore.WebUtilities;

namespace Musickr.App.Helpers;

public static class HttpClientHelper
{
    public static async Task<string> GetWithParametersAsync(
        string uri, 
        List<KeyValuePair<string, string>> parameters,
        HttpClient httpClient
    )
    {
        var urlWithParams = QueryHelpers.AddQueryString(uri, parameters);
        
        var result = 
            await httpClient.GetAsync(urlWithParams);
        
        return await result.Content.ReadAsStringAsync();
    }
}