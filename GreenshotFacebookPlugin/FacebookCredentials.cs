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

namespace GreenshotFacebookPlugin {
	/// <summary>
	/// This class is merely a placeholder for the file keeping the API key and secret for Facebook integration.
	/// Copy this file to FacebookCredentials.private.cs and fill in valid credentials. (Or empty strings, but of course you won't be able to use the Facebook plugin then.)
	/// </summary>
	public class FacebookCredentials {
        public static string ConsumerKey = "295187153948976";

        private static readonly FacebookConfiguration config = IniConfig.GetIniSection<FacebookConfiguration>();

	    public string getAuthorizationToken()
	    {
	        AuthorizeForm oAuthLoginForm = new AuthorizeForm();
            if (oAuthLoginForm.ShowDialog() == DialogResult.OK)
	        {
	            if (!string.IsNullOrEmpty(oAuthLoginForm.AccessToken))
	            {
                    Token = oAuthLoginForm.AccessToken;
	                return Token;
	            }
	        }
	        return null;
	    }

        public string Token { get; set; }

	    public bool Authorize()
	    {
	        if (string.IsNullOrEmpty(config.Token))
	        {
	            getAuthorizationToken();
	        }
	        return !string.IsNullOrEmpty(Token);
	    }
	}
}
