using PotatoReader.Providers.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotatoReader.Structures;
using System.Threading;

namespace PotatoReader.Providers
{
	class OnlineSource : Source
	{
		List<Site> sites = new List<Site>()
		{
			new MangaReader(),
			new KissManga(),
			new MangaHere()
		};
		public HashSet<Chapter> downloadingChapters = new HashSet<Chapter>();

		public OnlineSource()
		{
		}
		
		public override Chapter LoadChapter(Book book, int chapterNumber, Action recheckCallback)
		{
			//Book data is always available in an online provider
			Chapter chapter = book.Chapters[chapterNumber];

			//Check if chapter has been downloaded
			if (!chapter.Loaded)
			{
				lock (downloadingChapters)
				{
					if (downloadingChapters.Contains(chapter))
						return null;
					downloadingChapters.Add(chapter);
				}
				Task.Run(async () =>
				{
					await book.Source.GetPageUrls(chapter);
					lock (downloadingChapters)
					{
						downloadingChapters.Remove(chapter);
					}
					recheckCallback();
				});

				return null;
			} else
			{
				return chapter;
			}
		}

		public override async Task<Book> LoadBook(string path)
		{
			foreach (var site in sites)
			{
				if (site.Matches(path))
				{
					Book book = await site.GetBook(path);
					book.Source = site;
					return book;
				}
			}
			return null;
		}

		public override async Task<Page> LoadPage(Page page)
		{
			return await page.Chapter.Book.Source.DownloadPage(page);
		}

		public override void WaitForChapter(Book book, int chapterNumber)
		{
			while (!book.Chapters[chapterNumber].Loaded)
			{
				Thread.Sleep(1);
			}
		}
	}
}
