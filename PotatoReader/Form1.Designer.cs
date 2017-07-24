namespace PotatoReader
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabSummary = new System.Windows.Forms.TabPage();
			this.panelDescription = new System.Windows.Forms.Panel();
			this.lblMangaDescription = new System.Windows.Forms.Label();
			this.lblMangaStatus = new System.Windows.Forms.Label();
			this.pictureBoxCoverImage = new System.Windows.Forms.PictureBox();
			this.lblMangaTitle = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.listChapters = new System.Windows.Forms.ListView();
			this.columnTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.infiniteReader = new PotatoReader.InfiniteReader();
			this.tablessTabControl1 = new PotatoReader.TablessTabControl();
			this.screenManga = new System.Windows.Forms.TabPage();
			this.screenReader = new System.Windows.Forms.TabPage();
			this.screenBrowser = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.toastPanel1 = new PotatoReader.ToastPanel();
			this.tabControl1.SuspendLayout();
			this.tabSummary.SuspendLayout();
			this.panelDescription.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxCoverImage)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.tablessTabControl1.SuspendLayout();
			this.screenManga.SuspendLayout();
			this.screenReader.SuspendLayout();
			this.screenBrowser.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItem1});
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 0;
			this.menuItemFile.Text = "File";
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem2,
            this.menuItem3});
			this.menuItem1.Text = "View";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 0;
			this.menuItem4.Text = "Show browser screen";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Show manga screen";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Show reader screen";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabSummary);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(825, 710);
			this.tabControl1.TabIndex = 6;
			// 
			// tabSummary
			// 
			this.tabSummary.Controls.Add(this.panelDescription);
			this.tabSummary.Controls.Add(this.lblMangaStatus);
			this.tabSummary.Controls.Add(this.pictureBoxCoverImage);
			this.tabSummary.Controls.Add(this.lblMangaTitle);
			this.tabSummary.Location = new System.Drawing.Point(4, 22);
			this.tabSummary.Name = "tabSummary";
			this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
			this.tabSummary.Size = new System.Drawing.Size(817, 684);
			this.tabSummary.TabIndex = 0;
			this.tabSummary.Text = "Summary";
			this.tabSummary.UseVisualStyleBackColor = true;
			// 
			// panelDescription
			// 
			this.panelDescription.Controls.Add(this.lblMangaDescription);
			this.panelDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelDescription.Location = new System.Drawing.Point(3, 269);
			this.panelDescription.Name = "panelDescription";
			this.panelDescription.Size = new System.Drawing.Size(811, 412);
			this.panelDescription.TabIndex = 4;
			// 
			// lblMangaDescription
			// 
			this.lblMangaDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblMangaDescription.Location = new System.Drawing.Point(0, 0);
			this.lblMangaDescription.Name = "lblMangaDescription";
			this.lblMangaDescription.Size = new System.Drawing.Size(811, 412);
			this.lblMangaDescription.TabIndex = 1;
			this.lblMangaDescription.Text = "Lorem ipsum magical description";
			// 
			// lblMangaStatus
			// 
			this.lblMangaStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMangaStatus.AutoSize = true;
			this.lblMangaStatus.Location = new System.Drawing.Point(428, 162);
			this.lblMangaStatus.Name = "lblMangaStatus";
			this.lblMangaStatus.Size = new System.Drawing.Size(245, 104);
			this.lblMangaStatus.TabIndex = 3;
			this.lblMangaStatus.Text = "Alternative name(s): 多分ワンピース、オタクがいじん\r\nChapters: 10\r\nStatus: Ongoing\r\n\r\nAuthor(s):" +
    " Some guy\r\nArtist(s): Some girl\r\n\r\nGenre(s): Action, Comedy, Slice of Life";
			// 
			// pictureBoxCoverImage
			// 
			this.pictureBoxCoverImage.BackColor = System.Drawing.SystemColors.Window;
			this.pictureBoxCoverImage.Location = new System.Drawing.Point(15, 7);
			this.pictureBoxCoverImage.Name = "pictureBoxCoverImage";
			this.pictureBoxCoverImage.Size = new System.Drawing.Size(407, 259);
			this.pictureBoxCoverImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxCoverImage.TabIndex = 2;
			this.pictureBoxCoverImage.TabStop = false;
			// 
			// lblMangaTitle
			// 
			this.lblMangaTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMangaTitle.AutoSize = true;
			this.lblMangaTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMangaTitle.Location = new System.Drawing.Point(428, 7);
			this.lblMangaTitle.MaximumSize = new System.Drawing.Size(400, 300);
			this.lblMangaTitle.Name = "lblMangaTitle";
			this.lblMangaTitle.Size = new System.Drawing.Size(381, 74);
			this.lblMangaTitle.TabIndex = 0;
			this.lblMangaTitle.Text = "Probably one piece you weeb";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.listChapters);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(817, 684);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Chapters";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// listChapters
			// 
			this.listChapters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnTitle});
			this.listChapters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listChapters.Location = new System.Drawing.Point(3, 3);
			this.listChapters.Name = "listChapters";
			this.listChapters.Size = new System.Drawing.Size(811, 678);
			this.listChapters.TabIndex = 0;
			this.listChapters.UseCompatibleStateImageBehavior = false;
			this.listChapters.View = System.Windows.Forms.View.Details;
			this.listChapters.ItemActivate += new System.EventHandler(this.listChapters_ItemActivate);
			// 
			// columnTitle
			// 
			this.columnTitle.Text = "Chapter";
			this.columnTitle.Width = 589;
			// 
			// infiniteReader
			// 
			this.infiniteReader.BackColor = System.Drawing.Color.White;
			this.infiniteReader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.infiniteReader.Location = new System.Drawing.Point(3, 3);
			this.infiniteReader.Name = "infiniteReader";
			this.infiniteReader.Size = new System.Drawing.Size(825, 710);
			this.infiniteReader.TabIndex = 5;
			this.infiniteReader.Text = "infiniteReader1";
			// 
			// tablessTabControl1
			// 
			this.tablessTabControl1.Controls.Add(this.screenManga);
			this.tablessTabControl1.Controls.Add(this.screenReader);
			this.tablessTabControl1.Controls.Add(this.screenBrowser);
			this.tablessTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tablessTabControl1.Location = new System.Drawing.Point(0, 0);
			this.tablessTabControl1.Name = "tablessTabControl1";
			this.tablessTabControl1.SelectedIndex = 0;
			this.tablessTabControl1.Size = new System.Drawing.Size(839, 742);
			this.tablessTabControl1.TabIndex = 7;
			// 
			// screenManga
			// 
			this.screenManga.Controls.Add(this.tabControl1);
			this.screenManga.Location = new System.Drawing.Point(4, 22);
			this.screenManga.Name = "screenManga";
			this.screenManga.Padding = new System.Windows.Forms.Padding(3);
			this.screenManga.Size = new System.Drawing.Size(831, 716);
			this.screenManga.TabIndex = 0;
			this.screenManga.Text = "Manga";
			this.screenManga.UseVisualStyleBackColor = true;
			// 
			// screenReader
			// 
			this.screenReader.Controls.Add(this.infiniteReader);
			this.screenReader.Location = new System.Drawing.Point(4, 22);
			this.screenReader.Name = "screenReader";
			this.screenReader.Padding = new System.Windows.Forms.Padding(3);
			this.screenReader.Size = new System.Drawing.Size(831, 716);
			this.screenReader.TabIndex = 1;
			this.screenReader.Text = "Reader";
			this.screenReader.UseVisualStyleBackColor = true;
			// 
			// screenBrowser
			// 
			this.screenBrowser.Controls.Add(this.button1);
			this.screenBrowser.Controls.Add(this.label2);
			this.screenBrowser.Controls.Add(this.label1);
			this.screenBrowser.Controls.Add(this.textBox1);
			this.screenBrowser.Location = new System.Drawing.Point(4, 22);
			this.screenBrowser.Name = "screenBrowser";
			this.screenBrowser.Padding = new System.Windows.Forms.Padding(3);
			this.screenBrowser.Size = new System.Drawing.Size(831, 716);
			this.screenBrowser.TabIndex = 2;
			this.screenBrowser.Text = "Browser";
			this.screenBrowser.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(388, 34);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Go";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 103);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(160, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Actual browser coming soon (tm)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(408, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Enter manga URL (Kissmanga, Mangahere, Mangareader and Funmanga supported):";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(8, 36);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(374, 20);
			this.textBox1.TabIndex = 0;
			// 
			// toastPanel1
			// 
			this.toastPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toastPanel1.Location = new System.Drawing.Point(0, 702);
			this.toastPanel1.Name = "toastPanel1";
			this.toastPanel1.Size = new System.Drawing.Size(839, 40);
			this.toastPanel1.TabIndex = 8;
			this.toastPanel1.Visible = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(839, 742);
			this.Controls.Add(this.toastPanel1);
			this.Controls.Add(this.tablessTabControl1);
			this.Icon = global::PotatoReader.Properties.Resources.icon3_3tV_icon;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = " ";
			this.tabControl1.ResumeLayout(false);
			this.tabSummary.ResumeLayout(false);
			this.tabSummary.PerformLayout();
			this.panelDescription.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxCoverImage)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tablessTabControl1.ResumeLayout(false);
			this.screenManga.ResumeLayout(false);
			this.screenReader.ResumeLayout(false);
			this.screenBrowser.ResumeLayout(false);
			this.screenBrowser.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabSummary;
		private System.Windows.Forms.Label lblMangaStatus;
		private System.Windows.Forms.PictureBox pictureBoxCoverImage;
		private System.Windows.Forms.Label lblMangaDescription;
		private System.Windows.Forms.Label lblMangaTitle;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView listChapters;
		private System.Windows.Forms.ColumnHeader columnTitle;
		private InfiniteReader infiniteReader;
		private TablessTabControl tablessTabControl1;
		private System.Windows.Forms.TabPage screenBrowser;
		private System.Windows.Forms.TabPage screenManga;
		private System.Windows.Forms.TabPage screenReader;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private ToastPanel toastPanel1;
		private System.Windows.Forms.Panel panelDescription;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
	}
}

