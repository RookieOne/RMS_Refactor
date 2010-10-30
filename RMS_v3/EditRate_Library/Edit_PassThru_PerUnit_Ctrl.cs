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
	/// Summary description for Edit_PassThru_PerType.
	/// </summary>
	public class Edit_PassThru_PerUnit_Ctrl : Edit_Rate_Ctrl
	{
		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox rateBx;
		internal System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox thresholdBx;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_PassThru_PerUnit_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#region "Methods"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_PassThruBO passThruRate = (Rate_PassThruBO) rateToLoad;

			rateBx.Text = Convert.ToString(passThruRate.Rate);
			thresholdBx.Text = Convert.ToString(passThruRate.Threshold);
		}

		public override RateBO getRate()
		{
			Rate_PassThruBO rateToReturn = new Rate_PassThruBO();

			rateToReturn.Rate = Convert.ToDouble(rateBx.Text);
			rateToReturn.Threshold = Convert.ToDouble(thresholdBx.Text);

			return rateToReturn;
		}

		public override void Clear()
		{
			rateBx.Text = "";
			thresholdBx.Text = "";
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
			this.label2 = new System.Windows.Forms.Label();
			this.thresholdBx = new System.Windows.Forms.TextBox();
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
			this.label1.Text = "Unit Rate";
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
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label2.Size = new System.Drawing.Size(112, 14);
			this.label2.TabIndex = 73;
			this.label2.Text = "Threshold";
			// 
			// thresholdBx
			// 
			this.thresholdBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.thresholdBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.thresholdBx.Location = new System.Drawing.Point(128, 32);
			this.thresholdBx.Name = "thresholdBx";
			this.thresholdBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.thresholdBx.Size = new System.Drawing.Size(76, 20);
			this.thresholdBx.TabIndex = 72;
			this.thresholdBx.Text = "0";
			// 
			// Edit_PassThru_PerType
			// 
			this.Controls.Add(this.label2);
			this.Controls.Add(this.thresholdBx);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rateBx);
			this.Name = "Edit_PassThru_PerType";
			this.Size = new System.Drawing.Size(232, 64);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
