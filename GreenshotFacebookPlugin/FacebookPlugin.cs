/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2013  Thomas Braun, Jens Klingen, Robin Krom
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using System.ComponentModel.Composition;

namespace GreenshotFacebookPlugin {
	/// <summary>
	/// This is the GreenshotFacebookPlugin base code
	/// </summary>
	[Export(typeof(IGreenshotPlugin))]
	[ExportMetadata("name", "Facebook plug-in")]
	public class FacebookPlugin : AbstractPlugin {
		private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(FacebookPlugin));
		private static FacebookConfiguration config;
		private IGreenshotHost host;
		private ComponentResourceManager resources;
		private ToolStripMenuItem itemPlugInConfig;

		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (itemPlugInConfig != null) {
					itemPlugInConfig.Dispose();
					itemPlugInConfig = null;
				}
			}
		}

	    public override IEnumerable<IDestination> Destinations() {
			yield return new FacebookDestination(this, null);
		}
        
		/// <summary>
		/// Implementation of the IGreenshotPlugin.Initialize
		/// </summary>
		/// <param name="pluginHost">Use the IGreenshotPluginHost interface to register events</param>
		/// <param name="metadata">IDictionary&lt;string, object&gt;</param>
		/// <returns>true if plugin is initialized, false if not (doesn't show)</returns>
		public override bool Initialize(IGreenshotHost pluginHost, IDictionary<string, object> metadata) {
			host = pluginHost;

			// Get configuration
			config = IniConfig.GetIniSection<FacebookConfiguration>();
			resources = new ComponentResourceManager(typeof(FacebookPlugin));

            itemPlugInConfig = new ToolStripMenuItem(Language.GetString("facebook", LangKey.configure)) {Tag = host};
		    itemPlugInConfig.Click += delegate {
				config.ShowConfigDialog();
			};
			itemPlugInConfig.Image = (Image)resources.GetObject("Facebook");

			PluginUtils.AddToContextMenu(host, itemPlugInConfig);
			Language.LanguageChanged += OnLanguageChanged;
			return true;
		}

		public void OnLanguageChanged(object sender, EventArgs e) {
			if (itemPlugInConfig != null) {
                itemPlugInConfig.Text = Language.GetString("facebook", LangKey.configure);
			}
		}

		public override void Shutdown() {
			LOG.Debug("Facebook Plugin shutdown.");
			Language.LanguageChanged -= OnLanguageChanged;
		}

		/// <summary>
		/// Implementation of the IPlugin.Configure
		/// </summary>
		public override void Configure() {
			config.ShowConfigDialog();
		}
		
		/// <summary>
		/// Upload the capture to Facebook
		/// </summary>
		/// <param name="captureDetails"></param>
		/// <param name="surfaceToUpload">ISurface</param>
		/// <param name="uploadURL">out string for the url</param>
		/// <returns>true if the upload succeeded</returns>
		public bool Upload(ICaptureDetails captureDetails, ISurface surfaceToUpload, out string uploadURL) {
			SurfaceOutputSettings outputSettings = new SurfaceOutputSettings(OutputFormat.png);
			try {
				string filename = Path.GetFileName(FilenameHelper.GetFilename(OutputFormat.png, captureDetails));
				FacebookInfo FacebookInfo = null;
			
				// Run upload in the background
                new PleaseWaitForm().ShowAndWait("Facebook plug-in", Language.GetString("facebook", LangKey.communication_wait), 
					delegate {
						FacebookInfo = FacebookUtils.UploadToFacebook(surfaceToUpload, outputSettings, config.IncludeTitle ? captureDetails.Title : null, filename);
					}
				);
				// This causes an exeption if the upload failed :)
				LOG.DebugFormat("Uploaded to Facebook page: " + FacebookInfo.Page);
				uploadURL = null;
                
				try {
					uploadURL = FacebookInfo.Page;
					Clipboard.SetText(FacebookInfo.Page);
				} catch (Exception ex) {
					LOG.Error("Can't write to clipboard: ", ex);
				}
				return true;
			} catch (Exception e) {
				LOG.Error(e);
				MessageBox.Show(Language.GetString("facebook", LangKey.upload_failure) + " " + e.Message);
			}
			uploadURL = null;
			return false;
		}
	}
}
