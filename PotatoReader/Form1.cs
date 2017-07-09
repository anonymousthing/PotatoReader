using PotatoReader.Providers;
using PotatoReader.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotatoReader
{
	public partial class Form1 : Form
	{
		Source source = new OnlineSource();
		PageProvider provider;
		
		public Form1()
		{
			InitializeComponent();
			//We use raw input for CTRL so that Win10 users can zoom with CTRL + scroll wheel without the window being in focus.
			RawInput.RegisterDevice(HIDUsagePage.Generic, HIDUsage.Keyboard, RawInputDeviceFlags.InputSink, this.Handle);
			provider = new PageProvider(source);
			infiniteReader.SetPageProvider(provider);
			ShowBrowserScreen();
		}

		protected override void WndProc(ref Message m)
		{
			WM message = (WM)m.Msg;
			if (message == WM.INPUT)
				RawInput.ProcessMessage(m.LParam);
			base.WndProc(ref m);
		}

		private void SetStatus(string text)
		{
			toolStripStatusLabel1.Text = text;
		}

		private void ShowBrowserScreen()
		{
			panelBrowser.BringToFront();
		}

		private void ShowMangaScreen()
		{
			panelManga.BringToFront();
		}

		private void ShowReaderScreen()
		{
			panelReader.BringToFront();
		}

		private async void LoadBook(string url)
		{
			//Load book
			Book book = await source.LoadBook(url);

			//Set chapters
			listChapters.Items.Clear();
			foreach (var chapter in book.Chapters)
			{
				ListViewItem item = new ListViewItem(chapter.DisplayName);
				item.Tag = chapter;
				listChapters.Items.Add(item);
			}
		}

		private async void listChapters_ItemActivate(object sender, EventArgs e)
		{
			Chapter chapter = (Chapter)listChapters.SelectedItems[0].Tag;
			source.LoadChapter(chapter.Book, chapter.ChapterNumber);
			await Task.Run(() => source.WaitForChapter(chapter.Book, chapter.ChapterNumber));

			infiniteReader.SetPage(source.LoadChapter(chapter.Book, chapter.ChapterNumber).Pages[0]);
			ShowReaderScreen();
		}

		private void menuItemOpenUrl_Click(object sender, EventArgs e)
		{
			var form2 = new Form2();
			form2.ShowDialog();
			if (form2.search)
			{
				LoadBook(form2.searchText);
				ShowMangaScreen();
			}
		}
	}
}
