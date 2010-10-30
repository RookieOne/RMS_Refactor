using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using RMS_BusinessObjects;

namespace RateGrid_Library
{
	/// <summary>
	/// Summary description for InsurancePlansCovered_Ctrl.
	/// </summary>
	public class InsurancePlansCovered_Ctrl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label insurancePlansCoveredLbl;
		private System.Windows.Forms.Label insurancePlansCoveredTitleLbl;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private InsurancePlan_Collection insPlansCovered;

		public InsurancePlansCovered_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		public void setInsPlansCovered(InsurancePlan_Collection in_insPlansCovered)
		{
			insPlansCovered = in_insPlansCovered;

			this.loadCtrl();
		}

		private void loadCtrl()
		{
			string insPlansCoveredStr = "";
			InsurancePlanBO insPlan;

			for(int k=0; k < insPlansCovered.Count; k++)
			{
				insPlan = insPlansCovered.getInsurancePlanAt(k);
				insPlansCoveredStr += insPlan.InsurancePlanCode + " : " + insPlan.Description + ", ";
			}

			if (insPlansCoveredStr.Length >0)
			{	insPlansCoveredStr = insPlansCoveredStr.Substring(0, insPlansCoveredStr.Length - 2);	}

			insurancePlansCoveredLbl.Text = insPlansCoveredStr;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.insurancePlansCoveredLbl = new System.Windows.Forms.Label();
			this.insurancePlansCoveredTitleLbl = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// insurancePlansCoveredLbl
			// 
			this.insurancePlansCoveredLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.insurancePlansCoveredLbl.ForeColor = System.Drawing.Color.DimGray;
			this.insurancePlansCoveredLbl.Location = new System.Drawing.Point(0, 16);
			this.insurancePlansCoveredLbl.Name = "insurancePlansCoveredLbl";
			this.insurancePlansCoveredLbl.Size = new System.Drawing.Size(536, 32);
			this.insurancePlansCoveredLbl.TabIndex = 3;
			// 
			// insurancePlansCoveredTitleLbl
			// 
			this.insurancePlansCoveredTitleLbl.BackColor = System.Drawing.Color.Transparent;
			this.insurancePlansCoveredTitleLbl.Dock = System.Windows.Forms.DockStyle.Top;
			this.insurancePlansCoveredTitleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.insurancePlansCoveredTitleLbl.Location = new System.Drawing.Point(0, 0);
			this.insurancePlansCoveredTitleLbl.Name = "insurancePlansCoveredTitleLbl";
			this.insurancePlansCoveredTitleLbl.Size = new System.Drawing.Size(536, 16);
			this.insurancePlansCoveredTitleLbl.TabIndex = 2;
			this.insurancePlansCoveredTitleLbl.Text = "Insurance Plans Covered";
			// 
			// InsurancePlansCovered_Ctrl
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.insurancePlansCoveredLbl);
			this.Controls.Add(this.insurancePlansCoveredTitleLbl);
			this.Name = "InsurancePlansCovered_Ctrl";
			this.Size = new System.Drawing.Size(536, 48);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
