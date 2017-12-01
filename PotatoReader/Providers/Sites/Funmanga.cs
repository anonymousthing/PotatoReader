using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotatoReader.Structures;

namespace PotatoReader.Providers.Sites
{
	class Funmanga : Site
	{
		public override async Task<Page> DownloadPage(Page page)
		{
			page.Image = await DownloadHelper.DownloadImageAsync(page.Url);
			return page;
		}

		public override async Task<Book> GetBook(string bookUrl)
		{
			string page = await DownloadHelper.DownloadStringAsync(bookUrl);
			var chaptersLinks = ParseHelper.ParseGroup("<li>\\s*<a href=\"(?<Value>http://[^\"]+)\">\\s*<span class=\"val\">(?<Name>.+)</span>", page, "Name", "Value").Reverse().ToArray();
			Book book = new Book();
			var chapters = new List<Chapter>();
			for (int i = 0; i < chaptersLinks.Length; i++)
			{
				var c = chaptersLinks[i];
				chapters.Add(new Chapter()
				{
					DisplayName = c.Name,
					Url = c.Value,
					Book = book,
					ChapterNumber = i
				});
			}
			book.Chapters = chapters.ToArray();
			book.Url = bookUrl;

			//Get other book details
			var imgUrl = ParseHelper.Parse("<div class=\"col-md-4\">\\s*<img src=\"(?<Value>.+)\" alt=", page, "Value").First();
			book.CoverImage = await DownloadHelper.DownloadImageAsync(imgUrl);
			book.Title = ParseHelper.Parse("<h5 class=\"widget-heading\">(?<Value>.+)</h5>", page, "Value").First();
			book.Description = ParseHelper.Parse("<div class=\"note note-default margin-top-15\">(?<Value>(.|\\s)*?)</div>", page, "Value").First().Trim();
			book.Description = book.Description.Replace("<br/>", "\n");

			return book;
		}

		public override async Task<Chapter> GetPageUrls(Chapter chapter)
		{
			string page = await DownloadHelper.DownloadStringAsync(chapter.Url);
			var pageLinks = ParseHelper.Parse("{\"id\":[0-9]+,\"url\":\"(?<Value>http:.+?)\"}", page, "Value");
			var resolvedPages = pageLinks.Select(p => p.Replace("\\/", "/")).ToArray();

			var pages = resolvedPages.Select((url, i) =>
			{
				return new Page()
				{
					Chapter = chapter,
					PageNumber = i,
					Url = url
				};
			}).ToArray();
			
			chapter.Pages = pages;
			return chapter;
		}

		public override bool Matches(string path)
		{
			return new Uri(path).Host.ToLower() == "funmanga.com" || new Uri(path).Host.ToLower() == "www.funmanga.com";
		}

		public override async Task<IEnumerable<Book>> Search(string bookName)
		{
			throw new NotImplementedException();
		}
	}
}
