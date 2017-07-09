using PotatoReader.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoReader.Providers
{
	abstract class Source
	{
		public abstract Chapter LoadChapter(Book book, int chapterNumber);
		public abstract void WaitForChapter(Book book, int chapterNumber);
		public abstract Task<Page> LoadPage(Page page);
		public abstract Task<Book> LoadBook(string path);
	}
}
