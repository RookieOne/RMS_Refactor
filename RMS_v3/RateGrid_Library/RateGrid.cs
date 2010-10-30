using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace RateGrid_Library
{
	/// <summary>
	/// Summary description for RateGrid.
	/// </summary>
	public class RateGrid : GridControl
	{
		protected System.Windows.Forms.DataGrid rateDataGrid;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region "Variables"

		private int var_RateID;

		public event EventHandler RateSelected;

		private ArrayList columnPercents;

		#endregion

		#region "Constructors"

		public RateGrid(string mappingName, string captionTxt) : base()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			Font myFont;

			columnPercents = new ArrayList();

			// Caption
			myFont = new Font("Microsoft San Serif", 10);

			this.rateDataGrid.CaptionFont = myFont;
			this.rateDataGrid.CaptionForeColor = Color.SteelBlue;
			this.rateDataGrid.CaptionBackColor = Color.WhiteSmoke;
			this.rateDataGrid.CaptionText = captionTxt;
			

			// Regular Grid
			myFont = new Font("Microsoft San Serif", 8);
			this.rateDataGrid.Font = myFont;
			this.rateDataGrid.ReadOnly = true;
			this.rateDataGrid.FlatMode = true;

			this.rateDataGrid.BorderStyle = BorderStyle.None;

			this.rateDataGrid.GridLineColor = Color.Black;
			this.rateDataGrid.GridLineStyle = DataGridLineStyle.Solid;

			this.rateDataGrid.BackgroundColor = Color.WhiteSmoke;



			DataGridTableStyle tStyle = new DataGridTableStyle();

			tStyle.ReadOnly = true;
			tStyle.MappingName = mappingName;

			tStyle.AlternatingBackColor = Color.LightSteelBlue;
			tStyle.GridLineColor = Color.Gainsboro;
			tStyle.GridLineStyle = DataGridLineStyle.Solid;

			// Header Formatting
			tStyle.HeaderBackColor = Color.SlateGray;
			tStyle.HeaderForeColor = Color.Black;
	
			myFont = new Font("Microsoft San Serif", 9, System.Drawing.FontStyle.Bold);
			tStyle.HeaderFont = myFont;



			tStyle.AllowSorting = false;
			tStyle.RowHeadersVisible = false;

			this.rateDataGrid.TableStyles.Add(tStyle);
			this.Order = 0;
		}


		#endregion

		#region "Properties"

		public int RateID
		{
			get{return var_RateID;}
			set{var_RateID = value;}
		}

		#endregion

		#region "Methods"

		public void setDataSource(DataTable rateTable)
		{
			rateDataGrid.DataSource = rateTable;
		}

		public override void setHeight()
		{
			/*
			int colWidths = 0;
			for(int k=0; k<rateDataGrid.TableStyles[0].GridColumnStyles.Count; k++)
			{
				colWidths += rateDataGrid.TableStyles[0].GridColumnStyles[k].Width;
			}

			this.Width = 50 + colWidths;
			*/

			DataTable rateTable = (DataTable) rateDataGrid.DataSource;
      			
			this.Height = 40+rateTable.Rows.Count * 18;
		}

		public override void setWidth(int width)
		{
			
			ArrayList widthPercents = new ArrayList();
			double widthTotal = 0;
			double widthPercent;

			for(int k=0; k<rateDataGrid.TableStyles[0].GridColumnStyles.Count; k++)
			{
				widthPercent = ((double)rateDataGrid.TableStyles[0].GridColumnStyles[k].Width) / ((double)rateDataGrid.Width);
				widthPercents.Add(widthPercent);

				widthTotal += widthPercent;
			}

			if (widthTotal<1)
			{
				widthPercent = (double) widthPercents[0];
				widthPercent += 1 - widthTotal;
				widthPercents[0] = widthPercent;
			}

			this.Width = width;

			rateDataGrid.Width = width - 20;
			int columnTotalWidth = rateDataGrid.Width - 10;
			

			for(int k=0; k<rateDataGrid.TableStyles[0].GridColumnStyles.Count; k++)
			{
				rateDataGrid.TableStyles[0].GridColumnStyles[k].Width = Convert.ToInt16(columnTotalWidth * (double)widthPercents[k]);
			}


			setHeight();
		}

		#endregion

		#region "Event Handlers"

		public void rateDataGrid_CurrentCellChanged(object sender, System.EventArgs e)
		{
			RateSelected(sender, e);
		}


		#endregion


		#region "Overrrides"




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
			this.rateDataGrid = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.rateDataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// rateDataGrid
			// 
			this.rateDataGrid.CaptionBackColor = System.Drawing.Color.White;
			this.rateDataGrid.CaptionForeColor = System.Drawing.Color.Navy;
			this.rateDataGrid.DataMember = "";
			this.rateDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rateDataGrid.HeaderBackColor = System.Drawing.Color.SteelBlue;
			this.rateDataGrid.HeaderForeColor = System.Drawing.Color.WhiteSmoke;
			this.rateDataGrid.Location = new System.Drawing.Point(0, 0);
			this.rateDataGrid.Name = "rateDataGrid";
			this.rateDataGrid.ParentRowsVisible = false;
			this.rateDataGrid.RowHeadersVisible = false;
			this.rateDataGrid.Size = new System.Drawing.Size(608, 240);
			this.rateDataGrid.TabIndex = 0;
			this.rateDataGrid.CurrentCellChanged += new System.EventHandler(this.rateDataGrid_CurrentCellChanged);
			// 
			// RateGrid
			// 
			this.Controls.Add(this.rateDataGrid);
			this.Name = "RateGrid";
			this.Size = new System.Drawing.Size(608, 240);
			((System.ComponentModel.ISupportInitialize)(this.rateDataGrid)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
