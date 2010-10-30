using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

using RMS_DALObjects;

namespace RMS_v3
{
	/// <summary>
	/// Summary description for ManageDataSetsForm.
	/// </summary>
	public class ManageDataSetsForm : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Button deleteTableBtn;
		internal System.Windows.Forms.Label tableLbl;
		internal System.Windows.Forms.RadioButton encCptRadioBtn;
		internal System.Windows.Forms.RadioButton detailRadioBtn;
		internal System.Windows.Forms.RadioButton encRadioBtn;
		internal System.Windows.Forms.Label progLbl;
		internal System.Windows.Forms.ProgressBar progBar;
		internal System.Windows.Forms.Label exceptionLbl;
		internal System.Windows.Forms.ComboBox accessTable;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.Button importDataBtn;
		internal System.Windows.Forms.Button createDatasetBtn;
		internal System.Windows.Forms.GroupBox GroupBox8;
		internal System.Windows.Forms.Label accessLbl;
		internal System.Windows.Forms.Button findAccessBtn;
		internal System.Windows.Forms.Button deleteBtn;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		#region "Variables"

		baseDALObject data = new baseDALObject();
		internal System.Windows.Forms.ListBox datasetBx;
		AccessDALObject accessData = new AccessDALObject();

		#endregion


		#region "Constructors"

