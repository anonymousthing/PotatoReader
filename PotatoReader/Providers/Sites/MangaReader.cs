using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotatoReader.Structures;

namespace PotatoReader.Providers.Sites
{
	class MangaReader : Site
	{
		public override async Task<Page> DownloadPage(Page page)
		{
			page.Image = await DownloadHelper.DownloadImageAsync(page.Url);
			return page;
		}

		public override async Task<Book> GetBook(string bookUrl)
		{
			string page = await DownloadHelper.DownloadStringAsync(bookUrl);
			var chaptersLinks = ParseHelper.ParseGroup("<a href=\"(?<Value>[^\"]+)\">(?<Name>[^<]+)</a> :", page, "Name", "Value");
			var resolvedChapters = chaptersLinks.Reverse().GroupBy(x => x.Value).Select(g => g.First()).Reverse().ToArray();
			Book book = new Book();
			var chapters = new List<Chapter>();
			for (int i = 0; i < resolvedChapters.Length; i++)
			{
				var c = resolvedChapters[i];
				chapters.Add(new Chapter()
				{
					DisplayName = c.Name,
					Url = new Uri(new Uri(bookUrl), c.Value).AbsoluteUri,
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
			var pageLinks = ParseHelper.Parse(@"<option value=""(?<Value>[^""]+)""(| selected=""selected"")>\d+</option>", page, "Value");
			var resolvedPages = pageLinks.Select(p => new Uri(new Uri(chapter.Url), p).AbsoluteUri).ToArray();

			var pages = new Page[resolvedPages.Length];

			await Task.WhenAll(resolvedPages.Select((url, i) => Task.Run(async () =>
			{
				var imagePage = await DownloadHelper.DownloadStringAsync(url);
				pages[i] = new Page()
				{
					Chapter = chapter,
					PageNumber = i,
					Url = ParseHelper.Parse(@"<img id=""img"" width=""\d+"" height=""\d+"" src=""(?<Value>[^""]+)""", imagePage, "Value").First()
				};
			})));

			chapter.Pages = pages;
			return chapter;
		}

		public override bool Matches(string path)
		{
			return new Uri(path).Host.ToLower() == "www.mangareader.net";
		}

		public override Task<IEnumerable<Book>> Search(string bookName)
		{
			throw new NotImplementedException();
		}
	}
}
