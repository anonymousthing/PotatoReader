using PotatoReader.Providers.Sites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoReader.Structures
{
	class Book
	{
		/// <summary>
		/// The name of this book.
		/// </summary>
		public string Title;

		/// <summary>
		/// The description or synopsis of this book.
		/// </summary>
		public string Description;

		/// <summary>
		/// The cover image for this book.
		/// </summary>
		public Image CoverImage;

		/// <summary>
		/// The collection of chapters for this book.
		/// </summary>
		public Chapter[] Chapters;

		/// <summary>
		/// The source of this book (either the Site provider or null if on disk).
		/// </summary>
		public Site Source;

		/// <summary>
		/// The source URL of this book.
		/// </summary>
		public string Url;

		/// <summary>
		/// The genres for this book.
		/// </summary>
		public string[] Genres;
	}
}
