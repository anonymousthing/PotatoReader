using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotatoReader.Structures;

namespace PotatoReader.Providers.Sites
{
	class MangaHere : Site
	{
		public override async Task<Page> DownloadPage(Page page)
		{
			page.Image = await DownloadHelper.DownloadImageAsync(page.Url);
			return page;
		}

		public override async Task<Book> GetBook(string bookUrl)
		{
			string page = await DownloadHelper.DownloadStringAsync(bookUrl);
			var chaptersLinks = ParseHelper.ParseGroup("<a class=\"color_0077\" href=\"(?<Value>.*?)\".*>(?<Name>\\s*.*\\s*)</a>", page, "Name", "Value").Reverse().ToArray();
			Book book = new Book();
			var chapters = new List<Chapter>();
			for (int i = 0; i < chaptersLinks.Length; i++)
			{
				var c = chaptersLinks[i];
				chapters.Add(new Chapter()
				{
					DisplayName = c.Name,
					Url = "https:" + c.Value,
					Book = book,
					ChapterNumber = i
				});
			}
			book.Chapters = chapters.ToArray();
			book.Url = bookUrl;

			//Get other book details
			var imgUrl = ParseHelper.Parse("<div class=\"manga_detail_top clearfix\">\\s+<img src=\"(?<Value>.+)\" onerror=", page, "Value").First();
			book.CoverImage = await DownloadHelper.DownloadImageAsync(imgUrl);
			book.Title = ParseHelper.Parse("<h2>(?<Value>.+) Manga</h2>\\s+Summary:", page, "Value").First();
			book.Description = ParseHelper.Parse("<p id=\"show\" style=\"display:none;\">(?<Value>[^\"]+)<a href=\"javascript:void", page, "Value").First();
			book.Description = System.Web.HttpUtility.HtmlDecode(book.Description);

			return book;
		}

		public override async Task<Chapter> GetPageUrls(Chapter chapter)
		{
			string page = await DownloadHelper.DownloadStringAsync(chapter.Url);
			var pageLinks = ParseHelper.Parse("<option value=\"(?<Value>.*?)\".*>\\d+</option>", page, "Value");
			var resolvedPages = pageLinks.Select(p => new Uri(new Uri(chapter.Url), p).AbsoluteUri).ToArray();

			var pages = new Page[resolvedPages.Length];

			await Task.WhenAll(resolvedPages.Select((url, i) => Task.Run(async () =>
			{
				var imagePage = await DownloadHelper.DownloadStringAsync(url);
				pages[i] = new Page()
				{
					Chapter = chapter,
					PageNumber = i,
					Url = ParseHelper.Parse("<img src=\"(?<Value>.*?)\" onload=", imagePage, "Value").First()
			};
			})));

			chapter.Pages = pages;
			return chapter;
		}

		public override bool Matches(string path)
		{
			return new Uri(path).Host.ToLower() == "www.mangahere.co";
		}

		public override async Task<IEnumerable<Book>> Search(string bookName)
		{
			throw new NotImplementedException();
		}
	}
}
