using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GreenshotFacebookPlugin
{
    public partial class AuthorizeForm : FacebookForm
    {
        public AuthorizeForm()
        {
            InitializeComponent();
            Icon = GreenshotPlugin.Core.GreenshotResources.getGreenshotIcon();
        }

        private void AuthorizeForm_Load(object sender, System.EventArgs e)
        {
            var destinationURL = string.Format(
                @"https://www.facebook.com/dialog/oauth?client_id={0}&scope={1}&display=popup&redirect_uri=http://www.facebook.com/connect/login_success.html&response_type=token",
                FacebookCredentials.ConsumerKey, "publish_stream,offline_access,publish_actions");
            webBrowser.Navigated += WebBrowserNavigated;
            webBrowser.Navigate(destinationURL);
        }

        private void WebBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var url = e.Url.Fragment;
            if (!url.Contains("access_token") || !url.Contains("#"))
                return;

            Hide();
            url = (new Regex("#")).Replace(url, "?", 1);
            var parms = GetParams(url);
            if (parms.ContainsKey("access_token"))
            {
                AccessToken = parms["access_token"];
                DialogResult = DialogResult.OK;
            }
            else
                DialogResult = DialogResult.Cancel;
        }

        private static Dictionary<string, string> GetParams(string uri)
        {
            var matches = Regex.Matches(uri, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
            return matches.Cast<Match>().ToDictionary(
                m => Uri.UnescapeDataString(m.Groups[2].Value),
                m => Uri.UnescapeDataString(m.Groups[3].Value)
            );
        }

        public string AccessToken { get; private set; }
    }
}
