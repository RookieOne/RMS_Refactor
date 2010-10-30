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
	/// Summary description for Edit_Floor_PerDiem_Ctrl.
	/// </summary>
	public class Edit_Floor_PerDiem_Ctrl : Edit_Rate_Ctrl
	{
		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox floorBx;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_Floor_PerDiem_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#region "Methods"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_FloorBO floor = (Rate_FloorBO) rateToLoad;

			floorBx.Text = Convert.ToString(floor.Rate);
		}

		public override RateBO getRate()
		{
			Rate_FloorBO rateToReturn = new Rate_FloorBO();

			rateToReturn.Rate = Convert.ToDouble(floorBx.Text);

			return rateToReturn;
		}

		public override void Clear()
		{
			floorBx.Text = "";
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
			this.floorBx = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label1.Size = new System.Drawing.Size(112, 14);
			this.label1.TabIndex = 71;
			this.label1.Text = "Per Diem Floor";
			// 
			// floorBx
			// 
			this.floorBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.floorBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.floorBx.Location = new System.Drawing.Point(128, 8);
			this.floorBx.Name = "floorBx";
			this.floorBx.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.floorBx.Size = new System.Drawing.Size(76, 20);
			this.floorBx.TabIndex = 70;
			this.floorBx.Text = "0";
			// 
			// Edit_Floor_PerDiem_Ctrl
			// 
			this.Controls.Add(this.label1);
			this.Controls.Add(this.floorBx);
			this.Name = "Edit_Floor_PerDiem_Ctrl";
			this.Size = new System.Drawing.Size(240, 48);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
