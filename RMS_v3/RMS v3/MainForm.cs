using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem quitMenuItem;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem analyzeRateSchedulesMenuItem;
		private System.Windows.Forms.MenuItem manageDataSetsMenuItem;
		private System.Windows.Forms.MenuItem manageOtherDataMenuItem;

		private RMS_Controller rmsController;

		public MainForm()
		{
			rmsController = new RMS_Controller();
			rmsController.MainFrm = this;

			InitializeComponent();

			Panel leftPanel = new Panel();
			leftPanel.AutoScroll = true;
			leftPanel.Width = 350;
			leftPanel.Dock = DockStyle.Left;

			Splitter splitterCtrl = new Splitter();
			splitterCtrl.Dock = DockStyle.Left;
			splitterCtrl.MinExtra = 200;
			splitterCtrl.MinSize = 200;

			Panel mainPanel = new Panel();
			mainPanel.AutoScroll = true;
			mainPanel.Dock = DockStyle.Fill;

			this.Controls.AddRange(new Control[] {mainPanel, splitterCtrl, leftPanel});

			Contract_Control contractsCtrl = new Contract_Control(rmsController);
			contractsCtrl.Dock = DockStyle.Fill;
			leftPanel.Controls.Add(contractsCtrl);

			RateSchedule_Control rateScheduleCtrl = new RateSchedule_Control(rmsController);
			rateScheduleCtrl.Dock = DockStyle.Fill;
			mainPanel.Controls.Add(rateScheduleCtrl);
		

			//rmsController.RateIDChanged += new System.EventHandler(this.RateID_Changed);
			//rmsController.RateScheduleChange += new System.EventHandler(this.RateID_Changed);
		}


		public void RateID_Changed(object sender, System.EventArgs e)
		{

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.quitMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.manageDataSetsMenuItem = new System.Windows.Forms.MenuItem();
			this.manageOtherDataMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.analyzeRateSchedulesMenuItem = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuItem1,
																																							this.menuItem2,
																																							this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.quitMenuItem});
			this.menuItem1.Text = "File";
			// 
			// quitMenuItem
			// 
			this.quitMenuItem.Index = 0;
			this.quitMenuItem.Text = "Quit";
			this.quitMenuItem.Click += new System.EventHandler(this.quitMenuItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.manageDataSetsMenuItem,
																																							this.manageOtherDataMenuItem});
			this.menuItem2.Text = "Manage Data";
			// 
			// manageDataSetsMenuItem
			// 
			this.manageDataSetsMenuItem.Index = 0;
			this.manageDataSetsMenuItem.Text = "Manage Datasets";
			this.manageDataSetsMenuItem.Click += new System.EventHandler(this.manageDataSetsMenuItem_Click);
			// 
			// manageOtherDataMenuItem
			// 
			this.manageOtherDataMenuItem.Index = 1;
			this.manageOtherDataMenuItem.Text = "Manage Other Data";
			this.manageOtherDataMenuItem.Click += new System.EventHandler(this.manageOtherDataMenuItem_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.analyzeRateSchedulesMenuItem});
			this.menuItem3.Text = "Analysis";
			// 
			// analyzeRateSchedulesMenuItem
			// 
			this.analyzeRateSchedulesMenuItem.Index = 0;
			this.analyzeRateSchedulesMenuItem.Text = "Analyze Rate Schedules";
			this.analyzeRateSchedulesMenuItem.Click += new System.EventHandler(this.analyzeRateSchedulesMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(680, 470);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.Text = "RMS v3";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

		}
		#endregion


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void quitMenuItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void analyzeRateSchedulesMenuItem_Click(object sender, System.EventArgs e)
		{
			AnalysisForm analysisFrm = new AnalysisForm();
			analysisFrm.Show();
		}

		private void manageDataSetsMenuItem_Click(object sender, System.EventArgs e)
		{
			ManageDataSetsForm manageDataSetsFrm = new ManageDataSetsForm();
			manageDataSetsFrm.Show();
		}

		private void manageOtherDataMenuItem_Click(object sender, System.EventArgs e)
		{
			ManageOtherDataForm manageOtherDataFrm = new ManageOtherDataForm();
			manageOtherDataFrm.Show();
		}


	}
}
