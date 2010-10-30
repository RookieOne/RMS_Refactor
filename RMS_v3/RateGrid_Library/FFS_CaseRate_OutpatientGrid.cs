using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace RateGrid_Library
{
	/// <summary>
	/// Summary description for FFS_CaseRate_OutpatientGrid.
	/// </summary>
	public class FFS_CaseRate_OutpatientGrid : RateGrid
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FFS_CaseRate_OutpatientGrid(string mappingName, string captionTxt) : base(mappingName, captionTxt)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			base.Order = 60;

			setupColumns();
		}


		private void setupColumns()
		{
			DataGridTextBoxColumn tCol;
			
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Name";
			tCol.MappingName = "Name";

			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .25);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Codes";
			tCol.MappingName = "Codes";

			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .25);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Rate";
			tCol.MappingName = "Rate";

			tCol.Format = "C0";
			tCol.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .2);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Priority";
			tCol.MappingName = "Priority";

			tCol.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .15);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "PassThrus";
			tCol.MappingName = "PassThrus";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .15);

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
