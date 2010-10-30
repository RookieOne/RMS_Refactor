using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace AnalysisGrid_Library
{
	/// <summary>
	/// Summary description for Analysis_ParentGrid.
	/// </summary>
	public class Analysis_ParentGrid : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.DataGrid analysisGrid;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Analysis_ParentGrid()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

      setupGrid();
		}

		#region "Properties"

		public DataGrid AnalysisGrid
		{
			get{ return analysisGrid; }
		}

		#endregion

		#region "Methods"

		private void setupGrid()
		{
			Font myFont = new Font("Verdana", 8);

			analysisGrid.Font = myFont;
			analysisGrid.ReadOnly = true;
			analysisGrid.FlatMode = true;
			analysisGrid.CaptionVisible = false;
			analysisGrid.AllowSorting = false;
			analysisGrid.BorderStyle = BorderStyle.None;
			analysisGrid.BackgroundColor = Color.WhiteSmoke;

      DataGridTableStyle tStyle = new DataGridTableStyle();
      
			tStyle.ReadOnly = true;
			tStyle.MappingName = "Analysis";
			tStyle.GridLineColor = Color.Black;
			tStyle.GridLineStyle = DataGridLineStyle.Solid;
			tStyle.HeaderForeColor = Color.White;
			tStyle.HeaderBackColor = Color.RoyalBlue;
			tStyle.RowHeadersVisible = false;
		
			tStyle.HeaderFont = myFont;
			tStyle.AllowSorting = false;

			analysisGrid.TableStyles.Add(tStyle);
		
		}

		public void finalizeGrid()
		{
			foreach(Control ctrl in analysisGrid.Controls)
			{	ctrl.Visible = false;	}

			analysisGrid.TableStyles[0].RowHeadersVisible = false;
		}

		public DataGridTextBoxColumn newGridColumn(string header, string mappingName, bool visible)
		{
			DataGridTextBoxColumn tCol = new DataGridTextBoxColumn();

			tCol.HeaderText = header;
			tCol.TextBox.Visible = visible;
			tCol.MappingName = mappingName;

			return tCol;
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
			this.analysisGrid = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.analysisGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// analysisGrid
			// 
			this.analysisGrid.DataMember = "";
			this.analysisGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.analysisGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.analysisGrid.Location = new System.Drawing.Point(0, 0);
			this.analysisGrid.Name = "analysisGrid";
			this.analysisGrid.Size = new System.Drawing.Size(472, 184);
			this.analysisGrid.TabIndex = 0;
			// 
			// Analysis_ParentGrid
			// 
			this.Controls.Add(this.analysisGrid);
			this.Name = "Analysis_ParentGrid";
			this.Size = new System.Drawing.Size(472, 184);
			((System.ComponentModel.ISupportInitialize)(this.analysisGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
