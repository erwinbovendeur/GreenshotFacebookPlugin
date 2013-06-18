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
using System.ComponentModel;
using System.Drawing;
using Greenshot.Plugin;
using GreenshotPlugin.Core;

namespace GreenshotFacebookPlugin  {
	/// <summary>
	/// Description of FacebookDestination.
	/// </summary>
	public class FacebookDestination : AbstractDestination {
	    private readonly FacebookPlugin plugin;
		private readonly string albumPath;

		/// <summary>
		/// Create a Facebook destination, which also has the path to the album in it
		/// </summary>
		/// <param name="plugin"></param>
		/// <param name="albumPath">path to the album, null for default</param>
		public FacebookDestination(FacebookPlugin plugin, string albumPath) {
			this.plugin = plugin;
			this.albumPath = albumPath;
		}
		
		public override string Designation {
			get {
				return "Facebook";
			}
		}

		public override string Description {
			get {
				if (albumPath != null) {
					return albumPath;
				}
                return Language.GetString("facebook", LangKey.upload_menu_item);
			}
		}

		public override Image DisplayIcon {
			get {
				ComponentResourceManager resources = new ComponentResourceManager(typeof(FacebookPlugin));
				return (Image)resources.GetObject("Facebook");
			}
		}
		/*
		public override bool isDynamic {
			get {
				return true;
			}
		}
        
		public override IEnumerable<IDestination> DynamicDestinations() {
			List<string> albums = null;
			try {
				//albums = FacebookUtils.RetrieveFacebookAlbums();
			} catch {
			}

			if (albums == null || albums.Count == 0) {
				yield break;
			}
			foreach (string album in albums) {
				yield return new FacebookDestination(plugin, album);
			}
		}
        */
		/// <summary>
		/// Export the capture to Facebook
		/// </summary>
		/// <param name="manuallyInitiated"></param>
		/// <param name="surface"></param>
		/// <param name="captureDetails"></param>
		/// <returns></returns>
		public override ExportInformation ExportCapture(bool manuallyInitiated, ISurface surface, ICaptureDetails captureDetails) {
			ExportInformation exportInformation = new ExportInformation(Designation, Description);
			string uploadURL;
			bool uploaded = plugin.Upload(captureDetails, surface, out uploadURL);
			if (uploaded) {
				exportInformation.ExportMade = true;
				exportInformation.Uri = uploadURL;
			}
			ProcessExport(exportInformation, surface);
			return exportInformation;
		}
	}
}