		public ManageDataSetsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			updateDatasetBx();
		}


		#endregion

		#region "Methods"

		private void updateDatasetBx()
		{
			DataSet oDataSet = data.getDataSet("SELECT * FROM Dataset ORDER BY DatasetName ASC");
			
			datasetBx.DisplayMember = "DatasetName";
			datasetBx.ValueMember = "DatasetSeqNum";
			datasetBx.DataSource = oDataSet.Tables[0];
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.deleteTableBtn = new System.Windows.Forms.Button();
			this.tableLbl = new System.Windows.Forms.Label();
			this.encCptRadioBtn = new System.Windows.Forms.RadioButton();
			this.detailRadioBtn = new System.Windows.Forms.RadioButton();
			this.encRadioBtn = new System.Windows.Forms.RadioButton();
			this.progLbl = new System.Windows.Forms.Label();
			this.progBar = new System.Windows.Forms.ProgressBar();
			this.exceptionLbl = new System.Windows.Forms.Label();
			this.accessTable = new System.Windows.Forms.ComboBox();
			this.Label7 = new System.Windows.Forms.Label();
			this.importDataBtn = new System.Windows.Forms.Button();
			this.createDatasetBtn = new System.Windows.Forms.Button();
			this.GroupBox8 = new System.Windows.Forms.GroupBox();
			this.accessLbl = new System.Windows.Forms.Label();
			this.findAccessBtn = new System.Windows.Forms.Button();
			this.deleteBtn = new System.Windows.Forms.Button();
			this.Label1 = new System.Windows.Forms.Label();
			this.datasetBx = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.GroupBox1.SuspendLayout();
			this.GroupBox8.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupBox1
			// 
			this.GroupBox1.BackColor = System.Drawing.Color.Transparent;
			this.GroupBox1.Controls.Add(this.deleteTableBtn);
			this.GroupBox1.Controls.Add(this.tableLbl);
			this.GroupBox1.Controls.Add(this.encCptRadioBtn);
			this.GroupBox1.Controls.Add(this.detailRadioBtn);
			this.GroupBox1.Controls.Add(this.encRadioBtn);
			this.GroupBox1.Controls.Add(this.progLbl);
			this.GroupBox1.Controls.Add(this.progBar);
			this.GroupBox1.Controls.Add(this.exceptionLbl);
			this.GroupBox1.Controls.Add(this.accessTable);
			this.GroupBox1.Controls.Add(this.Label7);
			this.GroupBox1.Controls.Add(this.importDataBtn);
			this.GroupBox1.Location = new System.Drawing.Point(184, 72);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(464, 152);
			this.GroupBox1.TabIndex = 33;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Manage Tables";
			// 
			// deleteTableBtn
			// 
			this.deleteTableBtn.BackColor = System.Drawing.Color.White;
			this.deleteTableBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteTableBtn.Location = new System.Drawing.Point(340, 76);
			this.deleteTableBtn.Name = "deleteTableBtn";
			this.deleteTableBtn.Size = new System.Drawing.Size(106, 22);
			this.deleteTableBtn.TabIndex = 36;
			this.deleteTableBtn.Text = "Delete Table";
			// 
			// tableLbl
			// 
			this.tableLbl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tableLbl.Location = new System.Drawing.Point(4, 48);
			this.tableLbl.Name = "tableLbl";
			this.tableLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.tableLbl.Size = new System.Drawing.Size(131, 14);
			this.tableLbl.TabIndex = 35;
			// 
			// encCptRadioBtn
			// 
			this.encCptRadioBtn.Location = new System.Drawing.Point(148, 22);
			this.encCptRadioBtn.Name = "encCptRadioBtn";
			this.encCptRadioBtn.Size = new System.Drawing.Size(80, 16);
			this.encCptRadioBtn.TabIndex = 34;
			this.encCptRadioBtn.Text = "EncCpt";
			// 
			// detailRadioBtn
			// 
			this.detailRadioBtn.Location = new System.Drawing.Point(90, 22);
			this.detailRadioBtn.Name = "detailRadioBtn";
			this.detailRadioBtn.Size = new System.Drawing.Size(56, 16);
			this.detailRadioBtn.TabIndex = 33;
			this.detailRadioBtn.Text = "Detail";
			// 
			// encRadioBtn
			// 
			this.encRadioBtn.Location = new System.Drawing.Point(8, 22);
			this.encRadioBtn.Name = "encRadioBtn";
			this.encRadioBtn.Size = new System.Drawing.Size(80, 16);
			this.encRadioBtn.TabIndex = 32;
			this.encRadioBtn.Text = "Encounter";
			// 
			// progLbl
			// 
			this.progLbl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.progLbl.Location = new System.Drawing.Point(132, 48);
			this.progLbl.Name = "progLbl";
			this.progLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.progLbl.Size = new System.Drawing.Size(205, 14);
			this.progLbl.TabIndex = 19;
			// 
			// progBar
			// 
			this.progBar.Location = new System.Drawing.Point(8, 40);
			this.progBar.Name = "progBar";
			this.progBar.Size = new System.Drawing.Size(437, 7);
			this.progBar.TabIndex = 17;
			// 
			// exceptionLbl
			// 
			this.exceptionLbl.Location = new System.Drawing.Point(8, 86);
			this.exceptionLbl.Name = "exceptionLbl";
			this.exceptionLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.exceptionLbl.Size = new System.Drawing.Size(332, 50);
			this.exceptionLbl.TabIndex = 18;
			// 
			// accessTable
			// 
			this.accessTable.Location = new System.Drawing.Point(108, 62);
			this.accessTable.Name = "accessTable";
			this.accessTable.Size = new System.Drawing.Size(232, 21);
			this.accessTable.TabIndex = 31;
			// 
			// Label7
			// 
			this.Label7.BackColor = System.Drawing.Color.Transparent;
			this.Label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label7.Location = new System.Drawing.Point(8, 66);
			this.Label7.Name = "Label7";
			this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label7.Size = new System.Drawing.Size(96, 16);
			this.Label7.TabIndex = 30;
			this.Label7.Text = "Access Table:";
			// 
			// importDataBtn
			// 
			this.importDataBtn.BackColor = System.Drawing.Color.White;
			this.importDataBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.importDataBtn.Location = new System.Drawing.Point(340, 52);
			this.importDataBtn.Name = "importDataBtn";
			this.importDataBtn.Size = new System.Drawing.Size(106, 22);
			this.importDataBtn.TabIndex = 29;
			this.importDataBtn.Text = "Import Data";
			this.importDataBtn.Click += new System.EventHandler(this.importDataBtn_Click);
			// 
			// createDatasetBtn
			// 
			this.createDatasetBtn.BackColor = System.Drawing.Color.White;
			this.createDatasetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.createDatasetBtn.Location = new System.Drawing.Point(184, 16);
			this.createDatasetBtn.Name = "createDatasetBtn";
			this.createDatasetBtn.Size = new System.Drawing.Size(100, 20);
			this.createDatasetBtn.TabIndex = 30;
			this.createDatasetBtn.Text = "Create Dataset";
			this.createDatasetBtn.Click += new System.EventHandler(this.createDatasetBtn_Click);
			// 
			// GroupBox8
			// 
			this.GroupBox8.BackColor = System.Drawing.Color.Transparent;
			this.GroupBox8.Controls.Add(this.accessLbl);
			this.GroupBox8.Controls.Add(this.findAccessBtn);
			this.GroupBox8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.GroupBox8.Location = new System.Drawing.Point(288, 16);
			this.GroupBox8.Name = "GroupBox8";
			this.GroupBox8.Size = new System.Drawing.Size(360, 44);
			this.GroupBox8.TabIndex = 34;
			this.GroupBox8.TabStop = false;
			this.GroupBox8.Text = "Access Database";
			// 
			// accessLbl
			// 
			this.accessLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.accessLbl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.accessLbl.Location = new System.Drawing.Point(4, 20);
			this.accessLbl.Name = "accessLbl";
			this.accessLbl.Size = new System.Drawing.Size(304, 16);
			this.accessLbl.TabIndex = 24;
			// 
			// findAccessBtn
			// 
			this.findAccessBtn.BackColor = System.Drawing.Color.White;
			this.findAccessBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.findAccessBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.findAccessBtn.Location = new System.Drawing.Point(312, 20);
			this.findAccessBtn.Name = "findAccessBtn";
			this.findAccessBtn.Size = new System.Drawing.Size(40, 20);
			this.findAccessBtn.TabIndex = 23;
			this.findAccessBtn.Text = "Find";
			this.findAccessBtn.Click += new System.EventHandler(this.findAccessBtn_Click);
			// 
			// deleteBtn
			// 
			this.deleteBtn.BackColor = System.Drawing.Color.White;
			this.deleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteBtn.Location = new System.Drawing.Point(184, 40);
			this.deleteBtn.Name = "deleteBtn";
			this.deleteBtn.Size = new System.Drawing.Size(100, 20);
			this.deleteBtn.TabIndex = 32;
			this.deleteBtn.Text = "Delete Dataset";
			this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.Color.Transparent;
			this.Label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.Color.ForestGreen;
			this.Label1.Location = new System.Drawing.Point(-180, 17);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(102, 20);
			this.Label1.TabIndex = 36;
			this.Label1.Text = "Datasets";
			// 
			// datasetBx
			// 
			this.datasetBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.datasetBx.Location = new System.Drawing.Point(8, 32);
			this.datasetBx.Name = "datasetBx";
			this.datasetBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.datasetBx.Size = new System.Drawing.Size(171, 210);
			this.datasetBx.TabIndex = 39;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.ForestGreen;
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label2.Size = new System.Drawing.Size(101, 20);
			this.label2.TabIndex = 38;
			this.label2.Text = "Datasets";
			// 
			// ManageDataSetsForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(664, 254);
			this.Controls.Add(this.datasetBx);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.GroupBox8);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.createDatasetBtn);
			this.Controls.Add(this.deleteBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ManageDataSetsForm";
			this.Text = "ManageDataSetsForm";
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox8.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void findAccessBtn_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.ShowDialog();

      accessLbl.Text = openDialog.FileName;

			accessData.setDynamicAccessDB(openDialog.FileName);

      accessTable.DisplayMember = "TABLE_NAME";
      accessTable.DataSource = accessData.getTables();
		}


		private void createDatasetBtn_Click(object sender, System.EventArgs e)
		{
			NewDataSetForm newDSFrm = new NewDataSetForm();

			newDSFrm.NewDataSet += new System.EventHandler(this.DataSetAdded);

			newDSFrm.Show();
		}


		private void DataSetAdded(object sender, System.EventArgs e)
		{
				updateDatasetBx();
		}


		private void deleteBtn_Click(object sender, System.EventArgs e)
		{
			string dataSetID = datasetBx.SelectedValue.ToString();

      deleteTable("EncntrCPT", dataSetID);
      deleteTable("Detl", dataSetID);
      deleteTable("Encntr", dataSetID);

			SqlParameter[] sqlParams = data.getParameters("DeleteDataset");
			sqlParams[0].Value = dataSetID;
			data.executeDelete("DeleteDataset", sqlParams);

      tableLbl.Text = "";
      tableLbl.Refresh();

      progLbl.Text = "Deletion Done";
      progLbl.Refresh();
      updateDatasetBx();
		}


		int count = 0;
		int barCount = 0;

		private void setupProgBar(int count)
		{
			progBar.Minimum = 0;
			progBar.Maximum = count;
      
			progBar.Step = 300;
			progBar.Refresh();
			progLbl.Text = "0 of " + count;
			progLbl.Refresh();
		}

		private void updateProgBar(int count)
		{
      progBar.PerformStep();
      progBar.Refresh();
      
			progLbl.Text = count + " of " + progBar.Maximum;
			progLbl.Refresh();
    }
							

		private void deleteTable(string tableName, string datasetID)
		{
			int count = 0; int barcount = 0;
			DataSet oDataSet;
			SqlParameter[] sqlParams;

			switch(tableName)
			{
				case "Encntr" :

					oDataSet = data.getDataSet("SELECT * FROM Encntr WHERE DatasetSeqNum=" + datasetID);
          
					if (oDataSet.Tables[0].Rows.Count>0)
					{
						setupProgBar(oDataSet.Tables[0].Rows.Count);

						tableLbl.Text = "Delete from Encntr";
						sqlParams = data.getParameters("DeleteEncntr");

						foreach(DataRow dRow in oDataSet.Tables[0].Rows)
						{
							sqlParams[0].Value = dRow["EncntrNum"];
							sqlParams[1].Value = dRow["DataSetSeqNum"];
							data.executeDelete("DeleteEncntr", sqlParams);

							count += 1;
							barcount += 1;

							if (barcount > 300)
							{
								barcount = 0;
								updateProgBar(count);
							}
						}

						updateProgBar(count);
					}
					break;

				case "Detl" :

					oDataSet = data.getDataSet("SELECT * FROM Detl WHERE DatasetSeqNum=" + datasetID);

					if (oDataSet.Tables[0].Rows.Count>0)
					{
						setupProgBar(oDataSet.Tables[0].Rows.Count);

						tableLbl.Text = "Delete from Detl";
						sqlParams = data.getParameters("DeleteDetl");

						foreach(DataRow dRow in oDataSet.Tables[0].Rows)
						{
							sqlParams[0].Value = dRow["EncntrNum"];
							sqlParams[1].Value = dRow["DataSetSeqNum"];
							sqlParams[2].Value = dRow["DetlSeqNum"];

							data.executeDelete("DeleteDetl", sqlParams);

							count += 1;
							barcount += 1;

							if (barcount > 300)
							{
								barcount = 0;
								updateProgBar(count);
							}
						}
						
						updateProgBar(count);
					}
					break;

				case "EncntrCPT" :
          
					oDataSet = data.getDataSet("SELECT * FROM EncntrCPT WHERE DatasetSeqNum=" + datasetID);

					if (oDataSet.Tables[0].Rows.Count>0)
					{
						setupProgBar(oDataSet.Tables[0].Rows.Count);

						tableLbl.Text = "Delete from EncCpt";
						sqlParams = data.getParameters("DeleteEncntrCPT");

						foreach(DataRow drow in oDataSet.Tables[0].Rows)
						{
							sqlParams[0].Value = drow["EncntrNum"];
							sqlParams[1].Value = drow["Datasetseqnum"];
							sqlParams[2].Value = drow["EncntrCPTSeqNum"];

							data.executeDelete("DeleteEncntrCPT", sqlParams);

							count += 1;
							barcount += 1;

							if (barcount > 300)
							{
								barcount = 0;
								updateProgBar(count);
							}
						}

						updateProgBar(count);
					}
					break;

					default : break;
			}

			
		}


		private void importDataBtn_Click(object sender, System.EventArgs e)
		{
			Thread t = new Thread(new ThreadStart(this.writeToDB)); 
			t.IsBackground = true;
			t.Start(); 
		}

		#region "Write Dataset"

		private void writeToDB()
		{
			string DataSetID = datasetBx.SelectedValue.ToString();

			DateTime start_time, stop_time;
			TimeSpan elapsed_time;

			count = 0;
			barCount = 0;

			start_time = DateTime.Now;

			if (encRadioBtn.Checked)
			{
				tableLbl.Text = "Encounter";
				updateEncounterTable(DataSetID);
			}
			else if (detailRadioBtn.Checked)
			{
				tableLbl.Text = "Detail";
				updateDetailTable(DataSetID);
			}
			else if (encCptRadioBtn.Checked)
			{
				tableLbl.Text = "EncCpt";
				updateEncCptTable(DataSetID);
			}

			stop_time = DateTime.Now;
			elapsed_time = stop_time.Subtract(start_time);

			exceptionLbl.Text = Math.Round(elapsed_time.TotalSeconds, 2) + " seconds";

			progLbl.Text = "Done";
			progLbl.Refresh();
		}



		#region "Write to Encounter"

		// ***** Field Values (used to speed importing process)
		static int fld_Encntr_EncntrNum = 0;
		static int fld_Encntr_DatasetSeqNum = 1;
		static int fld_Encntr_ChrgAmt = 2;
		static int fld_Encntr_CompanyCode = 3;
		static int fld_Encntr_CostAmt = 4;
		static int fld_Encntr_DRGRateCode = 5;
		static int fld_Encntr_InOutInd = 6;
		static int fld_Encntr_InsurncPlanCode = 7;
		static int fld_Encntr_LOSNum = 8;
		static int fld_Encntr_NetRevenueAmt = 9;
		static int fld_Encntr_PmtAmt = 10;
		static int fld_Encntr_PrincplDiagCode = 11;
		static int fld_Encntr_PrincplProcdrCode = 12;


		private void updateEncounterTable(string datasetID)
		{
			string accessTableName = "";
			accessTableName = ((DataRowView)accessTable.Items[accessTable.SelectedIndex])[2].ToString();

			// ***** Pull the Start and End Period for data to import

			DataSet oDataSet = accessData.getDataSet("SELECT Min(Period) as startPeriod, Min(Year) as startYear, Max(Period) as endPeriod, Max(Year) as endYear FROM [" + accessTableName + "]");

			DataRow dataSetRow = oDataSet.Tables[0].Rows[0];

			// ***** Pull Dataset information and update entry

			oDataSet = data.getDataSet("SELECT * FROM Dataset WHERE DatasetSeqNum=" + datasetID);

			SqlParameter[] sqlParams = data.getParameters("UpdateDataset");

			foreach(SqlParameter sqlParam in sqlParams)
			{
				switch(sqlParam.ParameterName)
				{
					case "@DatasetSeqNum" : sqlParam.Value = datasetID; break;
					case "@DatasetName" : sqlParam.Value = oDataSet.Tables[0].Rows[0]["DatasetName"]; break;
					case "@PulledDate" : sqlParam.Value = DateTime.Today; break;
					case "@StartDate" : sqlParam.Value = dataSetRow["startPeriod"] + "/1/" + dataSetRow["startYear"]; break;
					case "@EndDate" : sqlParam.Value = dataSetRow["endPeriod"] + "/1/" + dataSetRow["endYear"]; break;
					default : break;
				}
			}
			data.executeUpdate("UpdateDataset", sqlParams);


			// ***** Update Encounter

			oDataSet = accessData.getDataSet("SELECT * FROM [" + accessTableName + "]");
			setupProgBar(oDataSet.Tables[0].Rows.Count);

			sqlParams = data.getParameters("UpdateEncntr");

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				sqlParams[fld_Encntr_EncntrNum].Value = dRow["EncNum"];
				sqlParams[fld_Encntr_DatasetSeqNum].Value = datasetID;
				sqlParams[fld_Encntr_ChrgAmt].Value = dRow["Charges"];
				sqlParams[fld_Encntr_CompanyCode].Value = dRow["Company"];
				sqlParams[fld_Encntr_CostAmt].Value = dRow["Cost"];
				sqlParams[fld_Encntr_DRGRateCode].Value = dRow["DRG"];
				sqlParams[fld_Encntr_InOutInd].Value = dRow["InOut"];
				sqlParams[fld_Encntr_InsurncPlanCode].Value = dRow["InsPlan"];
				sqlParams[fld_Encntr_LOSNum].Value = dRow["LOS"];
				sqlParams[fld_Encntr_NetRevenueAmt].Value = dRow["NetRev"];
				sqlParams[fld_Encntr_PmtAmt].Value = dRow["Payment"];
				sqlParams[fld_Encntr_PrincplDiagCode].Value = dRow["PrincipalDiag"];
				sqlParams[fld_Encntr_PrincplProcdrCode].Value = dRow["PrincipalProc"];

				data.executeUpdate("UpdateEncntr", sqlParams);

				count = count + 1;
				barCount = barCount + 1;

				if (barCount >= 300)
				{
					barCount = 0;
					updateProgBar(count);
				}
			}

			updateProgBar(count);
		}

		#endregion


		#region "Write to Detail"

		// ***** Field Values (used to speed importing process)

		static int fld_Detl_EncntrNum = 0;
		static int fld_Detl_DatasetSeqNum = 1;
		static int fld_Detl_DetlSeqNum = 2;
		static int fld_Detl_Charges = 3;
		static int fld_Detl_CPT = 4;
		static int fld_Detl_Quantity = 5;
		static int fld_Detl_RevCode = 6;

		private void updateDetailTable(string datasetID)
		{
			string accessTableName = "";
			accessTableName = ((DataRowView)accessTable.Items[accessTable.SelectedIndex])[2].ToString();
			DataSet oDataSet = accessData.getDataSet("SELECT * FROM [" + accessTableName + "]");

			setupProgBar(oDataSet.Tables[0].Rows.Count);

			SqlParameter[] sqlParams = data.getParameters("UpdateDetl");

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				sqlParams[fld_Detl_EncntrNum].Value = dRow["EncNum"];
				sqlParams[fld_Detl_DatasetSeqNum].Value = datasetID;
				sqlParams[fld_Detl_DetlSeqNum].Value = DBNull.Value;
				sqlParams[fld_Detl_Charges].Value = dRow["Charges"];
				sqlParams[fld_Detl_CPT].Value = dRow["CPT"];
				sqlParams[fld_Detl_Quantity].Value = dRow["Quantity"];
				sqlParams[fld_Detl_RevCode].Value = dRow["RevCode"];

				data.executeUpdate("UpdateDetl", sqlParams);

				count += 1;
				barCount += 1;

				if (barCount >= 300)
				{
					barCount = 0;
					updateProgBar(count);
				}
			}
			updateProgBar(count);
		}

		#endregion


		#region "Write to Enc CPT"

		// ***** Field Values (used to speed importing process)
		static int fld_EncntrCpt_EncntrNum = 0;
		static int fld_EncntrCpt_DatasetSeqNum = 1;
		static int fld_EncntrCpt_EncntrCptSeqNum = 2;
		static int fld_EncntrCpt_CPT = 3;
		static int fld_EncntrCpt_CPT_Sequence = 4;

		private void updateEncCptTable(string datasetID)
		{
			string accessTableName = "";
			accessTableName = ((DataRowView)accessTable.Items[accessTable.SelectedIndex])[2].ToString();
			DataSet oDataSet = accessData.getDataSet("SELECT * FROM [" + accessTableName + "]");

			setupProgBar(oDataSet.Tables[0].Rows.Count);

			SqlParameter[] sqlParams = data.getParameters("UpdateEncntrCpt");

			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				sqlParams[fld_EncntrCpt_EncntrNum].Value = dRow["EncNum"];
				sqlParams[fld_EncntrCpt_DatasetSeqNum].Value = datasetID;
				sqlParams[fld_EncntrCpt_EncntrCptSeqNum].Value = DBNull.Value;
				sqlParams[fld_EncntrCpt_CPT].Value = dRow["CPT"];
				sqlParams[fld_EncntrCpt_CPT_Sequence].Value = dRow["CPT_Sequence"];

				data.executeUpdate("UpdateEncntrCpt", sqlParams);

				count += 1;
				barCount += 1;

				if (barCount >= 300)
				{
					barCount = 0;
					updateProgBar(count);
				}
			}

			updateProgBar(count);
		}

		#endregion

		#endregion



		}
	


	}

