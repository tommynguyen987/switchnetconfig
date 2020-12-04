using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SwitchNetConfig
{
	/// <summary>
	/// Shows progress and status of process while applying profile
	/// </summary>
	public class ApplySettingDialog : System.Windows.Forms.Form
	{
		#region Designer

		private System.Windows.Forms.Label lblProfile;
		private System.Windows.Forms.Label lblProfileName;
		private System.Windows.Forms.TextBox txtStatus;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ApplySettingDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblProfile = new System.Windows.Forms.Label();
			this.lblProfileName = new System.Windows.Forms.Label();
			this.txtStatus = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblProfile
			// 
			this.lblProfile.AutoSize = true;
			this.lblProfile.Location = new System.Drawing.Point(8, 8);
			this.lblProfile.Name = "lblProfile";
			this.lblProfile.Size = new System.Drawing.Size(72, 17);
			this.lblProfile.TabIndex = 0;
			this.lblProfile.Text = "Profile Name:";
			// 
			// lblProfileName
			// 
			this.lblProfileName.AutoSize = true;
			this.lblProfileName.Location = new System.Drawing.Point(88, 8);
			this.lblProfileName.Name = "lblProfileName";
			this.lblProfileName.Size = new System.Drawing.Size(77, 17);
			this.lblProfileName.TabIndex = 1;
			this.lblProfileName.Text = "[Profile Name]";
			// 
			// txtStatus
			// 
			this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtStatus.Location = new System.Drawing.Point(8, 24);
			this.txtStatus.Multiline = true;
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtStatus.Size = new System.Drawing.Size(472, 240);
			this.txtStatus.TabIndex = 2;
			this.txtStatus.Text = "";
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnClose.Location = new System.Drawing.Point(304, 272);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(176, 23);
			this.btnClose.TabIndex = 7;
			this.btnClose.Text = "&Close and Exit";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnOK.Location = new System.Drawing.Point(240, 272);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(56, 23);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// ApplySettingDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(488, 301);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtStatus);
			this.Controls.Add(this.lblProfileName);
			this.Controls.Add(this.lblProfile);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ApplySettingDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Apply Profile";
			this.ResumeLayout(false);

		}
		#endregion

		#endregion

		internal void ApplySetting( Profile profile )
		{
			lblProfileName.Text = profile.Name;

			// Invole the profile manager to apply the profile
			ProfileManager manager = new ProfileManager( profile );
			manager.OnStatusUpdate += new StatusUpdate(manager_OnStatusUpdate);
			manager.Run( );

			btnOK.Enabled = true;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
			Application.Exit();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();			
		}

		/// <summary>
		/// Callback function for ProfileManager to show messages
		/// </summary>
		/// <param name="message"></param>
		private void manager_OnStatusUpdate(string message)
		{
			txtStatus.AppendText( message );
		}
	}
}
