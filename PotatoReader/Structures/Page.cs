using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoReader.Structures
{
	class Page : IDisposable
	{
		/// <summary>
		/// The chapter that this page belongs to.
		/// </summary>
		public Chapter Chapter;

		/// <summary>
		/// The zero-indexed internal page number of this page, within the chapter.
		/// </summary>
		public int PageNumber;

		/// <summary>
		/// The image data of this page.
		/// </summary>
		public Image Image;

		/// <summary>
		/// Whether this page is invalid or not (e.g. if it could not be downloaded or if it is corrupted)
		/// </summary>
		public bool IsInvalid;

		/// <summary>
		/// Whether this image has been downloaded yet or not.
		/// </summary>
		public bool Loaded
		{
			get { return Image != null; }
		}

		/// <summary>
		/// The URL to the image.
		/// </summary>
		public string Url;

		public override string ToString()
		{
			string baseString = "Chapter " + Chapter.ChapterNumber + ": " + PageNumber + "/" + Chapter.Pages.Length;
			if (Image == null)
				return baseString + " - Not loaded yet";
			return baseString;
		}

		public void Dispose()
		{
			if (Image != null)
			{
				Image.Dispose();
				Image = null;
			}
		}
	}
}
