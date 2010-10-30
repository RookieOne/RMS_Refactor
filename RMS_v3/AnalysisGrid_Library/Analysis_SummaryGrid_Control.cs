using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace AnalysisGrid_Library
{
	/// <summary>
	/// Summary description for Analysis_SummaryGrid_Control.
	/// </summary>
	public class Analysis_SummaryGrid_Control : Analysis_ParentGrid
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region "Variables"

		DataTable dTable;

		#endregion

		public Analysis_SummaryGrid_Control()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			setupGrid();
			setupDataTable();
		}

		#region "Methods"

		private void setupGrid()
		{
      DataGridTableStyle tStyle = this.AnalysisGrid.TableStyles[0];

			DataGridTextBoxColumn bc;

			tStyle.GridColumnStyles.Add(this.newGridColumn("Patient Type", "InOut", true));

			bc = this.newGridColumn("Cases", "Cases", true);
			bc.Format = "#,###";
			bc.Alignment = HorizontalAlignment.Right;
			tStyle.GridColumnStyles.Add(bc);

			bc = this.newGridColumn("Charges", "Charges", true);
			bc.Format = "C0";
			bc.Alignment = HorizontalAlignment.Right;
			tStyle.GridColumnStyles.Add(bc);

			bc = this.newGridColumn("Model", "Model", true);
			bc.Format = "C0";
			bc.Alignment = HorizontalAlignment.Right;
			tStyle.GridColumnStyles.Add(bc);

			bc = this.newGridColumn("POC", "POC", true);
			bc.Format = "P2";
			bc.Alignment = HorizontalAlignment.Right;
			tStyle.GridColumnStyles.Add(bc);

			bc = this.newGridColumn("NetRev", "NetRev", true);
			bc.Format = "C0";
			bc.Alignment = HorizontalAlignment.Right;
			tStyle.GridColumnStyles.Add(bc);

			bc = this.newGridColumn("Var", "Var", true);
			bc.Format = "P2";
			bc.Alignment = HorizontalAlignment.Right;
			tStyle.GridColumnStyles.Add(bc);

    }

		private void setupDataTable()
		{
			dTable = new DataTable("Analysis");

			dTable.Columns.Add("InOut", Type.GetType("System.String"));
			dTable.Columns.Add("Cases", Type.GetType("System.Double"));
			dTable.Columns.Add("Charges", Type.GetType("System.Double"));
			dTable.Columns.Add("Model", Type.GetType("System.Double"));
			dTable.Columns.Add("POC", Type.GetType("System.Double"));
			dTable.Columns.Add("NetRev", Type.GetType("System.Double"));
			dTable.Columns.Add("Var", Type.GetType("System.Double"));
		}

		public void loadData(DataTable encounterTable)
		{
			double inCases = 0;
			double inCharges = 0;
			double inModel = 0;
			double inNetRev = 0;

			double outCases = 0;
			double outCharges = 0;
			double outModel = 0;
			double outNetRev = 0;

			foreach(DataRow dRow in encounterTable.Rows)
			{
				if (dRow["InOut"].ToString() =="I")
				{
					inCases += 1;
          
					inCharges += Convert.ToDouble(dRow["Charges"]);
					inModel += Convert.ToDouble(dRow["Model"]);
					inNetRev += Convert.ToDouble(dRow["NetRev"]);
				}
				else
				{
					outCases += 1;

					outCharges += Convert.ToDouble(dRow["Charges"]);
					outModel += Convert.ToDouble(dRow["Model"]);
					outNetRev += Convert.ToDouble(dRow["NetRev"]);
				}
			}

			DataRow inRow, outRow, totalRow;

			inRow = dTable.NewRow();
			outRow = dTable.NewRow();
			totalRow = dTable.NewRow();

			inRow["InOut"] = "Inpatient";
			inRow["Cases"] = inCases;
			inRow["Charges"] = inCharges;
			inRow["Model"] = inModel;
			inRow["NetRev"] = inNetRev;
			inRow["POC"] = inModel / inCharges;
			inRow["Var"] = (inModel / inNetRev) - 1;

			outRow["InOut"] = "Outpatient";
			outRow["Cases"] = outCases;
			outRow["Charges"] = outCharges;
			outRow["Model"] = outModel;
			outRow["NetRev"] = outNetRev;
			outRow["POC"] = outModel / outCharges;
			outRow["Var"] = (outModel / outNetRev) - 1;

			totalRow["InOut"] = "Total";
			totalRow["Cases"] = inCases + outCases;
			totalRow["Charges"] = inCharges + outCharges;
			totalRow["Model"] = inModel + outModel;
			totalRow["NetRev"] = inNetRev + outNetRev;
			totalRow["POC"] = (inModel + outModel) / (inCharges + outCharges);
			totalRow["Var"] = ( (inModel + outModel) / (inNetRev + outNetRev) ) -1;

      dTable.Rows.Add(inRow);
			dTable.Rows.Add(outRow);
      dTable.Rows.Add(totalRow);

			this.AnalysisGrid.DataSource = dTable;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Analysis_SummaryGrid_Control
			// 
			this.Name = "Analysis_SummaryGrid_Control";
			this.Size = new System.Drawing.Size(488, 216);

		}
		#endregion
	}
}
