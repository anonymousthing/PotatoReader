using PotatoReader.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoReader.Providers.Sites
{
	abstract class Site
	{
		public abstract Task<IEnumerable<Book>> Search(string bookName);

		public abstract Task<Book> GetBook(string bookUrl);

		public abstract Task<Chapter> GetPageUrls(Chapter chapter);

		public abstract Task<Page> DownloadPage(Page page);

		public abstract bool Matches(string path);
	}
}
