using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace RateGrid_Library
{
	/// <summary>
	/// Summary description for PerDiemGrid.
	/// </summary>
	public class PerDiemGrid : RateGrid
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PerDiemGrid(string mappingName, string captionTxt) : base(mappingName, captionTxt)
		{
			InitializeComponent();	// This call is required by the Windows.Forms Form Designer.

			base.Order = 50;

			setupColumns();
		}


		private void setupColumns()
		{
			DataGridTextBoxColumn tCol;
			
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Name";
			tCol.MappingName = "Name";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .35);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Codes";
			tCol.MappingName = "Codes";
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .35);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Rate";
			tCol.MappingName = "Rate";
			tCol.Format = "C0";
			tCol.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			tCol.Width = Convert.ToInt16(rateDataGrid.Width * .3);

			this.rateDataGrid.TableStyles[0].GridColumnStyles.Add(tCol);
		}



		#region "Override"

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
			// PerDiemGrid
			// 
			this.Name = "PerDiemGrid";
			this.Size = new System.Drawing.Size(376, 200);

		}
		#endregion
	}
}
