using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using RMS_BusinessObjects;
using RMS_DALObjects;
using EditRate_Library;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for editRateFrm.
	/// </summary>
	public class EditRateForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		#region "Variables"

		RMS_Controller rmsController;

		private char inOut;
		private string rateCategory;
		private string rateType;
		private System.Windows.Forms.Panel titlePanel;
		private System.Windows.Forms.Label titleLbl;
		internal System.Windows.Forms.Panel Panel3;
		internal System.Windows.Forms.Button clearBtn;
		internal System.Windows.Forms.Button deleteRateBtn;
		internal System.Windows.Forms.Button updateRateBtn;
		internal System.Windows.Forms.Button addRateBtn;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Label rateTypeLbl;
		internal System.Windows.Forms.ComboBox rateTypeBx;
		internal System.Windows.Forms.Label rateCategryLbl;
		internal System.Windows.Forms.ComboBox rateCategryBx;
		internal System.Windows.Forms.ComboBox inOutBx;
		internal System.Windows.Forms.Label inOutLbl;
		internal System.Windows.Forms.Label rateNameLbl;
		internal System.Windows.Forms.TextBox rateNameBx;
		private System.Windows.Forms.Panel dynamicPanel;
		private System.Windows.Forms.Panel passThruPanel;
		private System.Windows.Forms.Panel codesPanel;
		private System.Windows.Forms.Panel ratePanel;
		private System.Windows.Forms.Panel staticPanel;

		private RateDAL rateData;
		private PassThrusDAL passThrusData;

		#endregion

		#region "Constructors"

		public EditRateForm(RMS_Controller in_RMSController)
		{
			InitializeComponent();	// Required for Windows Form Designer support

			setupEventHandlers();

			rmsController = in_RMSController;

			rateData = new RateDAL(ref rmsController.CodesMngr, rmsController.RateScheduleID);

      codesPanel.Controls.Add(new CodeControl(ref rmsController.CodesMngr));
			codesPanel.Height = codesPanel.Controls[0].Height + 5;

			passThrusData = new PassThrusDAL(rmsController.RateScheduleID);

			passThruPanel.Controls.Add(new PassThruControl(passThrusData.RateSchedulePassThrus));
			passThruPanel.Height = passThruPanel.Controls[0].Height;

		}

		private void setupEventHandlers()
		{
			this.addRateBtn.Click += new System.EventHandler(this.addRateBtn_Click);
			this.updateRateBtn.Click += new System.EventHandler(this.updateRateBtn_Click);
			this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
			this.deleteRateBtn.Click += new System.EventHandler(this.deleteRateBtn_Click);
		}

		#endregion


		#region "Rate Information"

		#region "InOut"

		private void inOutBx_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (inOutBx.SelectedItem.ToString() == "Inpatient")
			{	inOut = 'I';	}
			else
			{	inOut = 'O';	}

			loadRateCategoryBx();	
		}

		private void setInOut(char in_inOut)
		{
			inOut = in_inOut;

			if (inOut=='I')
			{	inOutBx.SelectedIndex = 0;	}
			else
			{	inOutBx.SelectedIndex = 1; }

			loadRateCategoryBx();
		}

		#endregion


		#region "Rate Category"

		private void rateCategryBx_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			rateCategory = rateCategryBx.SelectedItem.ToString();
			loadRateTypeBx();
		}

		private void loadRateCategoryBx()
		{
			rateCategryBx.Items.Clear();

			if (inOut=='I')
			{
				rateCategryBx.Items.Add("PerDiem");
				rateCategryBx.Items.Add("BaseRate");
			}

			rateCategryBx.Items.Add("FFS");
			rateCategryBx.Items.Add("PassThru");
			rateCategryBx.Items.Add("LessorOf");
			rateCategryBx.Items.Add("StopLoss");
			rateCategryBx.Items.Add("Floor");
			rateCategryBx.Items.Add("Ceiling");
			rateCategryBx.Items.Add("Ignore");

			rateCategryBx.SelectedIndex = 0;

			loadRateTypeBx();
			}

		private void setRateCategory(string in_RateCategory)
		{
			rateCategory = in_RateCategory;

			for(int k=0; k<rateCategryBx.Items.Count; k++)
			{
				if (rateCategryBx.Items[k].ToString() == in_RateCategory)
				{	rateCategryBx.SelectedIndex = k;	}
			}

			loadRateTypeBx();
		}

		#endregion


		#region "Rate Type"

		private void loadRateTypeBx()
		{
			rateTypeBx.Items.Clear();

			string rateCategory = rateCategryBx.SelectedItem.ToString();

			switch(rateCategory)
			{
				case "PerDiem" :	rateTypeBx.Items.Add("Per Diem"); break;

				case "BaseRate" : rateTypeBx.Items.Add("BaseRate"); break;

				case "FFS": 
					rateTypeBx.Items.Add("CaseRate");
					rateTypeBx.Items.Add("POC");
					break;

				case "PassThru":
					rateTypeBx.Items.Add("PerVisit");
					rateTypeBx.Items.Add("PerUnit");
					rateTypeBx.Items.Add("POC");
					break;

				case "StopLoss":
					rateTypeBx.Items.Add("Type I");
					rateTypeBx.Items.Add("Type II");
					break;

				case "Floor":
					rateTypeBx.Items.Add("CaseRate");
					rateTypeBx.Items.Add("POC");
					rateTypeBx.Items.Add("Per Diem");
					break;

				case "Ceiling":
					rateTypeBx.Items.Add("CaseRate");
					rateTypeBx.Items.Add("POC");
					rateTypeBx.Items.Add("Per Diem");
					break;

				case "LessorOf":
					rateTypeBx.Items.Add("LessorOf");
					break;

				case "Ignore":
					rateTypeBx.Items.Add("Ignore");
					break;

				default : break;
			}

			rateTypeBx.SelectedIndex = 0;
		}


		private void rateTypeBx_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			rateType = rateTypeBx.SelectedItem.ToString();
			loadRateEditControl();
		}

		
		private void setRateType(string in_RateType)
		{
			rateType = in_RateType;

			for(int k=0; k<rateTypeBx.Items.Count; k++)
			{
				if (rateTypeBx.Items[k].ToString() == in_RateType)
				{	rateTypeBx.SelectedIndex = k;	}
			}

			loadRateEditControl();
		}

		#endregion


		#endregion

		private void loadRateEditControl()
		{
			ratePanel.Controls.Clear();

			passThruPanel.Visible = false;

			switch(rateCategory)
			{
				case "LessorOf" : ratePanel.Controls.Add(new Edit_LessorOf_Ctrl());	break;
				case "Ignore" : ratePanel.Controls.Add(new Edit_Ignore_Ctrl()); break;

				case "BaseRate": passThruPanel.Visible = true; ratePanel.Controls.Add(new Edit_BaseRate_Ctrl()); break;
				case "StopLoss": ratePanel.Controls.Add(new Edit_StopLoss_Ctrl()); passThruPanel.Visible = true; break;
				case "PerDiem" :	ratePanel.Controls.Add(new Edit_PerDiem_Ctrl()); break;

				case "FFS" : passThruPanel.Visible = true;

				switch(rateType)
				{
					case "CaseRate" : ratePanel.Controls.Add(new Edit_FFS_CaseRate_Ctrl()); break;
					case "POC" : ratePanel.Controls.Add(new Edit_FFS_POC_Ctrl()); break;
					default : break;
				}
					break;

				case "PassThru" :

				switch(rateType)
				{
					case "PerVisit" : ratePanel.Controls.Add(new Edit_PassThru_PerVisit_Ctrl()); break;
					case "PerUnit" : ratePanel.Controls.Add(new Edit_PassThru_PerUnit_Ctrl()); break;
					case "POC" : ratePanel.Controls.Add(new Edit_PassThru_POC_Ctrl()); break;
					default : break;
				}
					break;

				case "Floor" :

				switch(rateType)
				{
					case "CaseRate" : ratePanel.Controls.Add(new Edit_Floor_CaseRate_Ctrl()); break;
					case "POC" : ratePanel.Controls.Add(new Edit_Floor_POC_Ctrl()); break;
					case "Per Diem" : ratePanel.Controls.Add(new Edit_Floor_PerDiem_Ctrl()); break;
					default : break;
				}
					break;

				case "Ceiling" :

				switch(rateType)
				{
					case "CaseRate" : ratePanel.Controls.Add(new Edit_Ceiling_CaseRate_Ctrl()); break;
					case "POC" : ratePanel.Controls.Add(new Edit_Ceiling_POC_Ctrl()); break;
					case "Per Diem" : ratePanel.Controls.Add(new Edit_Ceiling_PerDiem_Ctrl()); break;
					default : break;
				}
					break;

				default: break;
			}

			if (ratePanel.Controls.Count>0)
			{	ratePanel.Height = ratePanel.Controls[0].Height;	}

			this.adjustSize();
		}


		public void loadRate(int RateID)
		{
			RateBO rate = rateData.getRate(RateID);

			this.setInOut(rate.InOut);
			this.setRateCategory(rate.RateCategory);
			this.setRateType(rate.RateType);

      rateNameBx.Text = rate.Name;

			CodeControl codeCtrl = (CodeControl) codesPanel.Controls[0];
			codeCtrl.setCodes(rate.Codes);

			switch(rate.RateCategory)
			{
				case "LessorOf" : ((Edit_LessorOf_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_LessorOfBO) rate); break;
				case "Ignore" : ((Edit_Ignore_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_IgnoreBO) rate); break;

				case "BaseRate": ((Edit_BaseRate_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_BaseRateBO) rate); break;
				case "StopLoss": ((Edit_StopLoss_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_StopLossBO) rate); break;
				case "PerDiem" :	((Edit_PerDiem_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PerDiemBO) rate); break;

				case "FFS" :

				switch(rate.RateType)
				{
					case "CaseRate" : 
						((Edit_FFS_CaseRate_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_FFS_In_CaseRateBO) rate); 	
						break;

					case "POC" : 
						((Edit_FFS_POC_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_FFS_POCBO) rate); 
						break;

					default : break;
				}
					break;

				case "PassThru" :

				switch(rate.RateType)
				{
					case "PerVisit" : ((Edit_PassThru_PerVisit_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PassThruBO) rate); break;
					case "PerUnit" : ((Edit_PassThru_PerUnit_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PassThruBO) rate); 	break;
					case "POC" : ((Edit_PassThru_POC_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PassThruBO) rate); break;
					default : break;
				}
					break;

				case "Floor" :

				switch(rate.RateType)
				{
					case "CaseRate" : ((Edit_PassThru_PerVisit_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PassThruBO) rate); break;
					case "POC" : ((Edit_PassThru_PerUnit_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PassThruBO) rate); 	break;
					case "Per Diem" : ((Edit_PassThru_POC_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PassThruBO) rate); break;
					default : break;
				}
					break;

				case "Ceiling" :

				switch(rate.RateType)
				{
					case "CaseRate" : ((Edit_PassThru_PerVisit_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PassThruBO) rate); break;
					case "POC" : ((Edit_PassThru_PerUnit_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PassThruBO) rate); 	break;
					case "Per Diem" : ((Edit_PassThru_POC_Ctrl) ratePanel.Controls[0]).loadRate( (Rate_PassThruBO) rate); break;
					default : break;
				}
					break;

				default: break;
			}
		}


		private void adjustSize()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EditRateForm));
			this.titlePanel = new System.Windows.Forms.Panel();
			this.titleLbl = new System.Windows.Forms.Label();
			this.staticPanel = new System.Windows.Forms.Panel();
			this.Panel3 = new System.Windows.Forms.Panel();
			this.clearBtn = new System.Windows.Forms.Button();
			this.deleteRateBtn = new System.Windows.Forms.Button();
			this.updateRateBtn = new System.Windows.Forms.Button();
			this.addRateBtn = new System.Windows.Forms.Button();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.rateTypeLbl = new System.Windows.Forms.Label();
			this.rateTypeBx = new System.Windows.Forms.ComboBox();
			this.rateCategryLbl = new System.Windows.Forms.Label();
			this.rateCategryBx = new System.Windows.Forms.ComboBox();
			this.inOutBx = new System.Windows.Forms.ComboBox();
			this.inOutLbl = new System.Windows.Forms.Label();
			this.rateNameLbl = new System.Windows.Forms.Label();
			this.rateNameBx = new System.Windows.Forms.TextBox();
			this.dynamicPanel = new System.Windows.Forms.Panel();
			this.passThruPanel = new System.Windows.Forms.Panel();
			this.codesPanel = new System.Windows.Forms.Panel();
			this.ratePanel = new System.Windows.Forms.Panel();
			this.titlePanel.SuspendLayout();
			this.staticPanel.SuspendLayout();
			this.Panel3.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.dynamicPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// titlePanel
			// 
			this.titlePanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("titlePanel.BackgroundImage")));
			this.titlePanel.Controls.Add(this.titleLbl);
			this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.titlePanel.Location = new System.Drawing.Point(0, 0);
			this.titlePanel.Name = "titlePanel";
			this.titlePanel.Size = new System.Drawing.Size(480, 28);
			this.titlePanel.TabIndex = 108;
			// 
			// titleLbl
			// 
			this.titleLbl.BackColor = System.Drawing.Color.Transparent;
			this.titleLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.titleLbl.ForeColor = System.Drawing.Color.White;
			this.titleLbl.Location = new System.Drawing.Point(0, 0);
			this.titleLbl.Name = "titleLbl";
			this.titleLbl.Size = new System.Drawing.Size(480, 28);
			this.titleLbl.TabIndex = 0;
			this.titleLbl.Text = "Rate Editor";
			// 
			// staticPanel
			// 
			this.staticPanel.Controls.Add(this.Panel3);
			this.staticPanel.Controls.Add(this.GroupBox1);
			this.staticPanel.Controls.Add(this.rateNameLbl);
			this.staticPanel.Controls.Add(this.rateNameBx);
			this.staticPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.staticPanel.Location = new System.Drawing.Point(0, 28);
			this.staticPanel.Name = "staticPanel";
			this.staticPanel.Size = new System.Drawing.Size(480, 128);
			this.staticPanel.TabIndex = 109;
			// 
			// Panel3
			// 
			this.Panel3.BackColor = System.Drawing.Color.Transparent;
			this.Panel3.Controls.Add(this.clearBtn);
			this.Panel3.Controls.Add(this.deleteRateBtn);
			this.Panel3.Controls.Add(this.updateRateBtn);
			this.Panel3.Controls.Add(this.addRateBtn);
			this.Panel3.Location = new System.Drawing.Point(224, 8);
			this.Panel3.Name = "Panel3";
			this.Panel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Panel3.Size = new System.Drawing.Size(240, 64);
			this.Panel3.TabIndex = 98;
			// 
			// clearBtn
			// 
			this.clearBtn.BackColor = System.Drawing.Color.White;
			this.clearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.clearBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.clearBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearBtn.Image")));
			this.clearBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.clearBtn.Location = new System.Drawing.Point(120, 32);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new System.Drawing.Size(116, 24);
			this.clearBtn.TabIndex = 14;
			this.clearBtn.Text = "Clear";
			this.clearBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// deleteRateBtn
			// 
			this.deleteRateBtn.BackColor = System.Drawing.Color.White;
			this.deleteRateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteRateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.deleteRateBtn.Image = ((System.Drawing.Image)(resources.GetObject("deleteRateBtn.Image")));
			this.deleteRateBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.deleteRateBtn.Location = new System.Drawing.Point(120, 4);
			this.deleteRateBtn.Name = "deleteRateBtn";
			this.deleteRateBtn.Size = new System.Drawing.Size(116, 24);
			this.deleteRateBtn.TabIndex = 13;
			this.deleteRateBtn.Text = "Delete Rate";
			this.deleteRateBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// updateRateBtn
			// 
			this.updateRateBtn.BackColor = System.Drawing.Color.White;
			this.updateRateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.updateRateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.updateRateBtn.Image = ((System.Drawing.Image)(resources.GetObject("updateRateBtn.Image")));
			this.updateRateBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.updateRateBtn.Location = new System.Drawing.Point(0, 32);
			this.updateRateBtn.Name = "updateRateBtn";
			this.updateRateBtn.Size = new System.Drawing.Size(116, 24);
			this.updateRateBtn.TabIndex = 12;
			this.updateRateBtn.Text = "Update Rate";
			this.updateRateBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// addRateBtn
			// 
			this.addRateBtn.BackColor = System.Drawing.Color.White;
			this.addRateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addRateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.addRateBtn.Image = ((System.Drawing.Image)(resources.GetObject("addRateBtn.Image")));
			this.addRateBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.addRateBtn.Location = new System.Drawing.Point(0, 4);
			this.addRateBtn.Name = "addRateBtn";
			this.addRateBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.addRateBtn.Size = new System.Drawing.Size(116, 24);
			this.addRateBtn.TabIndex = 11;
			this.addRateBtn.Text = "Add Rate";
			this.addRateBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// GroupBox1
			// 
			this.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.GroupBox1.Controls.Add(this.rateTypeLbl);
			this.GroupBox1.Controls.Add(this.rateTypeBx);
			this.GroupBox1.Controls.Add(this.rateCategryLbl);
			this.GroupBox1.Controls.Add(this.rateCategryBx);
			this.GroupBox1.Controls.Add(this.inOutBx);
			this.GroupBox1.Controls.Add(this.inOutLbl);
			this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GroupBox1.Location = new System.Drawing.Point(8, 8);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(208, 92);
			this.GroupBox1.TabIndex = 99;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Rate Information";
			// 
			// rateTypeLbl
			// 
			this.rateTypeLbl.BackColor = System.Drawing.Color.Transparent;
			this.rateTypeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateTypeLbl.Location = new System.Drawing.Point(4, 68);
			this.rateTypeLbl.Name = "rateTypeLbl";
			this.rateTypeLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.rateTypeLbl.Size = new System.Drawing.Size(88, 14);
			this.rateTypeLbl.TabIndex = 95;
			this.rateTypeLbl.Text = "Rate Type";
			// 
			// rateTypeBx
			// 
			this.rateTypeBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateTypeBx.Location = new System.Drawing.Point(92, 64);
			this.rateTypeBx.Name = "rateTypeBx";
			this.rateTypeBx.Size = new System.Drawing.Size(108, 21);
			this.rateTypeBx.TabIndex = 94;
			this.rateTypeBx.SelectedIndexChanged += new System.EventHandler(this.rateTypeBx_SelectedIndexChanged);
			// 
			// rateCategryLbl
			// 
			this.rateCategryLbl.BackColor = System.Drawing.Color.Transparent;
			this.rateCategryLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateCategryLbl.Location = new System.Drawing.Point(4, 44);
			this.rateCategryLbl.Name = "rateCategryLbl";
			this.rateCategryLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.rateCategryLbl.Size = new System.Drawing.Size(88, 14);
			this.rateCategryLbl.TabIndex = 93;
			this.rateCategryLbl.Text = "Rate Category";
			// 
			// rateCategryBx
			// 
			this.rateCategryBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateCategryBx.Location = new System.Drawing.Point(92, 40);
			this.rateCategryBx.Name = "rateCategryBx";
			this.rateCategryBx.Size = new System.Drawing.Size(108, 21);
			this.rateCategryBx.TabIndex = 1;
			this.rateCategryBx.SelectedIndexChanged += new System.EventHandler(this.rateCategryBx_SelectedIndexChanged);
			// 
			// inOutBx
			// 
			this.inOutBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.inOutBx.Items.AddRange(new object[] {
																								 "Inpatient",
																								 "Outpatient"});
			this.inOutBx.Location = new System.Drawing.Point(92, 16);
			this.inOutBx.Name = "inOutBx";
			this.inOutBx.Size = new System.Drawing.Size(109, 21);
			this.inOutBx.TabIndex = 0;
			this.inOutBx.SelectedIndexChanged += new System.EventHandler(this.inOutBx_SelectedIndexChanged);
			// 
			// inOutLbl
			// 
			this.inOutLbl.BackColor = System.Drawing.Color.Transparent;
			this.inOutLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.inOutLbl.Location = new System.Drawing.Point(48, 20);
			this.inOutLbl.Name = "inOutLbl";
			this.inOutLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.inOutLbl.Size = new System.Drawing.Size(44, 14);
			this.inOutLbl.TabIndex = 92;
			this.inOutLbl.Text = "In/Out";
			// 
			// rateNameLbl
			// 
			this.rateNameLbl.BackColor = System.Drawing.Color.Transparent;
			this.rateNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateNameLbl.Location = new System.Drawing.Point(16, 102);
			this.rateNameLbl.Name = "rateNameLbl";
			this.rateNameLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.rateNameLbl.Size = new System.Drawing.Size(62, 14);
			this.rateNameLbl.TabIndex = 101;
			this.rateNameLbl.Text = "Rate Name";
			// 
			// rateNameBx
			// 
			this.rateNameBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rateNameBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateNameBx.Location = new System.Drawing.Point(81, 102);
			this.rateNameBx.Name = "rateNameBx";
			this.rateNameBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.rateNameBx.Size = new System.Drawing.Size(320, 20);
			this.rateNameBx.TabIndex = 100;
			this.rateNameBx.Text = "";
			// 
			// dynamicPanel
			// 
			this.dynamicPanel.Controls.Add(this.passThruPanel);
			this.dynamicPanel.Controls.Add(this.codesPanel);
			this.dynamicPanel.Controls.Add(this.ratePanel);
			this.dynamicPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dynamicPanel.Location = new System.Drawing.Point(0, 156);
			this.dynamicPanel.Name = "dynamicPanel";
			this.dynamicPanel.Size = new System.Drawing.Size(480, 498);
			this.dynamicPanel.TabIndex = 110;
			// 
			// passThruPanel
			// 
			this.passThruPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.passThruPanel.Location = new System.Drawing.Point(0, 128);
			this.passThruPanel.Name = "passThruPanel";
			this.passThruPanel.Size = new System.Drawing.Size(480, 370);
			this.passThruPanel.TabIndex = 107;
			// 
			// codesPanel
			// 
			this.codesPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.codesPanel.Location = new System.Drawing.Point(0, 68);
			this.codesPanel.Name = "codesPanel";
			this.codesPanel.Size = new System.Drawing.Size(480, 60);
			this.codesPanel.TabIndex = 106;
			// 
			// ratePanel
			// 
			this.ratePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.ratePanel.Location = new System.Drawing.Point(0, 0);
			this.ratePanel.Name = "ratePanel";
			this.ratePanel.Size = new System.Drawing.Size(480, 68);
			this.ratePanel.TabIndex = 105;
			// 
			// EditRateForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(480, 654);
			this.Controls.Add(this.dynamicPanel);
			this.Controls.Add(this.staticPanel);
			this.Controls.Add(this.titlePanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EditRateForm";
			this.Text = "Rate Editor";
			this.titlePanel.ResumeLayout(false);
			this.staticPanel.ResumeLayout(false);
			this.Panel3.ResumeLayout(false);
			this.GroupBox1.ResumeLayout(false);
			this.dynamicPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region "Buttons"

		private RateBO setupRateForDB()
		{
			Edit_Rate_Ctrl rateCtrl = (Edit_Rate_Ctrl) ratePanel.Controls[0];
			RateBO rate = (RateBO) rateCtrl.getRate();

			rate.RateScheduleID = this.rmsController.RateScheduleID;
			rate.Name = rateNameBx.Text;
			rate.InOut = inOut;
			rate.RateCategory = rateCategory;
			rate.RateType = rateType;

			CodeControl codeCtrl = (CodeControl) codesPanel.Controls[0];
			rate.Codes = codeCtrl.getCodes();

			if (passThruPanel.Visible)
			{
				((Rate_W_PassThruBO) rate).PassThrus = ((PassThruControl) passThruPanel.Controls[0]).getPassThrus();
			}
		
			return rate;
		}


		private void addRateBtn_Click(object sender, System.EventArgs e)
		{
			RateBO rate = this.setupRateForDB();

			rateData.insertRate(rate);

			Clear();
			rmsController.RateScheduleChanged();
		}


		private void updateRateBtn_Click(object sender, System.EventArgs e)
		{
			RateBO rate = this.setupRateForDB();
			rate.ID = rmsController.RateID;

			rateData.updateRate(rate);		
			Clear();
			rmsController.RateScheduleChanged();
		}


		private void deleteRateBtn_Click(object sender, System.EventArgs e)
		{
			RateBO rate = this.setupRateForDB();
			rate.ID = rmsController.RateID;


			rateData.deleteRate(rate);	
			Clear();	
			rmsController.RateScheduleChanged();
		}

		private void clearBtn_Click(object sender, System.EventArgs e)
		{	
			Clear();		
		}

		private void Clear()
		{
			this.rateNameBx.Text = "";

			Edit_Rate_Ctrl rateCtrl = (Edit_Rate_Ctrl) ratePanel.Controls[0];
			rateCtrl.Clear();

			CodeControl codeCtrl = (CodeControl) codesPanel.Controls[0];
			codeCtrl.Clear();
		}

		#endregion






	}
}
