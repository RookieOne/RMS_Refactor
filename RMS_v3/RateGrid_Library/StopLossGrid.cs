using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace RateGrid_Library
{
	/// <summary>
	/// StopLossGrid displays a datatable of StopLoss Rates
	/// </summary>
	public class StopLossGrid : RateGrid
	{
		#region "Variables"

		private System.ComponentModel.Container components = null;

		#endregion

		#region "Constructors"

		public StopLossGrid(string mappingName, string captionTxt) : base(mappingName, captionTxt)
		{
			InitializeComponent();	// This call is required by the Windows.Forms Form Designer.

			base.Order = 15;

			setupColumns();
		}

		private void setupColumns()
		{
			DataGridTextBoxColumn tCol;
			
			// NAME
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Name";
			tCol.MappingName = "Name";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .25);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			// CODES
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Codes";
			tCol.MappingName = "Codes";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .15);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			// TYPE
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Type";
			tCol.MappingName = "RateType";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .1);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			// THRESHOLD
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Threshold";
			tCol.MappingName = "Threshold";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .15);

			tCol.Format = "C0";
			tCol.Alignment = System.Windows.Forms.HorizontalAlignment.Right;

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			// POC
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "POC";
			tCol.MappingName = "POC";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .1);

			tCol.Format = "P2";
			tCol.Alignment = System.Windows.Forms.HorizontalAlignment.Right;

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			// DAILY CAP
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Daily Cap";
			tCol.MappingName = "DailyCap";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .1);

			tCol.Format = "C0";
			tCol.Alignment = System.Windows.Forms.HorizontalAlignment.Right;

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			// PASSTHRU
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "PassThrus";
			tCol.MappingName = "PassThrus";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .15);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

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
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
