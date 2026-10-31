using Microsoft.JSInterop;
using System.Text.Json;
using Utilities;

namespace MauiHybridAutoApp1.Web.Client.Services
{
    public class ClientLocalStorage : ILocalStorage
    {
        private readonly IJSRuntime jsruntime;
        public ClientLocalStorage(IJSRuntime jSRuntime)
        {
            jsruntime = jSRuntime;
        }

        /// <summary>
        /// Remove a key from browser local storage.
        /// </summary>
        /// <param name="key">The key previously used to save to local storage.</param>
        public async Task RemoveAsync(string key)
        {
            await jsruntime.InvokeVoidAsync("localStorage.removeItem", key).ConfigureAwait(false);
        }

        /// <summary>
        /// Save a string value to browser local storage.
        /// </summary>
        /// <param name="key">The key to use to save to and retrieve from local storage.</param>
        /// <param name="value">The string value to save to local storage.</param>
        public async Task SaveStringAsync(string key, string value)
        {
            await jsruntime.InvokeVoidAsync("localStorage.setItem", key, value).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a string value from browser local storage.
        /// </summary>
        /// <param name="key">The key previously used to save to local storage.</param>
        /// <returns>The string previously saved to local storage.</returns>
        public async Task<string> GetStringAsync(string key)
        {
            return await jsruntime.InvokeAsync<string>("localStorage.getItem", key).ConfigureAwait(false);
        }

        /// <summary>
        /// Save an array of string values to browser local storage.
        /// </summary>
        /// <param name="key">The key previously used to save to local storage.</param>
        /// <param name="values">The array of string values to save to local storage.</param>
        public async Task SaveStringArrayAsync(string key, string[] values)
        {
            if (values != null)
                await jsruntime.InvokeVoidAsync("localStorage.setItem", key, string.Join('\0', values)).ConfigureAwait(false);
        }

        /// <summary>
        /// Get an array of string values from browser local storage.
        /// </summary>
        /// <param name="key">The key previously used to save to local storage.</param>
        /// <returns>The array of string values previously saved to local storage.</returns>
        public async Task<string[]> GetStringArrayAsync(string key)
        {
            var data = await jsruntime.InvokeAsync<string>("localStorage.getItem", key).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(data))
                return data.Split('\0');
            return null;
        }

        /// <summary>
        /// Save an object value to browser local storage.
        /// </summary>
        /// <param name="key">The key to use to save to and retrieve from local storage.</param>
        /// <param name="value">The object to save to local storage.</param>
        public async Task SaveObjectAsync<T>(string key, T value)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize<T>(value, options);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            string b64 = System.Convert.ToBase64String(data);
            await SaveStringAsync(key, b64);
        }

        /// <summary>
        /// Get an object from browser local storage.
        /// </summary>
        /// <param name="key">The key previously used to save to local storage.</param>
        /// <returns>The object previously saved to local storage.</returns>
        public async Task<T?> GetObjectAsync<T>(string key)
        {
            string b64 = await GetStringAsync(key);
            if (b64 == null)
                return default(T);
            byte[] data = System.Convert.FromBase64String(b64);
            string json = System.Text.Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
