using System.Net.Http.Json;

namespace Warehouse.Client.Services.HttpClients
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<T>> PostAsJsonAsync<T>(string url, object content)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<T>();
                    return ApiResponse<T>.BuildSuccess(responseContent);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    T responseContent = default;
                    try
                    {
                        responseContent = await response.Content.ReadFromJsonAsync<T>();
                        return ApiResponse<T>.BuildFailed(responseContent, response.StatusCode);
                    }
                    catch { }
                }
                return ApiResponse<T>.BuildFailed("Error on sending response. Please try again later", response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                return ApiResponse<T>.BuildFailed($"Server is not responding. {ex.Message}", ex.StatusCode);
            }
        }
    }
}
