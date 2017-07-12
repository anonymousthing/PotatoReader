using DequeNet;
using PotatoReader.Providers;
using PotatoReader.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotatoReader
{
	class InfiniteReader : Control
	{
		bool renderDebug = true;
		//N pages above, N pages below of buffer space. This refers to loaded pages, not rendered pages.
		int buffer = 7;
		PageProvider provider;
		//Indices [0 -> buffer] are previous pages, index [buffer] is the current page, indices [buffer + 1 -> 2 * buffer] are next pages
		Deque<Page> loadedPages = new Deque<Page>();
		Page currentPage
		{
			get { return loadedPages[buffer]; }
		}		
		Page lastVisiblePage;

		//Scroll applies after scaling
		int scrollOffset = 0;
		//Zoom level is a scale factor
		float scale = 1;
		Font pageNumberFont = new Font("Arial", 10);
		Brush pageNumberBG = new SolidBrush(Color.FromArgb(128, 0, 0, 0));

		public InfiniteReader()
		{
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			this.BackColor = Color.White;
			this.MouseWheel += InfiniteReader_MouseWheel;
			RawInput.RegisterCallback(VirtualKeys.K, () => {
				renderDebug = !renderDebug;
				this.Invalidate();
			});
		}

		public InfiniteReader(PageProvider provider) : this()
		{
			this.provider = provider;
		}

		public void SetPageProvider(PageProvider provider)
		{
			this.provider = provider;
		}

		public void SetPage(Page page)
		{
			if (!page.Loaded)
				provider.LoadPage(page, this.Invalidate);

			//TODO: compare loaded pages and only remove those which are different
			while (loadedPages.Count > 0)
				loadedPages.PopLeft()?.Dispose();
			loadedPages.PushRight(page);
			
			//Add previous pages
			Page previousPage = page;
			for (int i = 0; i < buffer; i++)
			{
				previousPage = provider.GetPreviousPage(previousPage, this.Invalidate, RefetchPages);
				loadedPages.PushLeft(previousPage);
			}

			//Add next pages
			Page nextPage = page;
			for (int i = 0; i < buffer; i++)
			{
				nextPage = provider.GetNextPage(nextPage, this.Invalidate, RefetchPages);
				loadedPages.PushRight(nextPage);
			}

			scrollOffset = 0;
			this.Invalidate();
		}

		/// <summary>
		/// If a new chapter has to be fetched first, this callback will re-ask for pages from the provider once 
		/// the chapter metadata has finished downloading.
		/// </summary>
		private void RefetchPages()
		{
			//Next chapter now loaded
			if (IsMissingNextPages())
			{
				LoadNextPages();
			}

			//Previous chapter now loaded
			if (IsMissingPreviousPages())
			{
				LoadPreviousPages();
			}
		}

		private void InfiniteReader_MouseWheel(object sender, MouseEventArgs e)
		{
			bool zooming = false;
			if (RawInput.IsPressed(VirtualKeys.Control))
			{
				zooming = true;
				if (e.Delta > 0)
					scale *= 1.41f;
				else
					scale /= 1.41f;

				this.Invalidate();
			}
			int movement = -e.Delta;

			//Don't allow scrolling if the last visible page is the last page available
			if (movement > 0 || zooming)
			{
				if (provider.IsLastPage(lastVisiblePage))
				{
					float maximumScroll = GetFitDimensions(currentPage).Height;
					for (int i = buffer + 1; i < loadedPages.Count; i++)
					{
						if (loadedPages[i] == null)
							break;
						maximumScroll += GetFitDimensions(loadedPages[i]).Height;
					}
					maximumScroll -= Height;
					scrollOffset = Math.Min((int)maximumScroll, scrollOffset + movement);

					//If we zoom out and we're now on the previous page, just update the current page
					while (scrollOffset < 0)
					{
						MoveToPreviousPage();
						scrollOffset += GetFitDimensions(currentPage).Height;
					}

					this.Invalidate();
					return;
				}
			}
			else if (movement < 0)
			{
				if (provider.IsFirstPage(currentPage))
				{
					scrollOffset = Math.Max(0, scrollOffset + movement);
					this.Invalidate();
					return;
				}
			}

			if (zooming)
				return;
			
			scrollOffset += movement;

			RefetchPages();

			int scaledPageHeight = GetFitDimensions(currentPage).Height;

			if (scrollOffset > scaledPageHeight)
			{
				while (scrollOffset > scaledPageHeight)
				{
					MoveToNextPage();
					scrollOffset = scrollOffset - scaledPageHeight;
					scaledPageHeight = GetFitDimensions(currentPage).Height;
				}
			}
			else if (scrollOffset < 0)
			{
				while (scrollOffset < 0)
				{
					MoveToPreviousPage();
					scrollOffset += GetFitDimensions(currentPage).Height;
				}
			}

			this.Invalidate();
		}

		private bool IsMissingPreviousPages()
		{
			for (int i = buffer - 1; i >= 0; i--)
			{
				if (loadedPages[i] == null)
				{
					if (!provider.IsFirstPage(loadedPages[i + 1]))
						return true;
					break;
				}
			}
			return false;
		}

		private bool IsMissingNextPages()
		{
			for (int i = buffer + 1; i < loadedPages.Count ; i++)
			{
				if (loadedPages[i] == null)
				{
					if (!provider.IsLastPage(loadedPages[i - 1]))
						return true;
					break;
				}
			}
			return false;
		}

		private void LoadNextPages()
		{
			//Pop (and count) all nulls from the right of the deque
			int i = 0;
			for (; i < buffer && loadedPages[loadedPages.Count - 1] == null; i++)
				loadedPages.PopRight()?.Dispose();

			//Fetch i next pages
			Page page = loadedPages[loadedPages.Count - 1];
			for (int j = 0; j < i; j++)
			{
				var newPage = provider.GetNextPage(page, this.Invalidate, RefetchPages);
				loadedPages.PushRight(newPage);
				page = newPage;
			}

			this.Invalidate();
		}

		private void LoadPreviousPages()
		{
			//Drop old previous pages
			int i = 0;
			for (; i < buffer && loadedPages[0] == null; i++)
				loadedPages.PopLeft()?.Dispose();

			Page page = loadedPages[0];
			for (int j = 0; j < i; j++)
			{
				var newPage = provider.GetPreviousPage(page, this.Invalidate, RefetchPages);
				loadedPages.PushLeft(newPage);
				page = newPage;
			}

			this.Invalidate();
		}
		
		private void MoveToPreviousPage()
		{
			loadedPages.PopRight()?.Dispose();
			loadedPages.PushLeft(provider.GetPreviousPage(loadedPages[0], this.Invalidate, RefetchPages));
		}

		private void MoveToNextPage()
		{
			loadedPages.PopLeft()?.Dispose();
			loadedPages.PushRight(provider.GetNextPage(loadedPages[loadedPages.Count - 1], this.Invalidate, RefetchPages));
		}

		//Only downscale to fit, don't upscale
		private Size GetFitDimensions(Page page)
		{
			//Default dimensions if not loaded
			int pageWidth = 500;
			int pageHeight = 1200;

			if (page.Loaded)
			{
				pageWidth = page.Image.Width;
				pageHeight = page.Image.Height;
			}

			float pageAspectRatio = (pageWidth * scale) / (pageHeight * scale);
			float controlAspectRatio = Width / (float)Height;
			if (pageWidth * scale > Width) 
			{
				return new Size(Width, pageHeight * Width / pageWidth).Scale(scale);
			}
			return new Size(pageWidth, pageHeight).Scale(scale);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (loadedPages.Count < buffer + 1)
				return;
			
			//First draw the current page at (0, -scrollOffset)
			float currentHeight;
			if (currentPage == null || !currentPage.Loaded)
			{
				currentHeight = 1200 * scale;
				e.Graphics.FillRectangle(Brushes.LightBlue, (Width - 500 * scale) / 2, scrollOffset, 500 * scale, 1200 * scale);
			}
			else
			{
				var size = GetFitDimensions(currentPage);
				currentHeight = size.Height;
				e.Graphics.DrawImage(currentPage.Image, (Width - size.Width) / 2, -scrollOffset, size.Width, size.Height);
				//e.Graphics.DrawLine(Pens.Red, new Point(0, -scrollOffset), new Point(Width, -scrollOffset));
			}

			//Iteratively draw next pages until we can't see those pages anymore
			//(only draw pages that are visible)
			int i = buffer + 1;
			for (; i < loadedPages.Count; i++)
			{
				if (currentHeight - scrollOffset > Height || loadedPages[i] == null)
				{
					break;
				}
				if (!loadedPages[i].Loaded)
				{
					e.Graphics.FillRectangle(Brushes.LightBlue, (Width - 500 * scale) / 2, currentHeight - scrollOffset, 500 * scale, 1200 * scale);
					currentHeight += 1200 * scale;
				}
				else
				{
					var size = GetFitDimensions(loadedPages[i]);
					e.Graphics.DrawImage(loadedPages[i].Image, (Width - size.Width) / 2, currentHeight - scrollOffset, size.Width, size.Height);
					if (renderDebug)
						e.Graphics.DrawLine(Pens.Red, new Point(0, (int)(currentHeight - scrollOffset)), new Point(Width, (int)(currentHeight - scrollOffset)));
					currentHeight += size.Height;
				}
			}
			lastVisiblePage = loadedPages[i - 1];

			//Render page numbers
			string pageNumberText = (currentPage.PageNumber + 1) + "/" + currentPage.Chapter.Pages.Length;
			var pageNumberSize = e.Graphics.MeasureString(pageNumberText, pageNumberFont);
			var pageNumberPosition = new PointF(Width - pageNumberSize.Width, Height - pageNumberSize.Height);
			e.Graphics.FillRectangle(pageNumberBG, new RectangleF(pageNumberPosition, pageNumberSize));
			e.Graphics.DrawString(pageNumberText, pageNumberFont, Brushes.White, pageNumberPosition);

			if (renderDebug)
				RenderDebug(e.Graphics);
		}

		private void RenderDebug(Graphics g)
		{
			g.FillRectangle(new SolidBrush(Color.FromArgb(100, 0, 0, 0)), 0, 0, 200, 400);
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < loadedPages.Count; i++)
			{
				var page = loadedPages[i];
				builder.Append(page?.ToString() ?? "No page");
				if (i == buffer)
					builder.Append(" -- current page");
				builder.AppendLine();
			}

			builder.AppendLine();
			builder.AppendLine("Waiting on chapter info:");
			if (provider.source is OnlineSource)
			{
				var pendingChapters = ((OnlineSource)provider.source).downloadingChapters;
				foreach (var chapter in pendingChapters)
					builder.AppendLine(chapter.ToString());
			}

			g.DrawString(builder.ToString(), pageNumberFont, Brushes.White, new Point(0, 0));
		}
	}
}
