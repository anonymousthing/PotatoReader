using CloudFlareUtilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PotatoReader.Providers.Sites
{
	static class DownloadHelper
	{
		static HttpClient client = CreateHttpClient();
		const int retryCap = 3;

		public static async Task<Image> DownloadImageAsync(string url)
		{
			using (var response = await client.GetAsync(url))
			{
				return Image.FromStream(await response.Content.ReadAsStreamAsync());
			}
		}
		
		public static async Task<string> DownloadStringAsync(string url)
		{
			return await DownloadStringAsync(url, 0);
		}

		private static async Task<string> DownloadStringAsync(string url, int retryCount)
		{
			using (var response = await client.GetAsync(url))
			{
				if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
				{
					if (retryCount == retryCap)
						return null;
					await Task.Delay(500);
					return await DownloadStringAsync(url, retryCount + 1);
				}
				return await response.Content.ReadAsStringAsync();
			}
		}

		private static HttpClient CreateHttpClient()
		{
			var firstHandle = new HttpClientHandler
			{
				AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
				Credentials = CredentialCache.DefaultCredentials,
				AllowAutoRedirect = false,
				CookieContainer = new CookieContainer()
			};

			var handler = new ClearanceHandler(firstHandle);
			HttpClient client = new HttpClient(handler);
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36");
			return client;
		}
	}
}
