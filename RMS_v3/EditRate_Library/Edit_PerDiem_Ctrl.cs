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
	/// Summary description for Edit_PerDiem.
	/// </summary>
	public class Edit_PerDiem_Ctrl : Edit_Rate_Ctrl
	{
		internal System.Windows.Forms.TextBox rateBx;
		internal System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_PerDiem_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#region "Methods"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_PerDiemBO perDiem = (Rate_PerDiemBO) rateToLoad;

			rateBx.Text = Convert.ToString(perDiem.Rate);
		}

		public override RateBO getRate()
		{
			Rate_PerDiemBO rateToReturn = new Rate_PerDiemBO();

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
			this.rateBx = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// rateBx
			// 
			this.rateBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rateBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateBx.Location = new System.Drawing.Point(128, 8);
			this.rateBx.Name = "rateBx";
			this.rateBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.rateBx.Size = new System.Drawing.Size(76, 20);
			this.rateBx.TabIndex = 60;
			this.rateBx.Text = "0";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label1.Size = new System.Drawing.Size(112, 14);
			this.label1.TabIndex = 65;
			this.label1.Text = "Per Diem Rate";
			// 
			// Edit_PerDiem
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rateBx);
			this.Name = "Edit_PerDiem";
			this.Size = new System.Drawing.Size(224, 40);
			this.ResumeLayout(false);

		}
		#endregion


	}
}
