using PotatoReader.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoReader.Providers
{
	class PageProvider
	{
		public Source source;

		public PageProvider(Source source)
		{
			this.source = source;
		}

		/// <summary>
		/// Determines if the specified page is the last available loaded page,
		/// i.e. whether a chapter request would need to be made in order to retrieve the page after the specified page.
		/// </summary>
		/// <param name="page">The page to check.</param>
		/// <returns>True if the page is the last page in the book; false otherwise.</returns>
		public bool IsLastPage(Page page)
		{
			Chapter chapter = page.Chapter;
			if (page.PageNumber == chapter.Pages.Length - 1)
			{
				Book book = chapter.Book;
				if (chapter.ChapterNumber == book.Chapters.Length - 1 || !book.Chapters[chapter.ChapterNumber + 1].Loaded)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Determines if the specified page is the first available loaded page,
		/// i.e. whether a chapter request would need to be made in order to retrieve the page before the specified page.
		/// </summary>
		/// <param name="page">The page to check.</param>
		/// <returns>True if the page is the first page in the book; false otherwise.</returns>
		public bool IsFirstPage(Page page)
		{
			if (page.PageNumber == 0)
			{
				Chapter chapter = page.Chapter;
				if (chapter.ChapterNumber == 0 || !chapter.Book.Chapters[chapter.ChapterNumber - 1].Loaded)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the next page after this page, crossing over to the next chapter if required.
		/// </summary>
		/// <param name="page">The page to get the next page for</param>
		/// <returns>Page, or null if no more pages are available.</returns>
		public Page GetNextPage(Page page, Action repaintCallback, Action recheckCallback)
		{
			if (page == null)
				return null;

			Chapter chapter = page.Chapter;
			if (page.PageNumber == chapter.Pages.Length - 1)
			{
				Book book = chapter.Book;
				//Return first page of the first chapter that isn't empty
				for (int i = chapter.ChapterNumber + 1; i < book.Chapters.Length; i++)
				{
					Chapter nextChapter = source.LoadChapter(book, i, recheckCallback);

					//Future loading
					if (nextChapter == null)
						return null;

					if (nextChapter.Pages.Length > 0)
						return LoadPage(nextChapter.Pages[0], repaintCallback);
				}
				return null;
			}
			return LoadPage(chapter.Pages[page.PageNumber + 1], repaintCallback);
		}

		/// <summary>
		/// Gets the page before this page, crossing over to the previous chapter if required.
		/// </summary>
		/// <param name="page">The page to get the previous page for</param>
		/// <returns>Page, or null if no pages are available.</returns>
		public Page GetPreviousPage(Page page, Action repaintCallback, Action recheckCallback)
		{
			if (page == null)
				return null;

			Chapter chapter = page.Chapter;
			if (page.PageNumber > 0)
				return LoadPage(chapter.Pages[page.PageNumber - 1], repaintCallback);

			if (chapter.ChapterNumber > 0)
			{
				Book book = chapter.Book;
				//Return last page of the first chapter that isn't empty
				for (int i = chapter.ChapterNumber - 1; i >= 0; i--)
				{
					Chapter previousChapter = source.LoadChapter(book, i, recheckCallback);

					//Future loading
					if (previousChapter == null)
						return null;

					if (previousChapter.Pages.Length > 0)
						return LoadPage(previousChapter.Pages[book.Chapters[i].Pages.Length - 1], repaintCallback);
				}

				return null;
			}
			return null;
		}

		/// <summary>
		/// Loads a page and downloads the image if it has not been loaded already.
		/// </summary>
		/// <param name="page">The page to download.</param>
		/// <returns>The input page, with the content downloaded.</returns>
		public Page LoadPage(Page page, Action repaintCallback)
		{
			if (!page.Loaded)
				Task.Run(async () => {
					await source.LoadPage(page);
					repaintCallback();
				});

			return page;
		}
	}
}
