using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using RMS_BusinessObjects;
using RMS_DALObjects;


namespace RMS_v3
{
	/// <summary>
	/// The contract control is the user interface for adding and removing contracts and rate schedules.
	/// </summary>
	/// 

	public class Contract_Control : System.Windows.Forms.UserControl
	{
		#region "Variables"

		#region "Designer Variables"

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel buttonPanel;
		private System.Windows.Forms.PictureBox removeRateSchedulePicBx;
		private System.Windows.Forms.PictureBox addRateSchedulePicBx;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox removeContractPicBx;
		private System.Windows.Forms.PictureBox addContractPicBx;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label statusLbl;
		private System.Windows.Forms.ComboBox statusTypeComboBx;
		private System.Windows.Forms.TreeView contractsTree;

		private RMS_Controller rmsController;

		#endregion

		#region "Constructors"

		public Contract_Control(RMS_Controller inRMSController)
		{
			rmsController = inRMSController;

			InitializeComponent();

			loadEvents();
	
			setupStatusComboBox();
			loadContractsTreeView();
		}

		#region "Events"

		private void loadEvents()
		{
			// Load Events
			this.contractsTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.contractsTree_AfterSelect);

			this.statusTypeComboBx.SelectedIndexChanged += new System.EventHandler(this.statusTypeComboBx_SelectedIndexChanged);

			this.addContractPicBx.Click += new System.EventHandler(this.addContractPicBx_Click);
			this.removeContractPicBx.Click += new System.EventHandler(this.removeContractPicBx_Click);

			this.addRateSchedulePicBx.Click += new System.EventHandler(this.addRateSchedulePicBx_Click);
			this.removeRateSchedulePicBx.Click += new System.EventHandler(this.removeRateSchedulePicBx_Click);
		}

		#endregion

		#region "UI Setup"

		private void setupStatusComboBox()
		{
			StatusDAL myStatusDAL = new StatusDAL();

			DataTable statusTable = myStatusDAL.getStatusTable();

			statusTypeComboBx.DisplayMember = "Status";
			statusTypeComboBx.ValueMember = "StatusTypeCode";

			statusTypeComboBx.DataSource = statusTable;
			statusTypeComboBx.SelectedIndex = 0;
		}


		#endregion

		#endregion

		private void loadContractsTreeView()
		{
			contractsTree.Nodes.Clear();

			ContractDAL contractData = new ContractDAL();

			Contract_Collection contracts = contractData.getContractsByStatusType(statusTypeComboBx.SelectedValue.ToString());

			ContractBO contract;
			TreeNode contractNode, rateScheduleNode;
			
			for(int c=0; c<contracts.Count(); c++)
			{
				contract = contracts.getContractAt(c);

				contractNode = new TreeNode(contract.ToString());

				for(int r=0; r<contract.rateScheduleCount(); r++)
				{
					rateScheduleNode = new TreeNode(contract.getRateScheduleStringAt(r));

					contractNode.Nodes.Add(rateScheduleNode);
				}

				contractsTree.Nodes.Add(contractNode);
			}

			contractsTree.SelectedNode = contractsTree.Nodes[0];
		}


