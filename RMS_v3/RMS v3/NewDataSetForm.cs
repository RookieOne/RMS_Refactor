using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;

using RMS_DALObjects;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for NewDataSetForm.
	/// </summary>
	public class NewDataSetForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button setBtn;
		private System.Windows.Forms.TextBox datasetNameBx;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region "Variables"

		baseDALObject data = new baseDALObject();

		public event EventHandler NewDataSet;

		#endregion


		public NewDataSetForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

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
			this.label1 = new System.Windows.Forms.Label();
			this.setBtn = new System.Windows.Forms.Button();
			this.datasetNameBx = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "DataSet Name";
			// 
			// setBtn
			// 
			this.setBtn.Location = new System.Drawing.Point(296, 4);
			this.setBtn.Name = "setBtn";
			this.setBtn.TabIndex = 4;
			this.setBtn.Text = "Add";
			this.setBtn.Click += new System.EventHandler(this.setBtn_Click);
			// 
			// datasetNameBx
			// 
			this.datasetNameBx.Location = new System.Drawing.Point(92, 4);
			this.datasetNameBx.Name = "datasetNameBx";
			this.datasetNameBx.Size = new System.Drawing.Size(200, 20);
			this.datasetNameBx.TabIndex = 3;
			this.datasetNameBx.Text = "";
			// 
			// NewDataSetForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(396, 38);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.setBtn);
			this.Controls.Add(this.datasetNameBx);
			this.Name = "NewDataSetForm";
			this.Text = "NewDataSetForm";
			this.ResumeLayout(false);

		}
		#endregion

		private void setBtn_Click(object sender, System.EventArgs e)
		{
			SqlParameter[] sqlParams = data.getParameters("UpdateDataset");

			sqlParams[1].Value = datasetNameBx.Text;	// DatasetName
			sqlParams[2].Value = DateTime.Today; // Pulled Date
			sqlParams[3].Value = DateTime.Today; // Start Date
			sqlParams[4].Value = DateTime.Today; // End Date

			data.executeUpdate("UpdateDataset", sqlParams);

			NewDataSet(this, EventArgs.Empty);
		}
	}
}
