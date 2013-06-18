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
using System.Windows.Forms;
using Greenshot.IniFile;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;

namespace GreenshotFacebookPlugin {
	/// <summary>
	/// Description of FacebookConfiguration.
	/// </summary>
	[IniSection("Facebook", Description="Greenshot Facebook Plugin configuration")]
	public class FacebookConfiguration : IniSection {
		[IniProperty("Token", Description = "The Facebook token", Encrypted=true, ExcludeIfNull=true)]
		public string Token;

	    [IniProperty("Name", Description = "The name on facebook", ExcludeIfNull = true)] 
        public string Name;

	    [IniProperty("NotOnTimeline", Description = "Do not show the image on my timeline")] 
        public bool NotOnTimeline;

	    [IniProperty("IncludeTitle", Description = "Whether or not to include the capture title as description")] 
        public bool IncludeTitle;

	    /// <summary>
		/// A form for username/password
		/// </summary>
		/// <returns>bool true if OK was pressed, false if cancel</returns>
		public bool ShowConfigDialog() {
			SettingsForm settingsForm = null;

			new PleaseWaitForm().ShowAndWait("Facebook plug-in", Language.GetString("facebook", LangKey.communication_wait), 
				delegate {
					settingsForm = new SettingsForm(this);
				}
			);
			DialogResult result = settingsForm.ShowDialog();
			if (result == DialogResult.OK) {
				return true;
			}
			return false;
		}
	}
}
