using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using RMSCtrl_Library;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for ManageOtherDataForm.
	/// </summary>
	public class ManageOtherDataForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox tableNameComboBx;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ManageOtherDataForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ImportData_Control importDataCtrl = new ImportData_Control();
			importDataCtrl.Dock = DockStyle.Fill;

			mainPanel.Controls.Add(importDataCtrl);

			loadTableNames();
		}

		#region "Methods"

		private void loadTableNames()
		{
			tableNameComboBx.Items.Add("Dashboard");
		}

		#endregion

		#region "Overrides"

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

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ManageOtherDataForm));
			this.topPanel = new System.Windows.Forms.Panel();
			this.tableNameComboBx = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.topPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// topPanel
			// 
			this.topPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.topPanel.Controls.Add(this.tableNameComboBx);
			this.topPanel.Controls.Add(this.label1);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(792, 56);
			this.topPanel.TabIndex = 0;
			// 
			// tableNameComboBx
			// 
			this.tableNameComboBx.Location = new System.Drawing.Point(88, 16);
			this.tableNameComboBx.Name = "tableNameComboBx";
			this.tableNameComboBx.Size = new System.Drawing.Size(304, 21);
			this.tableNameComboBx.TabIndex = 1;
			this.tableNameComboBx.SelectedIndexChanged += new System.EventHandler(this.tableNameComboBx_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Table Name";
			// 
			// mainPanel
			// 
			this.mainPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 56);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(792, 374);
			this.mainPanel.TabIndex = 1;
			// 
			// ManageOtherDataForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(792, 430);
			this.Controls.Add(this.mainPanel);
			this.Controls.Add(this.topPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ManageOtherDataForm";
			this.Text = "Other Data Manager";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.topPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void tableNameComboBx_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ImportData_Control importDataCtrl = (ImportData_Control) mainPanel.Controls[0];
			importDataCtrl.DestinationTableName = tableNameComboBx.Items[tableNameComboBx.SelectedIndex].ToString();
		}
	}
}
