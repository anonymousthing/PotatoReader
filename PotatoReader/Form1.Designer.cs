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
			this.infiniteReader = new PotatoReader.InfiniteReader();
			this.panelReader = new System.Windows.Forms.Panel();
			this.panelManga = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabSummary = new System.Windows.Forms.TabPage();
			this.lblMangaStatus = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblMangaDescription = new System.Windows.Forms.Label();
			this.lblMangaTitle = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.listChapters = new System.Windows.Forms.ListView();
			this.columnTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panelBrowser = new System.Windows.Forms.Panel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.menuItemOpenUrl = new System.Windows.Forms.MenuItem();
			this.panelReader.SuspendLayout();
			this.panelManga.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabSummary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// infiniteReader
			// 
			this.infiniteReader.BackColor = System.Drawing.Color.White;
			this.infiniteReader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.infiniteReader.Location = new System.Drawing.Point(0, 0);
			this.infiniteReader.Name = "infiniteReader";
			this.infiniteReader.Size = new System.Drawing.Size(839, 742);
			this.infiniteReader.TabIndex = 0;
			this.infiniteReader.Text = "infiniteReader1";
			// 
			// panelReader
			// 
			this.panelReader.Controls.Add(this.infiniteReader);
			this.panelReader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelReader.Location = new System.Drawing.Point(0, 0);
			this.panelReader.Name = "panelReader";
			this.panelReader.Size = new System.Drawing.Size(839, 742);
			this.panelReader.TabIndex = 2;
			// 
			// panelManga
			// 
			this.panelManga.Controls.Add(this.statusStrip1);
			this.panelManga.Controls.Add(this.tabControl1);
			this.panelManga.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelManga.Location = new System.Drawing.Point(0, 0);
			this.panelManga.Name = "panelManga";
			this.panelManga.Size = new System.Drawing.Size(839, 742);
			this.panelManga.TabIndex = 1;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 720);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(839, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabSummary);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(839, 742);
			this.tabControl1.TabIndex = 3;
			// 
			// tabSummary
			// 
			this.tabSummary.Controls.Add(this.lblMangaStatus);
			this.tabSummary.Controls.Add(this.pictureBox1);
			this.tabSummary.Controls.Add(this.lblMangaDescription);
			this.tabSummary.Controls.Add(this.lblMangaTitle);
			this.tabSummary.Location = new System.Drawing.Point(4, 22);
			this.tabSummary.Name = "tabSummary";
			this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
			this.tabSummary.Size = new System.Drawing.Size(831, 716);
			this.tabSummary.TabIndex = 0;
			this.tabSummary.Text = "Summary";
			this.tabSummary.UseVisualStyleBackColor = true;
			// 
			// lblMangaStatus
			// 
			this.lblMangaStatus.AutoSize = true;
			this.lblMangaStatus.Location = new System.Drawing.Point(449, 99);
			this.lblMangaStatus.Name = "lblMangaStatus";
			this.lblMangaStatus.Size = new System.Drawing.Size(245, 104);
			this.lblMangaStatus.TabIndex = 3;
			this.lblMangaStatus.Text = "Alternative name(s): 多分ワンピース、オタクがいじん\r\nChapters: 10\r\nStatus: Ongoing\r\n\r\nAuthor(s):" +
    " Some guy\r\nArtist(s): Some girl\r\n\r\nGenre(s): Action, Comedy, Slice of Life";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Maroon;
			this.pictureBox1.Location = new System.Drawing.Point(15, 7);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(275, 214);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// lblMangaDescription
			// 
			this.lblMangaDescription.AutoSize = true;
			this.lblMangaDescription.Location = new System.Drawing.Point(12, 276);
			this.lblMangaDescription.Name = "lblMangaDescription";
			this.lblMangaDescription.Size = new System.Drawing.Size(159, 13);
			this.lblMangaDescription.TabIndex = 1;
			this.lblMangaDescription.Text = "Lorem ipsum magical description";
			// 
			// lblMangaTitle
			// 
			this.lblMangaTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMangaTitle.AutoSize = true;
			this.lblMangaTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMangaTitle.Location = new System.Drawing.Point(442, 7);
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
			this.tabPage2.Size = new System.Drawing.Size(831, 716);
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
			this.listChapters.Size = new System.Drawing.Size(825, 710);
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
			// panelBrowser
			// 
			this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBrowser.Location = new System.Drawing.Point(0, 0);
			this.panelBrowser.Name = "panelBrowser";
			this.panelBrowser.Size = new System.Drawing.Size(839, 742);
			this.panelBrowser.TabIndex = 3;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile});
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 0;
			this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOpenUrl});
			this.menuItemFile.Text = "File";
			// 
			// menuItemOpenUrl
			// 
			this.menuItemOpenUrl.Index = 0;
			this.menuItemOpenUrl.Text = "Open URL...";
			this.menuItemOpenUrl.Click += new System.EventHandler(this.menuItemOpenUrl_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(839, 742);
			this.Controls.Add(this.panelManga);
			this.Controls.Add(this.panelBrowser);
			this.Controls.Add(this.panelReader);
			this.Icon = global::PotatoReader.Properties.Resources.icon3_3tV_icon;
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "PotatoReader";
			this.panelReader.ResumeLayout(false);
			this.panelManga.ResumeLayout(false);
			this.panelManga.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabSummary.ResumeLayout(false);
			this.tabSummary.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private InfiniteReader infiniteReader;
		private System.Windows.Forms.Panel panelReader;
		private System.Windows.Forms.Panel panelManga;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabSummary;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView listChapters;
		private System.Windows.Forms.ColumnHeader columnTitle;
		private System.Windows.Forms.Panel panelBrowser;
		private System.Windows.Forms.Label lblMangaTitle;
		private System.Windows.Forms.Label lblMangaDescription;
		private System.Windows.Forms.Label lblMangaStatus;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemOpenUrl;
	}
}

