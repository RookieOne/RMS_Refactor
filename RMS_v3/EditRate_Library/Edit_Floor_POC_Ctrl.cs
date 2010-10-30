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
	/// Summary description for Edit_Floor_POC_Ctrl.
	/// </summary>
	public class Edit_Floor_POC_Ctrl : Edit_Rate_Ctrl
	{
		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox pocBx;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_Floor_POC_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#region "Methods"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_FloorBO floor = (Rate_FloorBO) rateToLoad;

			pocBx.Text = Convert.ToString(floor.Rate);
		}

		public override RateBO getRate()
		{
			Rate_FloorBO rateToReturn = new Rate_FloorBO();

			rateToReturn.Rate = Convert.ToDouble(pocBx.Text);

			return rateToReturn;
		}

		public override void Clear()
		{
			pocBx.Text = "";
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
			this.pocBx = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label1.Size = new System.Drawing.Size(112, 14);
			this.label1.TabIndex = 69;
			this.label1.Text = "Percent of Charges";
			// 
			// pocBx
			// 
			this.pocBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pocBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pocBx.Location = new System.Drawing.Point(128, 8);
			this.pocBx.Name = "pocBx";
			this.pocBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.pocBx.Size = new System.Drawing.Size(76, 20);
			this.pocBx.TabIndex = 68;
			this.pocBx.Text = "0";
			// 
			// Edit_Floor_POC_Ctrl
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pocBx);
			this.Name = "Edit_Floor_POC_Ctrl";
			this.Size = new System.Drawing.Size(496, 150);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
