using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace RateGrid_Library
{
	/// <summary>
	/// Summary description for CeilingGrid.
	/// </summary>
	public class CeilingGrid : RateGrid
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CeilingGrid(string mappingName, string captionTxt) : base(mappingName, captionTxt)
	{
		// This call is required by the Windows.Forms Form Designer.
		InitializeComponent();

		base.Order = 40;

		setupColumns();
	}


	private void setupColumns()
{
	DataGridTextBoxColumn tCol;
			
	tCol = new DataGridTextBoxColumn();
	tCol.HeaderText = "Name";
	tCol.MappingName = "Name";
	tCol.Width = 100;

	this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

	tCol = new DataGridTextBoxColumn();
	tCol.HeaderText = "Codes";
	tCol.MappingName = "Codes";
	tCol.Width = 100;

	this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

	tCol = new DataGridTextBoxColumn();
	tCol.HeaderText = "Rate";
	tCol.MappingName = "Rate";
	tCol.Width = 100;

	this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

	tCol = new DataGridTextBoxColumn();
	tCol.HeaderText = "PassThrus";
	tCol.MappingName = "PassThrus";
	tCol.Width = 100;

	this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);
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
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
