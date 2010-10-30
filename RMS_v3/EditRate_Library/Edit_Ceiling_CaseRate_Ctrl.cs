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
	/// Summary description for Edit_Ceiling_CaseRate_Ctrl.
	/// </summary>
	public class Edit_Ceiling_CaseRate_Ctrl : Edit_Rate_Ctrl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox ceilingBx;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Edit_Ceiling_CaseRate_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#region "Methods"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_CeilingBO ceiling = (Rate_CeilingBO) rateToLoad;

			ceilingBx.Text = Convert.ToString(ceiling.Rate);
		}

		public override RateBO getRate()
		{
			Rate_CeilingBO rateToReturn = new Rate_CeilingBO();

			rateToReturn.Rate = Convert.ToDouble(ceilingBx.Text);

			return rateToReturn;
		}

		public override void Clear()
		{
			ceilingBx.Text = "";
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
			this.ceilingBx = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Ceiling Amount";
			// 
			// ceilingBx
			// 
			this.ceilingBx.Location = new System.Drawing.Point(96, 8);
			this.ceilingBx.Name = "ceilingBx";
			this.ceilingBx.Size = new System.Drawing.Size(80, 20);
			this.ceilingBx.TabIndex = 1;
			this.ceilingBx.Text = "0";
			this.ceilingBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// Edit_Ceiling_CaseRate_Ctrl
			// 
			this.Controls.Add(this.ceilingBx);
			this.Controls.Add(this.label1);
			this.Name = "Edit_Ceiling_CaseRate_Ctrl";
			this.Size = new System.Drawing.Size(200, 48);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
