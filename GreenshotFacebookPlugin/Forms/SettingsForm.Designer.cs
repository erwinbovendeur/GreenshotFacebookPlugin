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
namespace GreenshotFacebookPlugin {
	partial class SettingsForm {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonOK = new GreenshotPlugin.Controls.GreenshotButton();
            this.buttonCancel = new GreenshotPlugin.Controls.GreenshotButton();
            this.checkbox_notontimeline = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.checkbox_includetitle = new GreenshotPlugin.Controls.GreenshotCheckBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.LanguageKey = "OK";
            this.buttonOK.Location = new System.Drawing.Point(278, 88);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(94, 29);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "Ok";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.LanguageKey = "CANCEL";
            this.buttonCancel.Location = new System.Drawing.Point(379, 88);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // checkbox_notontimeline
            // 
            this.checkbox_notontimeline.AutoSize = true;
            this.checkbox_notontimeline.LanguageKey = "facebook.not_on_timeline";
            this.checkbox_notontimeline.Location = new System.Drawing.Point(13, 13);
            this.checkbox_notontimeline.Margin = new System.Windows.Forms.Padding(4);
            this.checkbox_notontimeline.Name = "checkbox_notontimeline";
            this.checkbox_notontimeline.PropertyName = "";
            this.checkbox_notontimeline.SectionName = "Facebook";
            this.checkbox_notontimeline.Size = new System.Drawing.Size(270, 21);
            this.checkbox_notontimeline.TabIndex = 13;
            this.checkbox_notontimeline.Text = "Do not post the images on my timeline";
            this.checkbox_notontimeline.UseVisualStyleBackColor = true;
            // 
            // checkbox_includetitle
            // 
            this.checkbox_includetitle.AutoSize = true;
            this.checkbox_includetitle.LanguageKey = "facebook.include_title";
            this.checkbox_includetitle.Location = new System.Drawing.Point(13, 42);
            this.checkbox_includetitle.Margin = new System.Windows.Forms.Padding(4);
            this.checkbox_includetitle.Name = "checkbox_includetitle";
            this.checkbox_includetitle.PropertyName = "";
            this.checkbox_includetitle.SectionName = "Facebook";
            this.checkbox_includetitle.Size = new System.Drawing.Size(269, 21);
            this.checkbox_includetitle.TabIndex = 14;
            this.checkbox_includetitle.Text = "Include the capture title as description";
            this.checkbox_includetitle.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(484, 136);
            this.Controls.Add(this.checkbox_includetitle);
            this.Controls.Add(this.checkbox_notontimeline);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LanguageKey = "facebook.settings_title";
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Facebook settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		private GreenshotPlugin.Controls.GreenshotButton buttonCancel;
		private GreenshotPlugin.Controls.GreenshotButton buttonOK;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_notontimeline;
        private GreenshotPlugin.Controls.GreenshotCheckBox checkbox_includetitle;
	}
}
