using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

using RMSCtrl_Library;
using AnalysisGrid_Library;
using RMS_DALObjects;
using RMS_BusinessObjects;
using VB_ExcelReport_Library;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for AnalysisForm.
	/// </summary>
	public class AnalysisForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		
		#region "Variables"

		ArrayList filterEntityList;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel detailsPanel;
		internal System.Windows.Forms.Label exportLbl;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.CheckBox filterInsPlans;
		internal System.Windows.Forms.CheckBox filterHospitals;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.TextBox costInc;
		internal System.Windows.Forms.TextBox InChgInc;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.TextBox OutChgInc;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label rateScheduleLbl;
		internal System.Windows.Forms.ProgressBar progBar;
		internal System.Windows.Forms.Label progLbl;
		internal System.Windows.Forms.Label timerLbl;
		internal System.Windows.Forms.Button analyzeBtn;
		internal System.Windows.Forms.ComboBox baseComboBx;
		internal System.Windows.Forms.ComboBox datasetComboBx;
		internal System.Windows.Forms.Label Label7;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel mainRateSchedulePanel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel resultsTitlePanel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel resultsPanel;
		internal System.Windows.Forms.Button exportXMLBtn;
		internal System.Windows.Forms.Button saveReportBtn;
		ArrayList filterInsurancePlanCodeList;


		#endregion


		public AnalysisForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			setupUI();

			mainRateSchedulePanel.Controls.Add(new RateSchedule_Selection_Ctrl());
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

		#region "UI Methods"

		private void setupUI()
		{
      DataSet oDataSet = data.GetDataSet("SELECT * FROM Dataset ORDER BY DatasetName ASC");

      datasetComboBx.DisplayMember = "DatasetName";
      datasetComboBx.ValueMember = "DatasetSeqNum";
      datasetComboBx.DataSource = oDataSet.Tables[0];

			exportXMLBtn.Visible = false;
			saveReportBtn.Visible = false;
		}


		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AnalysisForm));
			this.panel3 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.topPanel = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.detailsPanel = new System.Windows.Forms.Panel();
			this.exportLbl = new System.Windows.Forms.Label();
			this.exportXMLBtn = new System.Windows.Forms.Button();
			this.saveReportBtn = new System.Windows.Forms.Button();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.Label11 = new System.Windows.Forms.Label();
			this.filterInsPlans = new System.Windows.Forms.CheckBox();
			this.filterHospitals = new System.Windows.Forms.CheckBox();
			this.Label8 = new System.Windows.Forms.Label();
			this.costInc = new System.Windows.Forms.TextBox();
			this.InChgInc = new System.Windows.Forms.TextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.OutChgInc = new System.Windows.Forms.TextBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.rateScheduleLbl = new System.Windows.Forms.Label();
			this.progBar = new System.Windows.Forms.ProgressBar();
			this.progLbl = new System.Windows.Forms.Label();
			this.timerLbl = new System.Windows.Forms.Label();
			this.baseComboBx = new System.Windows.Forms.ComboBox();
			this.datasetComboBx = new System.Windows.Forms.ComboBox();
			this.Label7 = new System.Windows.Forms.Label();
			this.analyzeBtn = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.mainRateSchedulePanel = new System.Windows.Forms.Panel();
			this.resultsTitlePanel = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.resultsPanel = new System.Windows.Forms.Panel();
			this.panel3.SuspendLayout();
			this.topPanel.SuspendLayout();
			this.panel2.SuspendLayout();
			this.detailsPanel.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.resultsTitlePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.White;
			this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(848, 48);
			this.panel3.TabIndex = 58;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(4, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(376, 40);
			this.label3.TabIndex = 0;
			this.label3.Text = "Rate Schedules Analysis";
			// 
			// topPanel
			// 
			this.topPanel.Controls.Add(this.panel2);
			this.topPanel.Controls.Add(this.panel1);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 48);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(848, 272);
			this.topPanel.TabIndex = 59;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.detailsPanel);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(412, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(648, 272);
			this.panel2.TabIndex = 1;
			// 
			// detailsPanel
			// 
			this.detailsPanel.BackColor = System.Drawing.Color.Transparent;
			this.detailsPanel.Controls.Add(this.exportLbl);
			this.detailsPanel.Controls.Add(this.exportXMLBtn);
			this.detailsPanel.Controls.Add(this.saveReportBtn);
			this.detailsPanel.Controls.Add(this.GroupBox1);
			this.detailsPanel.Controls.Add(this.datasetComboBx);
			this.detailsPanel.Controls.Add(this.Label7);
			this.detailsPanel.Controls.Add(this.analyzeBtn);
			this.detailsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.detailsPanel.Location = new System.Drawing.Point(0, 0);
			this.detailsPanel.Name = "detailsPanel";
			this.detailsPanel.Size = new System.Drawing.Size(648, 272);
			this.detailsPanel.TabIndex = 1;
			// 
			// exportLbl
			// 
			this.exportLbl.BackColor = System.Drawing.Color.Transparent;
			this.exportLbl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.exportLbl.ForeColor = System.Drawing.Color.Black;
			this.exportLbl.Location = new System.Drawing.Point(212, 232);
			this.exportLbl.Name = "exportLbl";
			this.exportLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.exportLbl.Size = new System.Drawing.Size(200, 16);
			this.exportLbl.TabIndex = 57;
			// 
			// exportXMLBtn
			// 
			this.exportXMLBtn.BackColor = System.Drawing.Color.White;
			this.exportXMLBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.exportXMLBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.exportXMLBtn.ForeColor = System.Drawing.Color.Black;
			this.exportXMLBtn.Location = new System.Drawing.Point(216, 200);
			this.exportXMLBtn.Name = "exportXMLBtn";
			this.exportXMLBtn.Size = new System.Drawing.Size(96, 24);
			this.exportXMLBtn.TabIndex = 54;
			this.exportXMLBtn.Text = "Export as XML";
			this.exportXMLBtn.Visible = false;
			// 
			// saveReportBtn
			// 
			this.saveReportBtn.BackColor = System.Drawing.Color.White;
			this.saveReportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.saveReportBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.saveReportBtn.ForeColor = System.Drawing.Color.Black;
			this.saveReportBtn.Location = new System.Drawing.Point(320, 200);
			this.saveReportBtn.Name = "saveReportBtn";
			this.saveReportBtn.Size = new System.Drawing.Size(96, 24);
			this.saveReportBtn.TabIndex = 56;
			this.saveReportBtn.Text = "Save Report";
			this.saveReportBtn.Visible = false;
			// 
			// GroupBox1
			// 
			this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.GroupBox1.Controls.Add(this.Label11);
			this.GroupBox1.Controls.Add(this.filterInsPlans);
			this.GroupBox1.Controls.Add(this.filterHospitals);
			this.GroupBox1.Controls.Add(this.Label8);
			this.GroupBox1.Controls.Add(this.costInc);
			this.GroupBox1.Controls.Add(this.InChgInc);
			this.GroupBox1.Controls.Add(this.Label2);
			this.GroupBox1.Controls.Add(this.OutChgInc);
			this.GroupBox1.Controls.Add(this.Label1);
			this.GroupBox1.Controls.Add(this.rateScheduleLbl);
			this.GroupBox1.Controls.Add(this.progBar);
			this.GroupBox1.Controls.Add(this.progLbl);
			this.GroupBox1.Controls.Add(this.timerLbl);
			this.GroupBox1.Controls.Add(this.baseComboBx);
			this.GroupBox1.Location = new System.Drawing.Point(8, 52);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(412, 144);
			this.GroupBox1.TabIndex = 55;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Analysis Details";
			// 
			// Label11
			// 
			this.Label11.BackColor = System.Drawing.Color.Transparent;
			this.Label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label11.ForeColor = System.Drawing.Color.Black;
			this.Label11.Location = new System.Drawing.Point(192, 52);
			this.Label11.Name = "Label11";
			this.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label11.Size = new System.Drawing.Size(60, 14);
			this.Label11.TabIndex = 56;
			this.Label11.Text = "Base";
			// 
			// filterInsPlans
			// 
			this.filterInsPlans.Location = new System.Drawing.Point(192, 32);
			this.filterInsPlans.Name = "filterInsPlans";
			this.filterInsPlans.Size = new System.Drawing.Size(136, 16);
			this.filterInsPlans.TabIndex = 42;
			this.filterInsPlans.Text = "Filter Insurance Plans";
			// 
			// filterHospitals
			// 
			this.filterHospitals.Location = new System.Drawing.Point(192, 16);
			this.filterHospitals.Name = "filterHospitals";
			this.filterHospitals.Size = new System.Drawing.Size(100, 16);
			this.filterHospitals.TabIndex = 41;
			this.filterHospitals.Text = "Filter Hospitals";
			// 
			// Label8
			// 
			this.Label8.BackColor = System.Drawing.Color.Transparent;
			this.Label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label8.ForeColor = System.Drawing.Color.Black;
			this.Label8.Location = new System.Drawing.Point(0, 66);
			this.Label8.Name = "Label8";
			this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label8.Size = new System.Drawing.Size(142, 14);
			this.Label8.TabIndex = 40;
			this.Label8.Text = "Cost Inc";
			// 
			// costInc
			// 
			this.costInc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.costInc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.costInc.Location = new System.Drawing.Point(142, 64);
			this.costInc.Name = "costInc";
			this.costInc.Size = new System.Drawing.Size(46, 21);
			this.costInc.TabIndex = 39;
			this.costInc.Text = "1.07";
			this.costInc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// InChgInc
			// 
			this.InChgInc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.InChgInc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.InChgInc.Location = new System.Drawing.Point(142, 20);
			this.InChgInc.Name = "InChgInc";
			this.InChgInc.Size = new System.Drawing.Size(46, 21);
			this.InChgInc.TabIndex = 0;
			this.InChgInc.Text = "1.07";
			this.InChgInc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// Label2
			// 
			this.Label2.BackColor = System.Drawing.Color.Transparent;
			this.Label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label2.ForeColor = System.Drawing.Color.Black;
			this.Label2.Location = new System.Drawing.Point(0, 44);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label2.Size = new System.Drawing.Size(142, 14);
			this.Label2.TabIndex = 24;
			this.Label2.Text = "Outpatient ChrgMstr Inc";
			// 
			// OutChgInc
			// 
			this.OutChgInc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.OutChgInc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OutChgInc.Location = new System.Drawing.Point(142, 42);
			this.OutChgInc.Name = "OutChgInc";
			this.OutChgInc.Size = new System.Drawing.Size(46, 21);
			this.OutChgInc.TabIndex = 23;
			this.OutChgInc.Text = "1.07";
			this.OutChgInc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.Color.Transparent;
			this.Label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.Color.Black;
			this.Label1.Location = new System.Drawing.Point(0, 22);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label1.Size = new System.Drawing.Size(142, 14);
			this.Label1.TabIndex = 2;
			this.Label1.Text = "Inpatient ChrgMstr Inc";
			// 
			// rateScheduleLbl
			// 
			this.rateScheduleLbl.BackColor = System.Drawing.Color.Transparent;
			this.rateScheduleLbl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateScheduleLbl.ForeColor = System.Drawing.Color.Black;
			this.rateScheduleLbl.Location = new System.Drawing.Point(6, 88);
			this.rateScheduleLbl.Name = "rateScheduleLbl";
			this.rateScheduleLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.rateScheduleLbl.Size = new System.Drawing.Size(398, 16);
			this.rateScheduleLbl.TabIndex = 19;
			// 
			// progBar
			// 
			this.progBar.Location = new System.Drawing.Point(6, 104);
			this.progBar.Name = "progBar";
			this.progBar.Size = new System.Drawing.Size(402, 8);
			this.progBar.TabIndex = 7;
			// 
			// progLbl
			// 
			this.progLbl.BackColor = System.Drawing.Color.Transparent;
			this.progLbl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.progLbl.ForeColor = System.Drawing.Color.Black;
			this.progLbl.Location = new System.Drawing.Point(6, 112);
			this.progLbl.Name = "progLbl";
			this.progLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.progLbl.Size = new System.Drawing.Size(211, 16);
			this.progLbl.TabIndex = 8;
			// 
			// timerLbl
			// 
			this.timerLbl.BackColor = System.Drawing.Color.Transparent;
			this.timerLbl.ForeColor = System.Drawing.Color.Black;
			this.timerLbl.Location = new System.Drawing.Point(292, 16);
			this.timerLbl.Name = "timerLbl";
			this.timerLbl.Size = new System.Drawing.Size(116, 16);
			this.timerLbl.TabIndex = 53;
			// 
			// baseComboBx
			// 
			this.baseComboBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.baseComboBx.Location = new System.Drawing.Point(256, 52);
			this.baseComboBx.Name = "baseComboBx";
			this.baseComboBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.baseComboBx.Size = new System.Drawing.Size(142, 21);
			this.baseComboBx.TabIndex = 55;
			// 
			// datasetComboBx
			// 
			this.datasetComboBx.Location = new System.Drawing.Point(84, 8);
			this.datasetComboBx.Name = "datasetComboBx";
			this.datasetComboBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.datasetComboBx.Size = new System.Drawing.Size(324, 21);
			this.datasetComboBx.TabIndex = 58;
			// 
			// Label7
			// 
			this.Label7.BackColor = System.Drawing.Color.Transparent;
			this.Label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label7.Location = new System.Drawing.Point(8, 8);
			this.Label7.Name = "Label7";
			this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label7.Size = new System.Drawing.Size(66, 20);
			this.Label7.TabIndex = 57;
			this.Label7.Text = "Data Set";
			// 
			// analyzeBtn
			// 
			this.analyzeBtn.BackColor = System.Drawing.Color.White;
			this.analyzeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.analyzeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.analyzeBtn.ForeColor = System.Drawing.Color.Black;
			this.analyzeBtn.Location = new System.Drawing.Point(116, 200);
			this.analyzeBtn.Name = "analyzeBtn";
			this.analyzeBtn.Size = new System.Drawing.Size(96, 24);
			this.analyzeBtn.TabIndex = 50;
			this.analyzeBtn.Text = "Run Analysis";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.mainRateSchedulePanel);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(412, 272);
			this.panel1.TabIndex = 0;
			// 
			// mainRateSchedulePanel
			// 
			this.mainRateSchedulePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.mainRateSchedulePanel.Location = new System.Drawing.Point(0, 0);
			this.mainRateSchedulePanel.Name = "mainRateSchedulePanel";
			this.mainRateSchedulePanel.Size = new System.Drawing.Size(412, 248);
			this.mainRateSchedulePanel.TabIndex = 0;
			// 
			// resultsTitlePanel
			// 
			this.resultsTitlePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resultsTitlePanel.BackgroundImage")));
			this.resultsTitlePanel.Controls.Add(this.label4);
			this.resultsTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.resultsTitlePanel.Location = new System.Drawing.Point(0, 320);
			this.resultsTitlePanel.Name = "resultsTitlePanel";
			this.resultsTitlePanel.Size = new System.Drawing.Size(848, 48);
			this.resultsTitlePanel.TabIndex = 60;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(376, 40);
			this.label4.TabIndex = 1;
			this.label4.Text = "Analysis Results";
			// 
			// resultsPanel
			// 
			this.resultsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resultsPanel.Location = new System.Drawing.Point(0, 368);
			this.resultsPanel.Name = "resultsPanel";
			this.resultsPanel.Size = new System.Drawing.Size(848, 182);
			this.resultsPanel.TabIndex = 61;
			// 
			// AnalysisForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(848, 550);
			this.Controls.Add(this.resultsPanel);
			this.Controls.Add(this.resultsTitlePanel);
			this.Controls.Add(this.topPanel);
			this.Controls.Add(this.panel3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AnalysisForm";
			this.Text = "AnalysisForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.panel3.ResumeLayout(false);
			this.topPanel.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.detailsPanel.ResumeLayout(false);
			this.GroupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.resultsTitlePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void analyzeBtn_Click(object sender, System.EventArgs e)
		{
      exportXMLBtn.Visible = true;
      exportXMLBtn.Refresh();

      saveReportBtn.Visible = true;
      saveReportBtn.Refresh();

			Thread thread = new Thread(new ThreadStart( analyzeRateSchedule ));
			thread.IsBackground = true;
			thread.Start(); 
		}



		DateTime start_time, stop_time;
		TimeSpan elapsed_time;

		DataSet resultsDS;
		string rateSchedulesAnalyzed;

		BaseDALObject data = new BaseDALObject();


		public void analyzeRateSchedule()
		{
			start_time = DateTime.Now;

			DataSet oDataSet, scheduleDS;
			DataRow copyRow;

			int rateScheduleCount;
			bool first = true;

			resultsDS = new DataSet();

			rateSchedulesAnalyzed = "";
			rateScheduleCount = 1;

			filterEntityList = new ArrayList();
			filterInsurancePlanCodeList = new ArrayList();

			first = true;
      
			RateSchedule_Selection_Ctrl rateScheduleCtrl = (RateSchedule_Selection_Ctrl) mainRateSchedulePanel.Controls[0];

			ArrayList rateSchedules = rateScheduleCtrl.getRateSchedules();

			for (int r=0; r<rateSchedules.Count; r++)
			{
				string rateScheduleID = rateScheduleCtrl.getRateScheduleID(rateSchedules[r].ToString());
				string rateScheduleName = rateScheduleCtrl.getRateScheduleName(rateSchedules[r].ToString());

				rateScheduleLbl.Text = rateScheduleCount + " of " + rateSchedules.Count + "   " + rateScheduleName;
				rateScheduleCount += 1;

				rateScheduleLbl.Refresh();

				if (! filterHospitals.Checked)
				{	filterEntityList.Add("All");	}
				else
				{
					oDataSet = data.GetDataSet("SELECT * FROM Entity_RateSched, Entity WHERE Entity_RateSched.EntityCode=Entity.EntityCode AND RateSchedSeqNum=" + rateScheduleID);

					if (oDataSet.Tables[0].Rows.Count == 0)
					{	filterEntityList.Add("All");	}
					else
					{
						foreach(DataRow dRow in oDataSet.Tables[0].Rows)
						{	filterEntityList.Add(dRow["CompanyCode"]);	}
					}
				}

				if (! filterInsPlans.Checked)
				{	filterInsurancePlanCodeList.Add("All");	}
				else
				{
					oDataSet = data.GetDataSet("SELECT * FROM InsurncPlanCode WHERE RateSchedSeqNum=" + rateScheduleID);

					if (oDataSet.Tables[0].Rows.Count == 0)
					{	filterInsurancePlanCodeList.Add("All");	}
					else
					{
						foreach(DataRow dRow in oDataSet.Tables[0].Rows)
						{	filterInsurancePlanCodeList.Add(dRow["InsurncPlanCode"].ToString());	}
					}
				}

				AnalysisObject analysis = new AnalysisObject(Convert.ToInt16(rateScheduleID), Convert.ToInt16(datasetComboBx.SelectedValue), progBar, progLbl);
				analysis.loadData(filterHospitals.Checked, filterInsPlans.Checked);

				scheduleDS = analysis.Analyze(Convert.ToDouble(InChgInc.Text), Convert.ToDouble(OutChgInc.Text), Convert.ToDouble(costInc.Text));

				if (first)
				{
					foreach(DataTable dTable in scheduleDS.Tables)
					{	resultsDS.Tables.Add(dTable.Clone());	}
					first = false;
				}

				foreach(DataTable dTable in scheduleDS.Tables)
				{
					foreach(DataRow dRow in dTable.Rows)
					{
						copyRow = resultsDS.Tables[dTable.TableName].NewRow();
						for(int k=0; k< dTable.Columns.Count; k++)
						{	copyRow[k] = dRow[k];	}
						resultsDS.Tables[dTable.TableName].Rows.Add(copyRow);
					}
				}

				if (rateSchedulesAnalyzed == "")
				{	rateSchedulesAnalyzed = rateScheduleName;	}
				else
				{	rateSchedulesAnalyzed = rateSchedulesAnalyzed + ", " + rateScheduleName;	}
			}

			stop_time = DateTime.Now;
			elapsed_time = stop_time.Subtract(start_time);

			timerLbl.Visible = true;
			timerLbl.Text = Math.Round(elapsed_time.TotalSeconds, 2) + " seconds";
			timerLbl.Refresh();

      this.BeginInvoke(new MethodInvoker( this.afterAnalysis));
		}


		private void afterAnalysis()
		{
			Analysis_SummaryGrid_Control summaryGrid = new Analysis_SummaryGrid_Control();

			summaryGrid.loadData(resultsDS.Tables["RMS Encounter"]);
			summaryGrid.finalizeGrid();
			summaryGrid.Width = resultsPanel.Width - 50;
			summaryGrid.Left = 5;

			resultsPanel.Controls.Add(summaryGrid);

		}


		private void exportBtn_Click(object sender, System.EventArgs e)
		{
			exportXML();
		}


		private void exportXML()
		{
			exportLbl.Text = "Creating Report...";

			RateSchedule_Selection_Ctrl rateScheduleCtrl = (RateSchedule_Selection_Ctrl) mainRateSchedulePanel.Controls[0];

			string contractName = rateScheduleCtrl.getContractName();

			FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
			folderBrowser.ShowDialog();

      resultsDS.WriteXml(folderBrowser.SelectedPath.ToString() + "\\RMS.xml", XmlWriteMode.WriteSchema);
			resultsDS.WriteXmlSchema(folderBrowser.SelectedPath.ToString() + "\\RMS.xms");
		
			exportLbl.Text = "Export Completed";
		}


		private void saveAnalysisBtn_Click(object sender, System.EventArgs e)
		{
			saveAnalysis();

			//Thread thread = new Thread(new ThreadStart( saveAnalysis ));
			//thread.IsBackground = true;
			//thread.Start(); 
		}

		private void saveAnalysis()
		{
			start_time = DateTime.Now;

			FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
			folderBrowser.ShowDialog();

			AnalysisReportObject report = new AnalysisReportObject();

			RateSchedule_Selection_Ctrl rateScheduleCtrl = (RateSchedule_Selection_Ctrl) mainRateSchedulePanel.Controls[0];

			string selectedContract = rateScheduleCtrl.getContractName();
			string contractName = selectedContract.Substring(selectedContract.IndexOf(":") + 1, selectedContract.Length - selectedContract.IndexOf(":") - 1).ToString();

			Form frm = new Form();

			frm.Text = "Creating Report...";
			frm.Width = 150;
			frm.Height = 40;
			frm.FormBorderStyle = FormBorderStyle.FixedDialog;

			frm.MaximizeBox = false;
			frm.MinimizeBox = false;
			frm.Show();
			frm.Activate();


			string filterEntity, filterInsurancePlanCode;
			bool first;

			filterEntity = "";
			first = true;
			for(int k=0; k< filterEntityList.Count; k++)
			{
				if (first)
				{
					filterEntity = filterEntityList[k].ToString();
					first = false;
				}
				else
				{
					filterEntity += ", " + filterEntityList[k];
				}
			}

			filterInsurancePlanCode = "";
			first = true;
			for(int k=0; k<filterInsurancePlanCodeList.Count; k++)
			{
				if (first)
				{
					filterInsurancePlanCode = filterInsurancePlanCodeList[k].ToString();
					first = false;
				}
				else
				{
					filterInsurancePlanCode += ", " + filterInsurancePlanCodeList[k];
				}
			}
				

			string baseComments = "";
			switch (baseComboBx.SelectedText)
			{
				case "Use Payment" :	baseComments = "Used Payments for NetRev"; break;
				case "Use Other Rate Schedules" : baseComments = "Used Other Rate Schedules to calculate NetRev"; break;
			}

			AnalysisBO analysis = new AnalysisBO();

			analysis.DataSetID = Convert.ToString(datasetComboBx.SelectedValue);

			analysis.ContractTitle = selectedContract;
			analysis.RateSchedulesAnalyzed = rateSchedulesAnalyzed;

			analysis.InpatientChargeIncrease = Convert.ToDouble(InChgInc.Text);
			analysis.OutpatientChargeIncrease = Convert.ToDouble(OutChgInc.Text);
			analysis.CostIncrease = Convert.ToDouble(costInc.Text);

			analysis.FilterEntity = filterEntity;
			analysis.FilterInsurancePlanCode = filterInsurancePlanCode;

			analysis.BaseComments = baseComments;
			
			analysis.Data = resultsDS;

			report.createReport(analysis, folderBrowser.SelectedPath);

			frm.Close();

			stop_time = DateTime.Now;
			elapsed_time = stop_time.Subtract(start_time);
			timerLbl.Visible = true;

			if (Math.Round(elapsed_time.TotalMinutes, 2) > 5)
			{
				timerLbl.Text = Math.Round(elapsed_time.TotalMinutes, 2) + " mins";
			}
			else
			{
				timerLbl.Text = Math.Round(elapsed_time.TotalSeconds, 2) + " secs";
			}

			timerLbl.Refresh();
		}





	}
}
