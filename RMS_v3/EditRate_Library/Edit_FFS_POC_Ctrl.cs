using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using RMS_BusinessObjects;

namespace EditRate_Library
{
	/// <summary>
	/// Summary description for Edit_FFS_POC.
	/// </summary>
	public class Edit_FFS_POC_Ctrl : Edit_Rate_Ctrl
	{
		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox rateBx;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_FFS_POC_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#region "Methods"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_FFS_POCBO caseRate = (Rate_FFS_POCBO) rateToLoad;

			rateBx.Text = Convert.ToString(caseRate.Rate);
		}

		public override RateBO getRate()
		{
			Rate_FFS_POCBO rateToReturn = new Rate_FFS_POCBO();

			rateToReturn.Rate = Convert.ToDouble(rateBx.Text);

			return rateToReturn;
		}

		public override void Clear()
		{
			rateBx.Text = "";
		}


		#endregion

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.rateBx = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label1.Size = new System.Drawing.Size(112, 14);
			this.label1.TabIndex = 67;
			this.label1.Text = "POC";
			// 
			// rateBx
			// 
			this.rateBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rateBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateBx.Location = new System.Drawing.Point(128, 8);
			this.rateBx.Name = "rateBx";
			this.rateBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.rateBx.Size = new System.Drawing.Size(76, 20);
			this.rateBx.TabIndex = 66;
			this.rateBx.Text = "0";
			// 
			// Edit_FFS_POC
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rateBx);
			this.Name = "Edit_FFS_POC";
			this.Size = new System.Drawing.Size(232, 40);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
