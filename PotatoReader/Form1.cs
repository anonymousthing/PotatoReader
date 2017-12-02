using PotatoReader.Providers;
using PotatoReader.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
			CheckForUpdates();
		}

		protected override void WndProc(ref Message m)
		{
			WM message = (WM)m.Msg;
			if (message == WM.INPUT)
				RawInput.ProcessMessage(m.LParam);
			base.WndProc(ref m);
		}

		private async void CheckForUpdates()
		{
			if (await Updater.CheckVersion())
			{
				System.Media.SystemSounds.Beep.Play(); //DING
				if (MessageBox.Show(this, "An update is available for PotatoReader. Do you want to update and restart the application now?",
						"PotatoReader - Update available", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					await Updater.UpdateToNewVersion(Wc_DownloadProgressChanged, Wc_DownloadFileCompleted);
				}
			}
		}

		private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			toastPanel1.SetUpdateStatus(2, 100);
			this.Invalidate();
		}

		private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			toastPanel1.SetUpdateStatus(1, e.BytesReceived / (float)e.TotalBytesToReceive);
			this.Invalidate();
		}

		private void SetStatus(string text)
		{
			//toolStripStatusLabel1.Text = text;
		}

		private void ShowBrowserScreen()
		{
			tablessTabControl1.SelectedTab = screenBrowser;
		}

		private void ShowMangaScreen()
		{
			tablessTabControl1.SelectedTab = screenManga;
		}

		private void ShowReaderScreen()
		{
			tablessTabControl1.SelectedTab = screenReader;
		}

		private async Task LoadBook(string url)
		{
			if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
			{
				label2.Text = "Unsupported source! Check your url.";
				return;
			}

			ShowMangaScreen();

			//Load book
			Book book = await source.LoadBook(url);
			if (book == null)
			{
				label2.Text = "Unsupported source! Check your url.";
				ShowBrowserScreen();
				return;
			}

			lblMangaTitle.Text = book.Title;
			pictureBoxCoverImage.Image = book.CoverImage;
			lblMangaDescription.Text = book.Description;

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
			ShowReaderScreen();
			Chapter chapter = (Chapter)listChapters.SelectedItems[0].Tag;
			source.LoadChapter(chapter.Book, chapter.ChapterNumber, () => { });
			await Task.Run(() => source.WaitForChapter(chapter.Book, chapter.ChapterNumber));

			infiniteReader.SetPage(source.LoadChapter(chapter.Book, chapter.ChapterNumber, () => { }).Pages[0]);
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			await LoadBook(textBox1.Text);
		}

		private void menuItem4_Click(object sender, EventArgs e)
		{
			ShowBrowserScreen();
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			ShowMangaScreen();
		}

		private void menuItem3_Click(object sender, EventArgs e)
		{
			ShowReaderScreen();
		}

		private async void menuItem6_Click(object sender, EventArgs e)
		{
			var currentPage = infiniteReader.CurrentPage;
			var book = currentPage.Chapter.Book;
			var chapterNumber = currentPage.Chapter.ChapterNumber + 1;

			source.LoadChapter(book, chapterNumber, () => { });
			await Task.Run(() => source.WaitForChapter(book, chapterNumber));
			infiniteReader.SetPage(source.LoadChapter(book, chapterNumber, () => { }).Pages[0]);
		}

		private async void Form1_DragDrop(object sender, DragEventArgs e)
		{
			string data = (string)e.Data.GetData(DataFormats.UnicodeText);
			await LoadBook(data);
		}

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.UnicodeText))
				e.Effect = DragDropEffects.Copy;
		}
	}
}
