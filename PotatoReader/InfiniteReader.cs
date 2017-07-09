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
				previousPage = provider.GetPreviousPage(previousPage, this.Invalidate);
				loadedPages.PushLeft(previousPage);
			}

			//Add next pages
			Page nextPage = page;
			for (int i = 0; i < buffer; i++)
			{
				nextPage = provider.GetNextPage(nextPage, this.Invalidate);
				loadedPages.PushRight(nextPage);
			}

			scrollOffset = 0;
			this.Invalidate();
		}

		private int GetScaledHeight(Page page)
		{
			return !page.Loaded ? (int)(1200 * scale) : (int)(page.Image.Height * scale);
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
					float maximumScroll = GetScaledHeight(currentPage);
					for (int i = buffer + 1; i < loadedPages.Count; i++)
					{
						if (loadedPages[i] == null)
							break;
						maximumScroll += GetScaledHeight(loadedPages[i]);
					}
					maximumScroll -= Height;
					scrollOffset = Math.Min((int)maximumScroll, scrollOffset + movement);

					//If we zoom out and we're now on the previous page, just update the current page
					while (scrollOffset < 0)
					{
						MoveToPreviousPage();
						scrollOffset += GetScaledHeight(currentPage);
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

			if (movement > 0)
			{
				//Next chapter now loaded
				if (IsMissingNextPages())
				{
					LoadNextPages();
				}
			}
			else
			{
				//Previous chapter now loaded
				if (IsMissingPreviousPages())
				{
					LoadPreviousPages();
				}
			}

			int scaledPageHeight = GetScaledHeight(currentPage);

			if (scrollOffset > scaledPageHeight)
			{
				while (scrollOffset > scaledPageHeight)
				{
					MoveToNextPage();
					scrollOffset = scrollOffset - scaledPageHeight;
				}
			}
			else if (scrollOffset < 0)
			{
				while (scrollOffset < 0)
				{
					MoveToPreviousPage();
					scrollOffset += GetScaledHeight(currentPage);
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
			for (int i = 0; i < buffer; i++)
				loadedPages.PopRight()?.Dispose();

			Page page = currentPage;
			for (int i = 0; i < buffer; i++)
			{
				var newPage = provider.GetNextPage(page, this.Invalidate);
				loadedPages.PushRight(newPage);
				page = newPage;
			}
		}

		private void LoadPreviousPages()
		{
			//Store current page before dropping the other previous pages
			Page page = currentPage;

			//Drop old previous pages
			//TODO: only need to pop null pages
			for (int i = 0; i < buffer; i++)
				loadedPages.PopLeft()?.Dispose();

			for (int i = 0; i < buffer; i++)
			{
				var newPage = provider.GetPreviousPage(page, this.Invalidate);
				loadedPages.PushLeft(newPage);
				page = newPage;
			}
		}
		
		private void MoveToPreviousPage()
		{
			loadedPages.PopRight()?.Dispose();
			loadedPages.PushLeft(provider.GetPreviousPage(loadedPages[0], this.Invalidate));
		}

		private void MoveToNextPage()
		{
			loadedPages.PopLeft()?.Dispose();
			loadedPages.PushRight(provider.GetNextPage(loadedPages[loadedPages.Count - 1], this.Invalidate));
		}

		//Only downscale to fit, don't upscale
		private Size GetFitDimensions(Page page)
		{
			float pageAspectRatio = (page.Image.Width * scale) / (page.Image.Height * scale);
			float controlAspectRatio = Width / (float)Height;
			if (page.Image.Width * scale > Width) 
			{
				return new Size(Width, page.Image.Height * Width / page.Image.Width).Scale(scale);
			}
			return page.Image.Size.Scale(scale);
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
		}
	}
}
