﻿using System.Net.Http.Json;

namespace Warehouse.Client.Services.HttpClients
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ApiResponse<T>> GetAsJsonAsync<T>(string url, object content)
        {
            try
            {
                var responseContent = await _httpClient.GetFromJsonAsync<T>(url, content);
                return ApiResponse<T>.BuildSuccess(responseContent);
            }
            catch (HttpRequestException ex)
            {
                return ApiResponse<T>.BuildFailed($"Server is not responding. {ex.Message}", ex.StatusCode);
            }
        }

        public async Task<ApiResponse> DeleteAsync(string url)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                    return ApiResponse.BuildSuccess();
                return ApiResponse.BuildFailed("Error on sending response. Please try again later", response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                return ApiResponse.BuildFailed($"Server is not responding. {ex.Message}", ex.StatusCode);
            }
        }

        public async Task<ApiResponse<T>> PostAsJsonAsync<T>(string url, object content)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, content);
                return await GetApiResponseAsync<T>(response);
            }
            catch (HttpRequestException ex)
            {
                return ApiResponse<T>.BuildFailed($"Server is not responding. {ex.Message}", ex.StatusCode);
            }
        }

        async Task<ApiResponse<T>> GetApiResponseAsync<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<T>();
                return ApiResponse<T>.BuildSuccess(responseContent);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                try
                {
                    T responseContent = await response.Content.ReadFromJsonAsync<T>();
                    return ApiResponse<T>.BuildFailed(responseContent, response.StatusCode);
                }
                catch { }
            }
            return ApiResponse<T>.BuildFailed("Error on sending response. Please try again later", response.StatusCode);
        }

        public async Task<ApiResponse<T>> PutAsJsonAsync<T>(string url, object content)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(url, content);
                return await GetApiResponseAsync<T>(response);
            }
            catch (HttpRequestException ex)
            {
                return ApiResponse<T>.BuildFailed($"Server is not responding. {ex.Message}", ex.StatusCode);
            }
        }
    }
}