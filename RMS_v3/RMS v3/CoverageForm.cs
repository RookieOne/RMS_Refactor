using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

using RMS_DALObjects;
using RMS_BusinessObjects;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for CoverageForm.
	/// </summary>
	public class CoverageForm : System.Windows.Forms.Form
	{
		#region "Variables"

		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.ListView ipcListView;
		internal System.Windows.Forms.ListView ipcCoveredListView;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.ComboBox tsiFinClassList;
		internal System.Windows.Forms.Button removeIPCBtn;
		internal System.Windows.Forms.Button addIPCBtn;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox endDateBx;
		internal System.Windows.Forms.TextBox startDateBx;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.Button updateBtn;
		internal System.Windows.Forms.Label label;
		internal System.Windows.Forms.TextBox rateScheduleNameBx;
		internal System.Windows.Forms.Label Label6;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private RMS_Controller rmsController;
		private baseDALObject data;

		private CoverageDAL coverage;

		private Entity_Collection entities;
		internal System.Windows.Forms.ComboBox statusTypeComboBx;
		internal System.Windows.Forms.ListBox entityListBx;

		private RateScheduleBO rateSchedule;

		#endregion

		#region "Constructors"

		public CoverageForm(RMS_Controller in_RmsController)
		{
			InitializeComponent();	// Required for Windows Form Designer support

			rmsController = in_RmsController;

			data = new baseDALObject();
			coverage = new CoverageDAL();

			//	Setup UI

			setupStatusComboBox();

			RateScheduleDAL rateScheduleData = new RateScheduleDAL();

			rateSchedule = rateScheduleData.getRateScheduleWithoutRates(rmsController.RateScheduleID);

			rateScheduleNameBx.Text = rateSchedule.RateScheduleName;
			startDateBx.Text = rateSchedule.Coverage.StartDate.ToString();
			endDateBx.Text = rateSchedule.Coverage.EndDate.ToString();

			for(int k=0; k<statusTypeComboBx.Items.Count; k++)
			{
				if (statusTypeComboBx.Items[k].ToString() == rateSchedule.Status)
				{	statusTypeComboBx.SelectedItem = statusTypeComboBx.Items[k];	}
			}


			//	Setup Entity List
			setupEntityListBox();
		}

		#region "Setup UI"

		private void setupStatusComboBox()
		{
			StatusDAL myStatusDAL = new StatusDAL();

			DataTable statusTable = myStatusDAL.getStatusTable();

			statusTypeComboBx.DisplayMember = "Status";
			statusTypeComboBx.ValueMember = "StatusTypeCode";

			statusTypeComboBx.DataSource = statusTable;
			statusTypeComboBx.SelectedIndex = 0;
		}

		private void setupEntityListBox()
		{
			entities = coverage.getEntitiesCovered();

			for(int k=0; k< entities.Count; k++)
			{	entityListBx.Items.Add(  ((EntityBO) entities.getEntityAt(k)).Name );	}

			// Set Selected

			for(int k=0; k<entityListBx.Items.Count; k++)
			{
				for(int e=0; e<rateSchedule.Coverage.Entities.Count; e++)
				{
					if (Convert.ToString(entityListBx.Items[k]) == ((EntityBO) rateSchedule.Coverage.Entities.getEntityAt(e)).Name)
					{	entityListBx.SetSelected(k, true);	}
				}
			}
		}

		#endregion

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CoverageForm));
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.Label4 = new System.Windows.Forms.Label();
			this.ipcListView = new System.Windows.Forms.ListView();
			this.ipcCoveredListView = new System.Windows.Forms.ListView();
			this.Label5 = new System.Windows.Forms.Label();
			this.tsiFinClassList = new System.Windows.Forms.ComboBox();
			this.removeIPCBtn = new System.Windows.Forms.Button();
			this.addIPCBtn = new System.Windows.Forms.Button();
			this.Label8 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.endDateBx = new System.Windows.Forms.TextBox();
			this.startDateBx = new System.Windows.Forms.TextBox();
			this.Label7 = new System.Windows.Forms.Label();
			this.updateBtn = new System.Windows.Forms.Button();
			this.label = new System.Windows.Forms.Label();
			this.statusTypeComboBx = new System.Windows.Forms.ComboBox();
			this.rateScheduleNameBx = new System.Windows.Forms.TextBox();
			this.Label6 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.entityListBx = new System.Windows.Forms.ListBox();
			this.GroupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupBox1
			// 
			this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.GroupBox1.Controls.Add(this.Label4);
			this.GroupBox1.Controls.Add(this.ipcListView);
			this.GroupBox1.Controls.Add(this.ipcCoveredListView);
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.tsiFinClassList);
			this.GroupBox1.Controls.Add(this.removeIPCBtn);
			this.GroupBox1.Controls.Add(this.addIPCBtn);
			this.GroupBox1.Location = new System.Drawing.Point(232, 92);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(428, 284);
			this.GroupBox1.TabIndex = 69;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Insurance Plan Codes";
			// 
			// Label4
			// 
			this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label4.Location = new System.Drawing.Point(2, 132);
			this.Label4.Name = "Label4";
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label4.Size = new System.Drawing.Size(168, 12);
			this.Label4.TabIndex = 34;
			this.Label4.Text = "Filter by TSI Financial Class";
			// 
			// ipcListView
			// 
			this.ipcListView.AllowColumnReorder = true;
			this.ipcListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ipcListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ipcListView.FullRowSelect = true;
			this.ipcListView.Location = new System.Drawing.Point(4, 152);
			this.ipcListView.Name = "ipcListView";
			this.ipcListView.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.ipcListView.Size = new System.Drawing.Size(340, 128);
			this.ipcListView.TabIndex = 33;
			this.ipcListView.View = System.Windows.Forms.View.Details;
			// 
			// ipcCoveredListView
			// 
			this.ipcCoveredListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ipcCoveredListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ipcCoveredListView.FullRowSelect = true;
			this.ipcCoveredListView.Location = new System.Drawing.Point(4, 36);
			this.ipcCoveredListView.Name = "ipcCoveredListView";
			this.ipcCoveredListView.Size = new System.Drawing.Size(416, 90);
			this.ipcCoveredListView.TabIndex = 32;
			this.ipcCoveredListView.View = System.Windows.Forms.View.Details;
			// 
			// Label5
			// 
			this.Label5.BackColor = System.Drawing.Color.RoyalBlue;
			this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label5.ForeColor = System.Drawing.Color.White;
			this.Label5.Location = new System.Drawing.Point(4, 16);
			this.Label5.Name = "Label5";
			this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label5.Size = new System.Drawing.Size(416, 17);
			this.Label5.TabIndex = 31;
			this.Label5.Text = "Insurance Plans Covered";
			// 
			// tsiFinClassList
			// 
			this.tsiFinClassList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tsiFinClassList.Location = new System.Drawing.Point(170, 128);
			this.tsiFinClassList.Name = "tsiFinClassList";
			this.tsiFinClassList.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsiFinClassList.Size = new System.Drawing.Size(121, 21);
			this.tsiFinClassList.TabIndex = 30;
			// 
			// removeIPCBtn
			// 
			this.removeIPCBtn.BackColor = System.Drawing.Color.White;
			this.removeIPCBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.removeIPCBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.removeIPCBtn.Location = new System.Drawing.Point(348, 180);
			this.removeIPCBtn.Name = "removeIPCBtn";
			this.removeIPCBtn.Size = new System.Drawing.Size(72, 24);
			this.removeIPCBtn.TabIndex = 37;
			this.removeIPCBtn.Text = "v Remove";
			// 
			// addIPCBtn
			// 
			this.addIPCBtn.BackColor = System.Drawing.Color.White;
			this.addIPCBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addIPCBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.addIPCBtn.Location = new System.Drawing.Point(348, 152);
			this.addIPCBtn.Name = "addIPCBtn";
			this.addIPCBtn.Size = new System.Drawing.Size(72, 24);
			this.addIPCBtn.TabIndex = 36;
			this.addIPCBtn.Text = "^ Add";
			// 
			// Label8
			// 
			this.Label8.BackColor = System.Drawing.Color.Transparent;
			this.Label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label8.Location = new System.Drawing.Point(224, 36);
			this.Label8.Name = "Label8";
			this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label8.Size = new System.Drawing.Size(120, 14);
			this.Label8.TabIndex = 68;
			this.Label8.Text = "Effective End Date";
			// 
			// Label3
			// 
			this.Label3.BackColor = System.Drawing.Color.Transparent;
			this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label3.Location = new System.Drawing.Point(12, 36);
			this.Label3.Name = "Label3";
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label3.Size = new System.Drawing.Size(120, 14);
			this.Label3.TabIndex = 67;
			this.Label3.Text = "Effective Start Date";
			// 
			// endDateBx
			// 
			this.endDateBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.endDateBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.endDateBx.Location = new System.Drawing.Point(348, 32);
			this.endDateBx.Name = "endDateBx";
			this.endDateBx.Size = new System.Drawing.Size(86, 20);
			this.endDateBx.TabIndex = 66;
			this.endDateBx.Text = "";
			// 
			// startDateBx
			// 
			this.startDateBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.startDateBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.startDateBx.Location = new System.Drawing.Point(136, 32);
			this.startDateBx.Name = "startDateBx";
			this.startDateBx.Size = new System.Drawing.Size(86, 20);
			this.startDateBx.TabIndex = 65;
			this.startDateBx.Text = "";
			// 
			// Label7
			// 
			this.Label7.BackColor = System.Drawing.Color.Transparent;
			this.Label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label7.Location = new System.Drawing.Point(88, 60);
			this.Label7.Name = "Label7";
			this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label7.Size = new System.Drawing.Size(41, 17);
			this.Label7.TabIndex = 64;
			this.Label7.Text = "Status";
			// 
			// updateBtn
			// 
			this.updateBtn.BackColor = System.Drawing.Color.White;
			this.updateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.updateBtn.Location = new System.Drawing.Point(452, 8);
			this.updateBtn.Name = "updateBtn";
			this.updateBtn.Size = new System.Drawing.Size(116, 28);
			this.updateBtn.TabIndex = 63;
			this.updateBtn.Text = "Update Coverage";
			// 
			// label
			// 
			this.label.BackColor = System.Drawing.Color.ForestGreen;
			this.label.Dock = System.Windows.Forms.DockStyle.Top;
			this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label.ForeColor = System.Drawing.Color.White;
			this.label.Location = new System.Drawing.Point(0, 0);
			this.label.Name = "label";
			this.label.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label.Size = new System.Drawing.Size(224, 17);
			this.label.TabIndex = 62;
			this.label.Text = "Entities Covered";
			// 
			// statusTypeComboBx
			// 
			this.statusTypeComboBx.DisplayMember = "StatusTypeDescr";
			this.statusTypeComboBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.statusTypeComboBx.Location = new System.Drawing.Point(136, 56);
			this.statusTypeComboBx.Name = "statusTypeComboBx";
			this.statusTypeComboBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusTypeComboBx.Size = new System.Drawing.Size(121, 21);
			this.statusTypeComboBx.TabIndex = 60;
			this.statusTypeComboBx.ValueMember = "StatusTypeCode";
			// 
			// rateScheduleNameBx
			// 
			this.rateScheduleNameBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rateScheduleNameBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateScheduleNameBx.Location = new System.Drawing.Point(136, 8);
			this.rateScheduleNameBx.Name = "rateScheduleNameBx";
			this.rateScheduleNameBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.rateScheduleNameBx.Size = new System.Drawing.Size(300, 20);
			this.rateScheduleNameBx.TabIndex = 59;
			this.rateScheduleNameBx.Text = "";
			// 
			// Label6
			// 
			this.Label6.BackColor = System.Drawing.Color.Transparent;
			this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label6.Location = new System.Drawing.Point(4, 8);
			this.Label6.Name = "Label6";
			this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.Label6.Size = new System.Drawing.Size(130, 18);
			this.Label6.TabIndex = 58;
			this.Label6.Text = "Rate Schedule Name";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.entityListBx);
			this.panel1.Controls.Add(this.label);
			this.panel1.Location = new System.Drawing.Point(4, 92);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(224, 280);
			this.panel1.TabIndex = 70;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.Label8);
			this.panel2.Controls.Add(this.Label3);
			this.panel2.Controls.Add(this.endDateBx);
			this.panel2.Controls.Add(this.startDateBx);
			this.panel2.Controls.Add(this.rateScheduleNameBx);
			this.panel2.Controls.Add(this.Label6);
			this.panel2.Controls.Add(this.Label7);
			this.panel2.Controls.Add(this.statusTypeComboBx);
			this.panel2.Location = new System.Drawing.Point(4, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(440, 84);
			this.panel2.TabIndex = 71;
			// 
			// entityListBx
			// 
			this.entityListBx.BackColor = System.Drawing.Color.White;
			this.entityListBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.entityListBx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.entityListBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.entityListBx.Location = new System.Drawing.Point(0, 17);
			this.entityListBx.Name = "entityListBx";
			this.entityListBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.entityListBx.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.entityListBx.Size = new System.Drawing.Size(224, 262);
			this.entityListBx.TabIndex = 63;
			// 
			// CoverageForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(664, 378);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.updateBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CoverageForm";
			this.Text = "Rate Schedule Coverage";
			this.GroupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


	}
}
