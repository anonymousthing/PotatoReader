using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoReader.Structures
{
	class Chapter
	{
		/// <summary>
		/// The display name of this chapter.
		/// </summary>
		public string DisplayName;

		/// <summary>
		/// The internal chapter number, numeric and starting from 0.
		/// </summary>
		public int ChapterNumber;

		/// <summary>
		/// The book that this chapter belongs to.
		/// </summary>
		public Book Book;

		/// <summary>
		/// The collection of pages in this chapter.
		/// </summary>
		public Page[] Pages;

		/// <summary>
		/// Whether this chapter has been downloaded yet or not.
		/// </summary>
		public bool Loaded
		{
			get { return Pages != null; }
		}

		/// <summary>
		/// The URL for this specific chapter, for this book's source.
		/// </summary>
		public string Url;

		public override string ToString()
		{
			return "#" + ChapterNumber + " - '" + DisplayName + "'";
		}
	}
}
