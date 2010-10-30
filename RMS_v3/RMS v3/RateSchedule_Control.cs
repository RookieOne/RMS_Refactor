using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using RMS_DALObjects;
using RMS_BusinessObjects;
using RateGrid_Library;
using VB_ExcelReport_Library;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for RateSchedule_Control.
	/// </summary>
	public class RateSchedule_Control : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel titlePanel;
		private System.Windows.Forms.Label titleLbl;
		private System.Windows.Forms.Panel buttonPanel;
		private System.Windows.Forms.Button printBtn;
		private System.Windows.Forms.Button editCoverageBtn;
		private System.Windows.Forms.Button addRateBtn;
		private System.Windows.Forms.Panel rateSchedulePanel;
		private System.Windows.Forms.Button adjustRatesBtn;
		private System.Windows.Forms.Button ascRatesBtn;

		private RMS_Controller rmsController;

		private RateScheduleBO rateSchedule;

		ArrayList rateScheduleControlList;

		public RateSchedule_Control(RMS_Controller inRMSController)
		{
			InitializeComponent();


			rmsController = inRMSController;

			rmsController.RateScheduleIDChanged += new System.EventHandler(this.RateScheduleID_Changed);
			rmsController.RateScheduleChange += new System.EventHandler(this.RateScheduleID_Changed);
		}

		


		private void loadRateSchedule()
		{
			this.rateSchedulePanel.Controls.Clear();
			rateScheduleControlList = new ArrayList();

			RateScheduleDAL rateScheduleData = new RateScheduleDAL();

			
			rateSchedule = rateScheduleData.getRateSchedule(ref rmsController.CodesMngr, rmsController.RateScheduleID);

			titleLbl.Text = rateSchedule.RateScheduleName;

			loadCoverage();

			loadRate();

			loadControls();
		}

		private void loadCoverage()
		{
			// Dates Coverage
			Label ptLbl = new Label();

			ptLbl.Left = 7;
			ptLbl.Dock = DockStyle.Top;
			ptLbl.Width = this.rateSchedulePanel.Width - 30;

			Font myFont = new Font("Microsoft San Serif", 12);

			ptLbl.Font = myFont;
			ptLbl.ForeColor = Color.SteelBlue;
			ptLbl.BackColor = Color.WhiteSmoke;
			
			ptLbl.Text = "Dates of Coverage : " + rateSchedule.Coverage.StartDate.ToShortDateString() + " - " + rateSchedule.Coverage.EndDate.ToShortDateString();

			rateScheduleControlList.Add(ptLbl);


			// Entities
			EntitiesCovered_Ctrl entitiesCoveredCtrl = new EntitiesCovered_Ctrl();

			entitiesCoveredCtrl.setEntitiesCovered(rateSchedule.Coverage.Entities);
			entitiesCoveredCtrl.Dock = DockStyle.Top;

			rateScheduleControlList.Add(entitiesCoveredCtrl);

			// Insurance Plans
			InsurancePlansCovered_Ctrl insPlansCoveredCtrl = new InsurancePlansCovered_Ctrl();

			insPlansCoveredCtrl.setInsPlansCovered(rateSchedule.Coverage.InsurancePlans);
			insPlansCoveredCtrl.Dock = DockStyle.Top;

			rateScheduleControlList.Add(insPlansCoveredCtrl);
		}


		private void loadRate()
    {
			ArrayList patientTypesList = rateSchedule.Rates.getPatientTypes();
			ArrayList rateCategoriesList;
			ArrayList rateTypesList;
			RateGrid rateGrid;

			ArrayList inpatientGridList = new ArrayList();
			ArrayList outpatientGridList = new ArrayList();

			foreach(char inOut in patientTypesList)
			{
				rateCategoriesList = rateSchedule.Rates.getRateCategories(inOut);

				foreach(string rateCategory in rateCategoriesList)
				{
					if (rateCategory=="ASC")
					{
						ASCGrid myASCGrid = new ASCGrid();

						ArrayList ascList = rateSchedule.Rates.getRateList(inOut, "ASC", "Main");
						myASCGrid.setASCRate((Rate_ASCRateBO) ascList[0]);

						if (inOut=='I')
						{	inpatientGridList.Add(myASCGrid);	}
						else
						{ outpatientGridList.Add(myASCGrid); }
					}
					else
					{
						rateTypesList = rateSchedule.Rates.getRateTypes(inOut, rateCategory);

						foreach(string rateType in rateTypesList)
						{
							rateGrid = getGridType(inOut, rateCategory, rateType);
						
							rateGrid.setDataSource(rateSchedule.Rates.getDataTable(inOut, rateCategory, rateType));

							rateGrid.RateSelected += new EventHandler(this.RateSelected);

							if (inOut=='I')
							{	inpatientGridList.Add(rateGrid);	}
							else
							{ outpatientGridList.Add(rateGrid); }
						}
					}
				}
			}

			object spot;

			for(int k=0; k<inpatientGridList.Count; k++)
			{
				for(int j=k; j<inpatientGridList.Count; j++)
				{
					if (  ((RateGrid)inpatientGridList[j]).Order < ((RateGrid)inpatientGridList[k]).Order )
					{
						spot = inpatientGridList[j];
						inpatientGridList[j] = inpatientGridList[k];
						inpatientGridList[k] = spot;
					}
				}
			}

			int top = displayPatientType(inpatientGridList, "Inpatient Rates", 10);
			displayPatientType(outpatientGridList, "Outpatient Rates", top);
		}


		private int displayPatientType(ArrayList gridList, string patientTypeText, int startingTop)
		{
			object spot;
			for(int k=0; k<gridList.Count; k++)
			{
				for(int j=k; j<gridList.Count; j++)
				{
					if (  ((GridControl)gridList[j]).Order < ((GridControl)gridList[k]).Order )
					{
						spot = gridList[j];
						gridList[j] = gridList[k];
						gridList[k] = spot;
					}
				}
			}

			Label spacerLbl = new Label();
			spacerLbl.Dock = DockStyle.Top;
			spacerLbl.Height = 20;
			spacerLbl.Width = this.rateSchedulePanel.Width - 30;
			rateScheduleControlList.Add(spacerLbl);

			Label ptLbl = new Label();

			ptLbl.Left = 7;
			ptLbl.Dock = DockStyle.Top;
			ptLbl.Text = patientTypeText;
			ptLbl.Width = this.rateSchedulePanel.Width - 30;

			Font myFont = new Font("Microsoft San Serif", 12);

			ptLbl.Font = myFont;
			ptLbl.ForeColor = Color.SteelBlue;
			ptLbl.BackColor = Color.WhiteSmoke;
			

			rateScheduleControlList.Add(ptLbl);

			int top = startingTop + ptLbl.Height;

			GridControl rateGrid;
      for(int k=0; k<gridList.Count; k++)
      {
        rateGrid = (GridControl) gridList[k];
        rateGrid.Left = 7;				
        rateGrid.Dock = DockStyle.Top;

        rateGrid.setWidth(this.Width - 25);
        rateScheduleControlList.Add(rateGrid);
        
				top += rateGrid.Height + 5;
			}

			return top;
		}

		
		private RateGrid getGridType(char inOut, string rateCategory, string rateType)
		{
			switch(rateCategory)
			{
				case "LessorOf":
					return new LessorOfGrid("LessorOf", "Lessor Of Rates");

				case "Ignore":
					return new IgnoreGrid("Ignore", "Codes Ignored");

				case "StopLoss":
					return new StopLossGrid("StopLoss", "Stop Loss Rates");
					
				case "PerDiem" :
					return new PerDiemGrid("PerDiem", "Per Diem Rates");

				case "BaseRate" :
					return new BaseRateGrid("BaseRate", "Base Rates");

				case "FFS" :

				switch(rateType)
				{
					case "CaseRate" :

						if (inOut=='I')
						{	return new FFS_CaseRateGrid(rateCategory, rateCategory + " " + rateType + " Rates");	}
						else
						{	return new FFS_CaseRate_OutpatientGrid(rateCategory, rateCategory + " " + rateType + " Rates");	}

					case "POC" :
						return new FFS_POCGrid(rateCategory, rateCategory + " " +  rateType + " Rates");

					default : 
						return new RateGrid(rateCategory, rateCategory + " Rates");

				}

				case "PassThru" :

				switch(rateType)
				{
					case "PerVisit" : return new PassThruGrid("PassThru", "PassThru Per Visit Rates");
					case "PerUnit" : return new PassThruGrid("PassThru", "PassThru Per Unit Rates");
					case "POC" : return new PassThruPOCGrid("PassThru", "PassThru POC Rates");
					default : return new RateGrid(rateCategory, rateCategory + " Rates");
				}

				default : return new RateGrid(rateCategory, rateCategory + " Rates");
			}
		}


		public void RateSelected(object sender, System.EventArgs e)
		{
			DataGrid dGrid = (DataGrid) sender;
			DataTable dTable = (DataTable) dGrid.DataSource;

			rmsController.launchEditWindow(Convert.ToInt16(dTable.Rows[dGrid.CurrentCell.RowNumber].ItemArray[0]));
		}



		private void loadControls()
		{
			for(int k=rateScheduleControlList.Count-1; k>=0; k--)
			{
				this.rateSchedulePanel.Controls.Add((Control) rateScheduleControlList[k]);

			}
		}


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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RateSchedule_Control));
			this.titlePanel = new System.Windows.Forms.Panel();
			this.titleLbl = new System.Windows.Forms.Label();
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.ascRatesBtn = new System.Windows.Forms.Button();
			this.printBtn = new System.Windows.Forms.Button();
			this.adjustRatesBtn = new System.Windows.Forms.Button();
			this.editCoverageBtn = new System.Windows.Forms.Button();
			this.addRateBtn = new System.Windows.Forms.Button();
			this.rateSchedulePanel = new System.Windows.Forms.Panel();
			this.titlePanel.SuspendLayout();
			this.buttonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// titlePanel
			// 
			this.titlePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("titlePanel.BackgroundImage")));
			this.titlePanel.Controls.Add(this.titleLbl);
			this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.titlePanel.Location = new System.Drawing.Point(0, 0);
			this.titlePanel.Name = "titlePanel";
			this.titlePanel.Size = new System.Drawing.Size(520, 36);
			this.titlePanel.TabIndex = 2;
			// 
			// titleLbl
			// 
			this.titleLbl.BackColor = System.Drawing.Color.Transparent;
			this.titleLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.titleLbl.ForeColor = System.Drawing.Color.White;
			this.titleLbl.Location = new System.Drawing.Point(0, 0);
			this.titleLbl.Name = "titleLbl";
			this.titleLbl.Size = new System.Drawing.Size(520, 36);
			this.titleLbl.TabIndex = 3;
			// 
			// buttonPanel
			// 
			this.buttonPanel.BackColor = System.Drawing.Color.White;
			this.buttonPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPanel.BackgroundImage")));
			this.buttonPanel.Controls.Add(this.ascRatesBtn);
			this.buttonPanel.Controls.Add(this.printBtn);
			this.buttonPanel.Controls.Add(this.adjustRatesBtn);
			this.buttonPanel.Controls.Add(this.editCoverageBtn);
			this.buttonPanel.Controls.Add(this.addRateBtn);
			this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonPanel.Location = new System.Drawing.Point(0, 36);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(520, 32);
			this.buttonPanel.TabIndex = 6;
			// 
			// ascRatesBtn
			// 
			this.ascRatesBtn.BackColor = System.Drawing.Color.Transparent;
			this.ascRatesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ascRatesBtn.ForeColor = System.Drawing.Color.White;
			this.ascRatesBtn.Location = new System.Drawing.Point(272, 4);
			this.ascRatesBtn.Name = "ascRatesBtn";
			this.ascRatesBtn.Size = new System.Drawing.Size(72, 23);
			this.ascRatesBtn.TabIndex = 4;
			this.ascRatesBtn.Text = "ASC Rates";
			// 
			// printBtn
			// 
			this.printBtn.BackColor = System.Drawing.Color.Transparent;
			this.printBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.printBtn.ForeColor = System.Drawing.Color.White;
			this.printBtn.Location = new System.Drawing.Point(348, 4);
			this.printBtn.Name = "printBtn";
			this.printBtn.Size = new System.Drawing.Size(64, 23);
			this.printBtn.TabIndex = 3;
			this.printBtn.Text = "Print";
			this.printBtn.Click += new System.EventHandler(this.printBtn_Click);
			// 
			// adjustRatesBtn
			// 
			this.adjustRatesBtn.BackColor = System.Drawing.Color.Transparent;
			this.adjustRatesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.adjustRatesBtn.ForeColor = System.Drawing.Color.White;
			this.adjustRatesBtn.Location = new System.Drawing.Point(168, 4);
			this.adjustRatesBtn.Name = "adjustRatesBtn";
			this.adjustRatesBtn.Size = new System.Drawing.Size(100, 23);
			this.adjustRatesBtn.TabIndex = 2;
			this.adjustRatesBtn.Text = "Adjust Rates";
			this.adjustRatesBtn.Click += new System.EventHandler(this.adjustRatesBtn_Click);
			// 
			// editCoverageBtn
			// 
			this.editCoverageBtn.BackColor = System.Drawing.Color.Transparent;
			this.editCoverageBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.editCoverageBtn.ForeColor = System.Drawing.Color.White;
			this.editCoverageBtn.Location = new System.Drawing.Point(76, 4);
			this.editCoverageBtn.Name = "editCoverageBtn";
			this.editCoverageBtn.Size = new System.Drawing.Size(88, 23);
			this.editCoverageBtn.TabIndex = 1;
			this.editCoverageBtn.Text = "Edit Coverage";
			this.editCoverageBtn.Click += new System.EventHandler(this.editCoverageBtn_Click);
			// 
			// addRateBtn
			// 
			this.addRateBtn.BackColor = System.Drawing.Color.Transparent;
			this.addRateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addRateBtn.ForeColor = System.Drawing.Color.White;
			this.addRateBtn.Location = new System.Drawing.Point(8, 4);
			this.addRateBtn.Name = "addRateBtn";
			this.addRateBtn.Size = new System.Drawing.Size(64, 23);
			this.addRateBtn.TabIndex = 0;
			this.addRateBtn.Text = "Add Rate";
			this.addRateBtn.Click += new System.EventHandler(this.addRateBtn_Click);
			// 
			// rateSchedulePanel
			// 
			this.rateSchedulePanel.AutoScroll = true;
			this.rateSchedulePanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.rateSchedulePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rateSchedulePanel.Location = new System.Drawing.Point(0, 68);
			this.rateSchedulePanel.Name = "rateSchedulePanel";
			this.rateSchedulePanel.Size = new System.Drawing.Size(520, 380);
			this.rateSchedulePanel.TabIndex = 8;
			// 
			// RateSchedule_Control
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.rateSchedulePanel);
			this.Controls.Add(this.buttonPanel);
			this.Controls.Add(this.titlePanel);
			this.Name = "RateSchedule_Control";
			this.Size = new System.Drawing.Size(520, 448);
			this.titlePanel.ResumeLayout(false);
			this.buttonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void RateScheduleID_Changed(object sender, System.EventArgs e)
		{	loadRateSchedule(); }

		private void addRateBtn_Click(object sender, System.EventArgs e)
		{
			rmsController.launchEditWindow();
		}

		private void editCoverageBtn_Click(object sender, System.EventArgs e)
		{
		rmsController.launchCoverageWindow();
		}

		private void printBtn_Click(object sender, System.EventArgs e)
		{
			RateScheduleReportObject rateScheduleReport = new RateScheduleReportObject(rmsController.RateScheduleID);

			rateScheduleReport.printRateSchedule();
		}

		private void adjustRatesBtn_Click(object sender, System.EventArgs e)
		{
			rmsController.launchAdjustRatesWindow();
		}


	}
}
