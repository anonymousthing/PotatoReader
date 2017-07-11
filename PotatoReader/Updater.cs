using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotatoReader
{
	class Updater
	{
		const string versionUrl = "https://fivepotato.es/potatoreader/version.txt";
		const string exeUrl = "https://fivepotato.es/potatoreader/PotatoReader.exe";
		static string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		public static async Task<bool> CheckVersion()
		{
			string version = await new HttpClient().GetStringAsync(versionUrl);

			//Same version
			if (version.Trim() == currentVersion)
				return false;

			var latestParts = version.Trim().Split(new char[] { '.' });
			var ourParts = currentVersion.Split(new char[] { '.' });

			//Must be really out of date if we've changed versioning schemes...
			if (latestParts.Length != ourParts.Length)
				return true;

			//Compare sub version numbers
			for (int i = 0; i < latestParts.Length; i++)
			{
				int latestVers;
				int ourVers;
				if (!int.TryParse(latestParts[i], out latestVers))
					return true;
				if (!int.TryParse(ourParts[i], out ourVers))
					return true;

				if (latestVers == ourVers)
					continue;
				else
					return latestVers > ourVers;
			}

			return false;
		}

		public static async Task UpdateToNewVersion(DownloadProgressChangedEventHandler dpceh, System.ComponentModel.AsyncCompletedEventHandler aceh)
		{
			var downloadPath = Path.GetTempFileName();
			WebClient wc = new WebClient();
			wc.DownloadProgressChanged += dpceh;
			wc.DownloadFileCompleted += aceh;
			await wc.DownloadFileTaskAsync(exeUrl, downloadPath);

			//Rename current executable as backup
			try
			{
				//Wait for a second before restarting
				await Task.Delay(1000);

				string exeName = Process.GetCurrentProcess().ProcessName;
				File.Delete(exeName + ".bak");
				File.Move(exeName + ".exe", exeName + ".bak");
				File.Move(downloadPath, exeName + ".exe");

				Process.Start(exeName + ".exe");
				Environment.Exit(0);
			}
			catch (Exception e)
			{
				MessageBox.Show("Unable to replace with updated executable. Check whether the executable is marked as read-only, or whether it is in a protected folder that requires administrative rights.");
			}
		}
	}
}
