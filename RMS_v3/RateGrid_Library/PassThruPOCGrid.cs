using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace RateGrid_Library
{
	/// <summary>
	/// PassThruPOCGrid displays a data table of Pass Thru rates that are POCs
	/// </summary>
	/// 

	public class PassThruPOCGrid : RateGrid
	{
		#region "Variables"

		private System.ComponentModel.Container components = null;

		#endregion

		#region "Constructors"

		public PassThruPOCGrid(string mappingName, string captionTxt) : base(mappingName, captionTxt)
	{
		InitializeComponent();	// This call is required by the Windows.Forms Form Designer.

		base.Order = 70;

		setupColumns();
	}


	private void setupColumns()
{
    DataGridTextBoxColumn tCol;
	
		// NAME
    tCol = new DataGridTextBoxColumn();
    tCol.HeaderText = "Name";
    tCol.MappingName = "Name";
    tCol.Width = Convert.ToInt16(rateDataGrid.Width * .35);

    this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

		// CODES
    tCol = new DataGridTextBoxColumn();
    tCol.HeaderText = "Codes";
    tCol.MappingName = "Codes";
    tCol.Width = Convert.ToInt16(rateDataGrid.Width * .35);

    this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

		// TYPE
    tCol = new DataGridTextBoxColumn();
    tCol.HeaderText = "Type";
    tCol.MappingName = "RateType";
    tCol.Width = Convert.ToInt16(rateDataGrid.Width * .15);

    this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

		// RATE
    tCol = new DataGridTextBoxColumn();
    tCol.HeaderText = "Rate";
    tCol.MappingName = "Rate";
    tCol.Format = "P2";
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
