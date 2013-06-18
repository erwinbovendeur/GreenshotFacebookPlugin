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
using Facebook;

namespace GreenshotFacebookPlugin
{
	/// <summary>
	/// Description of FacebookInfo.
	/// </summary>
	public class FacebookInfo {
		private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(FacebookInfo));

	    public string Page { get; set; }

	    /// <summary>
		/// Parse the upload response
		/// </summary>
		/// <param name="response">XML</param>
		/// <returns>FacebookInfo object</returns>
		public static FacebookInfo FromUploadResponse(JsonObject response)
		{
		    LOG.DebugFormat("Id: {0} - Post Id: {1}", response["id"], response["post_id"]);
			FacebookInfo FacebookInfo = new FacebookInfo
			    {
			        Page = string.Format("https://www.facebook.com/photo.php?fbid={0}", response["id"])
			    };
			return FacebookInfo;
		}
	}
}
