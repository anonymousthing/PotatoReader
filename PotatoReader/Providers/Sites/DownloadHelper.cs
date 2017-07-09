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

		public static async Task<Image> DownloadImageAsync(string url)
		{
			using (var response = await client.GetAsync(url))
			{
				return Image.FromStream(await response.Content.ReadAsStreamAsync());
			}
		}

		public static async Task<string> DownloadStringAsync(string url)
		{
			using (var response = await client.GetAsync(url))
			{
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
			return new HttpClient(handler);
		}
	}
}
