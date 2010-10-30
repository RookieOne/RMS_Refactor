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
	/// Summary description for ASCGrid.
	/// </summary>
	public class ASCGrid : GridControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label codesLbl;
		private System.Windows.Forms.Panel ascRatePanel;
		private System.Windows.Forms.Panel ratesPanel;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel etcPanel;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Rate_ASCRateBO ascRate;

		#region "Constructor"

		public ASCGrid() : base()
		{
			InitializeComponent();	// This call is required by the Windows.Forms Form Designer.

			this.Order = 1;
		}

		#endregion

		#region "Properties"


		#endregion

		#region "Methods"

		public void setASCRate(Rate_ASCRateBO in_ASCRate)
		{
			ascRate = in_ASCRate;

			codesLbl.Text = "";
			if (ascRate.StandardCPTs)
			{	codesLbl.Text = "CPT: 10000 - 69999; ";	}

			codesLbl.Text += ascRate.Codes.ToString();
			
			loadGroupDisplay();

			loadEtcDisplay();

		}

		public override void setHeight()
		{
			

		}

		public override void setWidth(int width)
		{
			this.Width = width;
		}


		private Control addLabel(string label, string text)
		{
			Panel newPanel = new Panel();

			Label labelLbl = new Label();
			labelLbl.Dock = DockStyle.Left;
			labelLbl.Width = 100;

			labelLbl.Font = new Font("Microsoft San Serif", 9, System.Drawing.FontStyle.Bold);

			labelLbl.Text = label;

						

			Label textLbl = new Label();
			textLbl.Dock = DockStyle.Fill;
			textLbl.Text = text;
			


			newPanel.Controls.Add(textLbl);
			newPanel.Controls.Add(labelLbl);

			newPanel.Dock = DockStyle.Top;
			newPanel.Height = 15;

			return newPanel;
		}


		private void loadGroupDisplay()
		{
			DataGrid groupGrid = new DataGrid();

			// Caption
			Font myFont = new Font("Microsoft San Serif", 10);

			groupGrid.CaptionFont = myFont;
			groupGrid.CaptionForeColor = Color.SteelBlue;
			groupGrid.CaptionBackColor = Color.WhiteSmoke;
			groupGrid.CaptionText = "Group Rates";
			

			// Regular Grid
			myFont = new Font("Microsoft San Serif", 8);
			groupGrid.Font = myFont;
			groupGrid.ReadOnly = true;
			groupGrid.FlatMode = true;

			groupGrid.BorderStyle = BorderStyle.None;

		  groupGrid.GridLineColor = Color.Black;
			groupGrid.GridLineStyle = DataGridLineStyle.Solid;

			groupGrid.BackgroundColor = Color.WhiteSmoke;



			DataGridTableStyle tStyle = new DataGridTableStyle();

			tStyle.ReadOnly = true;
			tStyle.MappingName = "ASC Rates";

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

			groupGrid.TableStyles.Add(tStyle);


			DataGridTextBoxColumn tCol;
			
			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Group";
			tCol.MappingName = "Group";
			tCol.Width = 70;

			groupGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Rate";
			tCol.MappingName = "Rate";
			tCol.Width = 80;

			groupGrid.TableStyles[0].GridColumnStyles.Add(tCol);

			tCol = new DataGridTextBoxColumn();
			tCol.HeaderText = "Threshold";
			tCol.MappingName = "Threshold";
			tCol.Width = 80;

			groupGrid.TableStyles[0].GridColumnStyles.Add(tCol);


			groupGrid.DataSource = ascRate.getASCRateTable();

			groupGrid.Dock = DockStyle.Fill;
    	
			ratesPanel.Controls.Add(groupGrid);
		}


		private void loadEtcDisplay()
		{
			string rateReimbursementStr = "";

			for(int r=ascRate.RateReimbursement.Count-1; r>=0; r--)
			{
				rateReimbursementStr += string.Format("{0:P2}", ascRate.RateReimbursementAt(r)) + ", ";
			}

			rateReimbursementStr = rateReimbursementStr.Substring(0, rateReimbursementStr.Length-2);

      etcPanel.Controls.Add(addLabel("Rate Reimbursement", rateReimbursementStr));

			etcPanel.Controls.Add(addLabel("Threshold", string.Format("{0:C0}", ascRate.Threshold.ToString())));
			etcPanel.Controls.Add(addLabel("Priority", ascRate.Priority.ToString()));

		}

		#endregion

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
			this.label1 = new System.Windows.Forms.Label();
			this.codesLbl = new System.Windows.Forms.Label();
			this.ascRatePanel = new System.Windows.Forms.Panel();
			this.ratesPanel = new System.Windows.Forms.Panel();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.etcPanel = new System.Windows.Forms.Panel();
			this.ascRatePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.SteelBlue;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(560, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Ambulatory Surgery Rates";
			// 
			// codesLbl
			// 
			this.codesLbl.Dock = System.Windows.Forms.DockStyle.Top;
			this.codesLbl.Location = new System.Drawing.Point(0, 20);
			this.codesLbl.Name = "codesLbl";
			this.codesLbl.Size = new System.Drawing.Size(560, 36);
			this.codesLbl.TabIndex = 1;
			// 
			// ascRatePanel
			// 
			this.ascRatePanel.Controls.Add(this.etcPanel);
			this.ascRatePanel.Controls.Add(this.splitter1);
			this.ascRatePanel.Controls.Add(this.ratesPanel);
			this.ascRatePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ascRatePanel.Location = new System.Drawing.Point(0, 56);
			this.ascRatePanel.Name = "ascRatePanel";
			this.ascRatePanel.Size = new System.Drawing.Size(560, 296);
			this.ascRatePanel.TabIndex = 2;
			// 
			// ratesPanel
			// 
			this.ratesPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.ratesPanel.Location = new System.Drawing.Point(0, 0);
			this.ratesPanel.Name = "ratesPanel";
			this.ratesPanel.Size = new System.Drawing.Size(230, 296);
			this.ratesPanel.TabIndex = 0;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(200, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 296);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// etcPanel
			// 
			this.etcPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.etcPanel.Location = new System.Drawing.Point(203, 0);
			this.etcPanel.Name = "etcPanel";
			this.etcPanel.Size = new System.Drawing.Size(357, 296);
			this.etcPanel.TabIndex = 2;
			// 
			// ASCGrid
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.ascRatePanel);
			this.Controls.Add(this.codesLbl);
			this.Controls.Add(this.label1);
			this.Name = "ASCGrid";
			this.Size = new System.Drawing.Size(560, 352);
			this.ascRatePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