		int getIDFromNode(TreeNode selectedNode)
		{
			// Extract the ID through string manipulation
			string selectedNodeTxt = selectedNode.Text;
			int beginIndex = selectedNodeTxt.LastIndexOf('#') + 1;
			int length = selectedNodeTxt.Length - beginIndex - 1;
			
			return Convert.ToInt16(selectedNodeTxt.Substring(beginIndex, length));
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Contract_Control));
			this.panel1 = new System.Windows.Forms.Panel();
			this.statusTypeComboBx = new System.Windows.Forms.ComboBox();
			this.statusLbl = new System.Windows.Forms.Label();
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.removeRateSchedulePicBx = new System.Windows.Forms.PictureBox();
			this.addRateSchedulePicBx = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.removeContractPicBx = new System.Windows.Forms.PictureBox();
			this.addContractPicBx = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.contractsTree = new System.Windows.Forms.TreeView();
			this.panel1.SuspendLayout();
			this.buttonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.Add(this.statusTypeComboBx);
			this.panel1.Controls.Add(this.statusLbl);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 27);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(496, 37);
			this.panel1.TabIndex = 8;
			// 
			// statusTypeComboBx
			// 
			this.statusTypeComboBx.Dock = System.Windows.Forms.DockStyle.Top;
			this.statusTypeComboBx.Location = new System.Drawing.Point(0, 16);
			this.statusTypeComboBx.Name = "statusTypeComboBx";
			this.statusTypeComboBx.Size = new System.Drawing.Size(496, 21);
			this.statusTypeComboBx.TabIndex = 7;
			
			// 
			// statusLbl
			// 
			this.statusLbl.BackColor = System.Drawing.Color.Transparent;
			this.statusLbl.Dock = System.Windows.Forms.DockStyle.Top;
			this.statusLbl.ForeColor = System.Drawing.Color.White;
			this.statusLbl.Location = new System.Drawing.Point(0, 0);
			this.statusLbl.Name = "statusLbl";
			this.statusLbl.Size = new System.Drawing.Size(496, 16);
			this.statusLbl.TabIndex = 6;
			this.statusLbl.Text = "Now Viewing:";
			// 
			// buttonPanel
			// 
			this.buttonPanel.BackColor = System.Drawing.Color.WhiteSmoke;
			this.buttonPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPanel.BackgroundImage")));
			this.buttonPanel.Controls.Add(this.removeRateSchedulePicBx);
			this.buttonPanel.Controls.Add(this.addRateSchedulePicBx);
			this.buttonPanel.Controls.Add(this.pictureBox2);
			this.buttonPanel.Controls.Add(this.removeContractPicBx);
			this.buttonPanel.Controls.Add(this.addContractPicBx);
			this.buttonPanel.Controls.Add(this.pictureBox1);
			this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.buttonPanel.DockPadding.All = 2;
			this.buttonPanel.Location = new System.Drawing.Point(0, 0);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(496, 27);
			this.buttonPanel.TabIndex = 5;
			// 
			// removeRateSchedulePicBx
			// 
			this.removeRateSchedulePicBx.BackColor = System.Drawing.SystemColors.Control;
			this.removeRateSchedulePicBx.Dock = System.Windows.Forms.DockStyle.Left;
			this.removeRateSchedulePicBx.Image = ((System.Drawing.Image)(resources.GetObject("removeRateSchedulePicBx.Image")));
			this.removeRateSchedulePicBx.Location = new System.Drawing.Point(292, 2);
			this.removeRateSchedulePicBx.Name = "removeRateSchedulePicBx";
			this.removeRateSchedulePicBx.Size = new System.Drawing.Size(54, 25);
			this.removeRateSchedulePicBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.removeRateSchedulePicBx.TabIndex = 19;
			this.removeRateSchedulePicBx.TabStop = false;
			// 
			// addRateSchedulePicBx
			// 
			this.addRateSchedulePicBx.BackColor = System.Drawing.SystemColors.Control;
			this.addRateSchedulePicBx.Dock = System.Windows.Forms.DockStyle.Left;
			this.addRateSchedulePicBx.Image = ((System.Drawing.Image)(resources.GetObject("addRateSchedulePicBx.Image")));
			this.addRateSchedulePicBx.Location = new System.Drawing.Point(263, 2);
			this.addRateSchedulePicBx.Name = "addRateSchedulePicBx";
			this.addRateSchedulePicBx.Size = new System.Drawing.Size(29, 25);
			this.addRateSchedulePicBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.addRateSchedulePicBx.TabIndex = 18;
			this.addRateSchedulePicBx.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.SystemColors.Control;
			this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(158, 2);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(105, 25);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 17;
			this.pictureBox2.TabStop = false;
			// 
			// removeContractPicBx
			// 
			this.removeContractPicBx.BackColor = System.Drawing.SystemColors.Control;
			this.removeContractPicBx.Dock = System.Windows.Forms.DockStyle.Left;
			this.removeContractPicBx.Image = ((System.Drawing.Image)(resources.GetObject("removeContractPicBx.Image")));
			this.removeContractPicBx.Location = new System.Drawing.Point(104, 2);
			this.removeContractPicBx.Name = "removeContractPicBx";
			this.removeContractPicBx.Size = new System.Drawing.Size(54, 25);
			this.removeContractPicBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.removeContractPicBx.TabIndex = 16;
			this.removeContractPicBx.TabStop = false;
			// 
			// addContractPicBx
			// 
			this.addContractPicBx.BackColor = System.Drawing.SystemColors.Control;
			this.addContractPicBx.Dock = System.Windows.Forms.DockStyle.Left;
			this.addContractPicBx.Image = ((System.Drawing.Image)(resources.GetObject("addContractPicBx.Image")));
			this.addContractPicBx.Location = new System.Drawing.Point(75, 2);
			this.addContractPicBx.Name = "addContractPicBx";
			this.addContractPicBx.Size = new System.Drawing.Size(29, 25);
			this.addContractPicBx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.addContractPicBx.TabIndex = 14;
			this.addContractPicBx.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(2, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(73, 25);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			// 
			// contractsTree
			// 
			this.contractsTree.BackColor = System.Drawing.Color.SteelBlue;
			this.contractsTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.contractsTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contractsTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.contractsTree.ForeColor = System.Drawing.Color.White;
			this.contractsTree.FullRowSelect = true;
			this.contractsTree.ImageIndex = -1;
			this.contractsTree.Location = new System.Drawing.Point(0, 64);
			this.contractsTree.Name = "contractsTree";
			this.contractsTree.SelectedImageIndex = -1;
			this.contractsTree.Size = new System.Drawing.Size(496, 416);
			this.contractsTree.TabIndex = 9;
			// 
			// Contract_Control
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.contractsTree);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.buttonPanel);
			this.Name = "Contract_Control";
			this.Size = new System.Drawing.Size(496, 480);
			this.panel1.ResumeLayout(false);
			this.buttonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void statusTypeComboBx_SelectedIndexChanged(object sender, System.EventArgs e)
		{	
			loadContractsTreeView();	

			setButtonsVisible();
		}

		private void contractsTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (contractsTree.SelectedNode.Parent==null)  // Then Contract Selected
			{
				removeRateSchedulePicBx.Visible = false;
        rmsController.ContractID = getIDFromNode(contractsTree.SelectedNode);
			}
			else // Then Rate Schedule selected
			{
				removeRateSchedulePicBx.Visible = true;

				// Set Rate Schedule as selected by RMS
				rmsController.ContractID = getIDFromNode(contractsTree.SelectedNode.Parent);
				rmsController.RateScheduleID = getIDFromNode(contractsTree.SelectedNode);
			}

			setButtonsVisible();
		}



		#region "Buttons"

		public void setButtonsVisible()
		{
			if (statusTypeComboBx.Text=="Development")
			{
				addContractPicBx.Visible = true;
				removeContractPicBx.Visible = true;

				addRateSchedulePicBx.Visible=true;

				if (contractsTree.SelectedNode.Parent==null)  // Then Contract Selected
				{	removeRateSchedulePicBx.Visible = false;	}
				else
				{	removeRateSchedulePicBx.Visible = true;	}
			}
			else
			{
				addContractPicBx.Visible = false;
				removeContractPicBx.Visible = false;

				addRateSchedulePicBx.Visible=false;
				removeRateSchedulePicBx.Visible = false;
			}
		}

		#region "Contract Buttons"

    private void handleContractAdded(object sender, System.EventArgs e)
		{
			loadContractsTreeView();
		}

		// ********************************* ADD CONTRACT
		private void addContractPicBx_Click(object sender, System.EventArgs e)
		{
			NewContractForm newContractFrm = new NewContractForm();
			newContractFrm.ContractAdded += new System.EventHandler(this.handleContractAdded);

			newContractFrm.Show();	
		}

		// ********************************* REMOVE CONTRACT
		private void removeContractPicBx_Click(object sender, System.EventArgs e)
		{
			ContractDAL contractData = new ContractDAL();
			ContractBO contract = contractData.getContract(rmsController.ContractID);

			contractData.deleteContract(contract);

			loadContractsTreeView();
		}

		#endregion

		#region "Rate Schedule Buttons"

		private void handleRateScheduleAdded(object sender, System.EventArgs e)
		{
			loadContractsTreeView();
		}

		// ********************************* ADD RATE SCHEDULE
		private void addRateSchedulePicBx_Click(object sender, System.EventArgs e)
		{
			NewRateScheduleForm newRateScheduleFrm = new NewRateScheduleForm(rmsController, statusTypeComboBx.SelectedValue.ToString());
			newRateScheduleFrm.RateScheduleAdded += new System.EventHandler(this.handleRateScheduleAdded);

			newRateScheduleFrm.Show();			
		}

		// ********************************* REMOVE RATE SCHEDULE
		private void removeRateSchedulePicBx_Click(object sender, System.EventArgs e)
		{
			RateScheduleDAL rateScheduleData = new RateScheduleDAL();

			rateScheduleData.deleteRateSchedule(rmsController.RateScheduleID);	

			loadContractsTreeView();
		}

		#endregion




		#endregion


	}
}
