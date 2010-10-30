using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using RMS_BusinessObjects;
using RMS_DALObjects;

namespace EditRate_Library
{
	/// <summary>
	/// Summary description for Edit_BaseRate.
	/// </summary>
	public class Edit_BaseRate_Ctrl : Edit_Rate_Ctrl
	{

		#region "Variables"

		internal System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox rateBx;
		internal System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox drgWgtComboBx;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		CodesDAL codesData;


		#endregion

		#region "Constructors"

		public Edit_BaseRate_Ctrl()
		{
			InitializeComponent();	// This call is required by the Windows.Forms Form Designer.

			codesData = new CodesDAL();

			setupWgtTableComboBx();
		}

		private void setupWgtTableComboBx()
		{
			ArrayList wgtList = codesData.getWeightTables();

			drgWgtComboBx.Items.Clear();
			for(int k=0; k<wgtList.Count; k++)
			{
				drgWgtComboBx.Items.Add((WeightTableStruct) wgtList[k]);
			}

		}

		#endregion

	 #region "Overrides"

		public override void loadRate(RateBO rateToLoad)
		{
			Rate_BaseRateBO baseRate = (Rate_BaseRateBO) rateToLoad;

			rateBx.Text = Convert.ToString(baseRate.Rate);

			WeightTableStruct weightTable;
			for(int k=0; k<drgWgtComboBx.Items.Count; k++)
			{
				weightTable = (WeightTableStruct) drgWgtComboBx.Items[k];

				if (weightTable.TableID == baseRate.WeightTable.TableID)
				{	drgWgtComboBx.SelectedIndex = k;	}
			}
		}

		public override RateBO getRate()
		{
			Rate_BaseRateBO rateToReturn = new Rate_BaseRateBO();

			rateToReturn.Rate = Convert.ToDouble(rateBx.Text);

			rateToReturn.WeightTable = (WeightTableStruct) drgWgtComboBx.Items[drgWgtComboBx.SelectedIndex];

			return rateToReturn;
		}

		public override void Clear()
		{
			rateBx.Text = "";
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

		#endregion

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
			this.drgWgtComboBx = new System.Windows.Forms.ComboBox();
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
			this.label1.Text = "Base Rate";
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
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.label2.Size = new System.Drawing.Size(112, 14);
			this.label2.TabIndex = 68;
			this.label2.Text = "DRG Weight Table";
			// 
			// drgWgtComboBx
			// 
			this.drgWgtComboBx.Location = new System.Drawing.Point(128, 40);
			this.drgWgtComboBx.Name = "drgWgtComboBx";
			this.drgWgtComboBx.Size = new System.Drawing.Size(240, 21);
			this.drgWgtComboBx.TabIndex = 69;
			// 
			// Edit_BaseRate
			// 
			this.Controls.Add(this.drgWgtComboBx);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rateBx);
			this.Name = "Edit_BaseRate";
			this.Size = new System.Drawing.Size(384, 80);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
