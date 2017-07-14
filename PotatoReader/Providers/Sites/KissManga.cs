using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotatoReader.Structures;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using Jurassic;
using System.Diagnostics;

namespace PotatoReader.Providers.Sites
{
	class KissMangaTextDecryptor
	{
		public KissMangaTextDecryptor(string iv, string chko)
		{
			IV = Enumerable.Range(0, iv.Length)
					 .Where(x => x % 2 == 0)
					 .Select(x => Convert.ToByte(iv.Substring(x, 2), 16))
					 .ToArray(); ;
			Key = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(chko));
		}

		public byte[] Key { get; set; }

		public byte[] IV { get; set; }

		/// <summary>
		/// Decrypt the Text
		/// </summary>
		/// <param name="cipherText"></param>
		/// <returns></returns>
		public string DecryptFromBase64(string cipherText)
		{
			var encrypted = Convert.FromBase64String(cipherText);
			var decriptedFromJavascript = DecryptFromBytes(encrypted);
			return decriptedFromJavascript;
		}

		private string DecryptFromBytes(byte[] cipherText)
		{
			try
			{
				var decryptor = CreateDecryptor();
				using (var msDecrypt = new MemoryStream(cipherText))
				using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
				using (var srDecrypt = new StreamReader(csDecrypt))
				{
					return srDecrypt.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Could not decrypt KissManga links");
			}
		}

		private ICryptoTransform CreateDecryptor()
		{
			using (var rijAlg = new RijndaelManaged())
			{
				rijAlg.Mode = CipherMode.CBC;
				rijAlg.Padding = PaddingMode.PKCS7;
				rijAlg.FeedbackSize = 128;
				rijAlg.Key = Key;
				rijAlg.IV = IV;
				return rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
			}
		}
	}

	class KissManga : Site
	{
		KissMangaTextDecryptor decryptor;
		ScriptEngine engine = new ScriptEngine();

		public KissManga()
		{
			LoadKeys().Wait();
		}

		public async Task LoadKeys()
		{
			string page = await DownloadHelper.DownloadStringAsync("http://kissmanga.com/Manga/Black-Clover/Chapter-001?id=277498");
			/// Could be secured against changes by capturing the script's path as it exists in the live document instead of assuming the location.
			string pattern = "<script\\s+(type=[\"']text/javascript[\"'])?\\s+(src=[\"']/Scripts/{0}[\"'])>";
			string concatedPattern = string.Concat(string.Format(pattern, "ca.js"), "|", string.Format(pattern, "lo.js"));

			if (Regex.IsMatch(page, concatedPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
			{
				string funcUri = "http://kissmanga.com/Scripts/lo.js";
				string decryptFunc = await DownloadHelper.DownloadStringAsync(funcUri);

				/// Execute CryptoJS from saved resources to reduce HTTP requests.
				engine.Execute(Properties.Resources.CryptoJs);

				/// Execute the decryption function to allow it to be called later.
				engine.Execute(decryptFunc);
				
				var keysPattern = "<script type=\"text/javascript\">[\\s]*(?<Value>.*)(?!</script>)";
				var regex = new Regex(keysPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
				var keys = string.Empty;

				foreach (Match match in regex.Matches(page))
				{
					if (match.Value.Contains("CryptoJS"))
					{
						keys = match.Groups["Value"].Value;
						break;
					}
				}

				if (string.IsNullOrWhiteSpace(keys))
				{
					throw new ArgumentException("Cannot decrypt image URIs.");
				}
				else
				{
					engine.Execute(keys);
				}

				decryptor = new KissMangaTextDecryptor((string)engine.Evaluate("iv.toString()"), (string)engine.Evaluate("chko"));
			}
		}

		public override async Task<Page> DownloadPage(Page page)
		{
			page.Image = await DownloadHelper.DownloadImageAsync(page.Url);
			return page;
		}

		public override async Task<Book> GetBook(string bookUrl)
		{
			string page = await DownloadHelper.DownloadStringAsync(bookUrl);
			var chaptersLinks = ParseHelper.ParseGroup("<td>\\s+<a\\s+href=\"(?=/Manga/)(?<Url>.[^\"]*)\"\\s+title=\"(?<Name>.[^\"]*)\"", page, "Name", "Url");
			var resolvedChapters = chaptersLinks.Select(c => NameResolver(c.Name, c.Value, bookUrl)).Reverse().ToArray();
			Book book = new Book();
			var chapters = new List<Chapter>();
			for (int i = 0; i < resolvedChapters.Length; i++)
			{
				var c = resolvedChapters[i];
				chapters.Add(new Chapter()
				{
					DisplayName = c.Name,
					Url = c.Url,
					Book = book,
					ChapterNumber = i
				});
			}
			book.Chapters = chapters.ToArray();
			book.Url = bookUrl;

			return book;
		}

		public override async Task<Chapter> GetPageUrls(Chapter chapter)
		{
			string page = await DownloadHelper.DownloadStringAsync(chapter.Url);
			var encryptedPages = ParseHelper.Parse("lstImages.push\\(wrapKA\\(\"(?<Value>.[^\"]*)\"\\)\\)", page, "Value");
			var pageLinks = encryptedPages.Select(e => decryptor.DecryptFromBase64(e)).ToArray();
			var pages = new List<Page>();
			for (int i = 0; i < pageLinks.Length; i++) {
				var pageLink = pageLinks[i];
				pages.Add(new Page()
				{
					Chapter = chapter,
					PageNumber = i,
					Url = new Uri(new Uri(chapter.Url), pageLink).AbsoluteUri
				});
			}

			chapter.Pages = pages.ToArray();
			return chapter;
		}

		public override bool Matches(string path)
		{
			return new Uri(path).Host.ToLower() == "kissmanga.com";
		}

		public override async Task<IEnumerable<Book>> Search(string bookName)
		{
			throw new NotImplementedException();
		}

		private (string Name, string Url) NameResolver(string name, string chapterUrl, string bookUrl)
		{
			var urle = new Uri(new Uri(bookUrl), chapterUrl);
			if (!string.IsNullOrWhiteSpace(name))
			{
				name = System.Net.WebUtility.HtmlDecode(name);
				name = Regex.Replace(name, "^Read\\s+|\\s+online$|:", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
				name = Regex.Replace(name, "\\s+Read\\s+Online$", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}
			return (name, urle.AbsoluteUri);
		}
	}
}
