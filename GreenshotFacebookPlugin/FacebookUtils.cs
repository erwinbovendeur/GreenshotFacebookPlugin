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
using Facebook;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Core;

namespace GreenshotFacebookPlugin {
	/// <summary>
	/// Description of FacebookUtils.
	/// </summary>
	public static class FacebookUtils {
		private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(FacebookUtils));
		private static readonly FacebookConfiguration config = IniConfig.GetIniSection<FacebookConfiguration>();
//		private static List<string> albumsCache = null;

        /// <summary>
        /// Helper method to create an OAuth session object for contacting the Photobucket API
        /// </summary>
        /// <returns>OAuthSession</returns>
        private static FacebookCredentials createSession(bool autoLogin)
        {
            FacebookCredentials oAuth = new FacebookCredentials();

            if (string.IsNullOrEmpty(config.Token))
            {
                if (!autoLogin)
                {
                    return null;
                }
                if (!oAuth.Authorize())
                {
                    return null;
                }
                if (!string.IsNullOrEmpty(oAuth.Token))
                {
                    config.Token = oAuth.Token;
                }
                IniConfig.Save();
            }
            oAuth.Token = config.Token;
            return oAuth;
        }

		/// <summary>
		/// Do the actual upload to Facebook
		/// </summary>
		/// <returns>FacebookResponse</returns>
		public static FacebookInfo UploadToFacebook(ISurface surfaceToUpload, SurfaceOutputSettings outputSettings, string title, string filename)
	    {
	        return UploadToFacebook(surfaceToUpload, outputSettings, title, filename, false);
	    }

	    private static FacebookInfo UploadToFacebook(ISurface surfaceToUpload, SurfaceOutputSettings outputSettings, string title, string filename, bool repeated) {
            FacebookCredentials oAuth = createSession(true);
            if (oAuth == null)
            {
                return null;
            }

            SurfaceContainer container = new SurfaceContainer(surfaceToUpload, outputSettings, filename);
		    var client = new FacebookClient(config.Token);
		    var img = new FacebookMediaObject {FileName = filename};
		    img.SetValue(container.ToByteArray());
		    img.ContentType = "multipart/form-data";

		    var param = new Dictionary<string, object> {{"attachment", img}};
		    if (!string.IsNullOrEmpty(title))
                param.Add("message", title);

            // Not on timeline?
            if (config.NotOnTimeline)
                param.Add("no_story", "1");

		    JsonObject result;
		    try
		    {
                result = (JsonObject) client.Post("me/photos", param);
            }
            catch (FacebookOAuthException ex)
            {
                if (!repeated && ex.ErrorCode == 190) // App no long authorized, reauthorize if possible
                {
                    config.Token = null;
                    return UploadToFacebook(surfaceToUpload, outputSettings, title, filename, true);
                }
                throw;
            }
            catch (Exception ex)
            {
				LOG.Error("Error uploading to Facebook.", ex);
				throw;
			}
        
            FacebookInfo facebookInfo = FacebookInfo.FromUploadResponse(result);
            LOG.Debug("Upload to Facebook was finished");
            return facebookInfo;
        }
	}
}
