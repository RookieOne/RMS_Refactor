using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading;

using RMS_DALObjects;

namespace RMSCtrl_Library
{
	/// <summary>
	/// Summary description for ImportData_Control.
	/// </summary>
	public class ImportData_Control : System.Windows.Forms.UserControl
	{
		internal System.Windows.Forms.ComboBox accessTable;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.Label accessLbl;
		internal System.Windows.Forms.Button findAccessBtn;
		internal System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		#region "Variables"

		static int fieldControlHeight = 27;

		string var_DestinationTableName;

		AccessDALObject accessData = new AccessDALObject();
		BaseDALObject data = new BaseDALObject();

		Panel destinationPanel, sourcePanel;
		RMS_ProgressBar progBar;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.CheckBox replaceChkBx;
		private System.Windows.Forms.Button importBtn;
		private System.Windows.Forms.GroupBox fieldsGroupBx;

		ArrayList destinationFieldsList;

		#endregion

		#region "Constructors"

		public ImportData_Control()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			progBar = new RMS_ProgressBar();
			progBar.Dock = DockStyle.Bottom;
			progBar.setAlignment("Center");
			topPanel.Controls.Add(progBar);

			sourcePanel = new Panel();
			sourcePanel.AutoScroll = true;
			sourcePanel.Width = 350;
			sourcePanel.Dock = DockStyle.Left;

			Splitter splitterCtrl = new Splitter();
			splitterCtrl.Dock = DockStyle.Left;
			splitterCtrl.MinExtra = 200;
			splitterCtrl.MinSize = 200;

			destinationPanel = new Panel();
			destinationPanel.AutoScroll = true;
			destinationPanel.Dock = DockStyle.Fill;

			fieldsGroupBx.Controls.AddRange(new Control[] {destinationPanel, splitterCtrl, sourcePanel});
		}

		#endregion

		#region "Properties"

		public string DestinationTableName
		{
			get{return var_DestinationTableName;}
			set
			{ 
				var_DestinationTableName = value;
				loadDestinationFields();
			}
		}


		#endregion

		#region "Methods"

		private void loadDestinationFields()
		{
			DataSet oDataSet = data.GetDataSet("SELECT * FROM [" + this.DestinationTableName + "]");

			destinationFieldsList = new ArrayList();

			foreach(DataColumn dCol in oDataSet.Tables[0].Columns)
			{
				if ( ! (( (dCol.ColumnName=="UpdateOpertr") || (dCol.ColumnName=="UpdateDate") )) )
				{
					destinationFieldsList.Add(dCol.ColumnName);	
				}
			}

			destinationPanel.Controls.Clear();
			for(int k=0; k<destinationFieldsList.Count; k++)
			{
				Label newLabel = new Label();
				newLabel.Dock = DockStyle.Top;
				newLabel.Text = destinationFieldsList[destinationFieldsList.Count - (k+1)].ToString();

				newLabel.Height = fieldControlHeight;

				destinationPanel.Controls.Add(newLabel);
			}
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
			this.topPanel = new System.Windows.Forms.Panel();
			this.accessTable = new System.Windows.Forms.ComboBox();
			this.Label7 = new System.Windows.Forms.Label();
			this.accessLbl = new System.Windows.Forms.Label();
			this.findAccessBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.replaceChkBx = new System.Windows.Forms.CheckBox();
			this.importBtn = new System.Windows.Forms.Button();
			this.fieldsGroupBx = new System.Windows.Forms.GroupBox();
			this.topPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// topPanel
			// 
			this.topPanel.Controls.Add(this.replaceChkBx);
			this.topPanel.Controls.Add(this.label1);
			this.topPanel.Controls.Add(this.accessLbl);
			this.topPanel.Controls.Add(this.findAccessBtn);
			this.topPanel.Controls.Add(this.accessTable);
			this.topPanel.Controls.Add(this.Label7);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(680, 88);
			this.topPanel.TabIndex = 2;
			// 
			// accessTable
			// 
			this.accessTable.Location = new System.Drawing.Point(124, 28);
			this.accessTable.Name = "accessTable";
			this.accessTable.Size = new System.Drawing.Size(304, 21);
			this.accessTable.TabIndex = 36;
			this.accessTable.SelectedIndexChanged += new System.EventHandler(this.accessTable_SelectedIndexChanged);
			// 
			// Label7
			// 
			this.Label7.BackColor = System.Drawing.Color.Transparent;
			this.Label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label7.Location = new System.Drawing.Point(32, 32);
			this.Label7.Name = "Label7";
			this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label7.Size = new System.Drawing.Size(88, 16);
			this.Label7.TabIndex = 35;
			this.Label7.Text = "Access Table";
			// 
			// accessLbl
			// 
			this.accessLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.accessLbl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.accessLbl.Location = new System.Drawing.Point(124, 8);
			this.accessLbl.Name = "accessLbl";
			this.accessLbl.Size = new System.Drawing.Size(304, 16);
			this.accessLbl.TabIndex = 39;
			// 
			// findAccessBtn
			// 
			this.findAccessBtn.BackColor = System.Drawing.Color.White;
			this.findAccessBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.findAccessBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.findAccessBtn.Location = new System.Drawing.Point(432, 8);
			this.findAccessBtn.Name = "findAccessBtn";
			this.findAccessBtn.Size = new System.Drawing.Size(40, 20);
			this.findAccessBtn.TabIndex = 38;
			this.findAccessBtn.Text = "Find";
			this.findAccessBtn.Click += new System.EventHandler(this.findAccessBtn_Click);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 40;
			this.label1.Text = "Access Database";
			// 
			// replaceChkBx
			// 
			this.replaceChkBx.Location = new System.Drawing.Point(488, 8);
			this.replaceChkBx.Name = "replaceChkBx";
			this.replaceChkBx.TabIndex = 41;
			this.replaceChkBx.Text = "Replace Old";
			// 
			// importBtn
			// 
			this.importBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.importBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.importBtn.Location = new System.Drawing.Point(0, 88);
			this.importBtn.Name = "importBtn";
			this.importBtn.Size = new System.Drawing.Size(680, 23);
			this.importBtn.TabIndex = 4;
			this.importBtn.Text = "Import";
			this.importBtn.Click += new System.EventHandler(this.importBtn_Click);
			// 
			// fieldsGroupBx
			// 
			this.fieldsGroupBx.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fieldsGroupBx.Location = new System.Drawing.Point(0, 111);
			this.fieldsGroupBx.Name = "fieldsGroupBx";
			this.fieldsGroupBx.Size = new System.Drawing.Size(680, 209);
			this.fieldsGroupBx.TabIndex = 5;
			this.fieldsGroupBx.TabStop = false;
			this.fieldsGroupBx.Text = "Fields";
			// 
			// ImportData_Control
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.fieldsGroupBx);
			this.Controls.Add(this.importBtn);
			this.Controls.Add(this.topPanel);
			this.Name = "ImportData_Control";
			this.Size = new System.Drawing.Size(680, 320);
			this.topPanel.ResumeLayout(false);
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


		private void accessTable_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string accessTableName = "";
			accessTableName = ((DataRowView)accessTable.Items[accessTable.SelectedIndex])[2].ToString();
			DataSet oDataSet = accessData.getDataSet("SELECT * FROM [" + accessTableName + "]");

			ArrayList columnsList = new ArrayList();
			foreach(DataColumn dCol in oDataSet.Tables[0].Columns)
			{
        columnsList.Add(dCol.ColumnName);
			}

			sourcePanel.Controls.Clear();
			for(int k=0; k<destinationFieldsList.Count; k++)
			{
				Panel comboBxPanel = new Panel();
				comboBxPanel.Dock = DockStyle.Top;
 			  comboBxPanel.Height = fieldControlHeight;

				sourcePanel.Controls.Add(comboBxPanel);

				ComboBox newComboBx = new ComboBox();

				newComboBx.Items.Add("NULL");
				foreach(DataColumn dCol in oDataSet.Tables[0].Columns)
				{
					newComboBx.Items.Add(dCol.ColumnName);
				}
				newComboBx.SelectedIndex=0;
				comboBxPanel.Controls.Add(newComboBx);

				Label newLabel = new Label();
				newLabel.Left = newComboBx.Left + newComboBx.Width;
				newLabel.Width = comboBxPanel.Width - newLabel.Left;
				newLabel.Text = destinationFieldsList[destinationFieldsList.Count - (k+1)].ToString();
				comboBxPanel.Controls.Add(newLabel);

			}
						
		}


		private void importBtn_Click(object sender, System.EventArgs e)
		{
			Thread t = new Thread(new ThreadStart(this.import)); 
			t.IsBackground = true;
			t.Start(); 
		}

		private void import()
		{
			Hashtable mappingTable = new Hashtable();
			Panel itemPanel;
			SqlParameter[] sqlParams;
			DataSet oDataSet;

			for(int k=0; k<destinationPanel.Controls.Count; k++)
			{
				itemPanel = (Panel) sourcePanel.Controls[k];
				string destinationFieldName = ( (Label) itemPanel.Controls[1]).Text;
				string sourceFieldName = ((ComboBox) itemPanel.Controls[0]).SelectedItem.ToString();

				if (! (sourceFieldName=="NULL") )
				{	mappingTable.Add(destinationFieldName, sourceFieldName);		}
			}

			// Check Replace and Replace

			if (replaceChkBx.Checked)
			{
				sqlParams = data.GetParameters("Delete" + this.DestinationTableName);
			
				oDataSet = data.GetDataSet("SELECT * FROM [" + this.DestinationTableName + "]");

				progBar.Title = "Deleting " + this.DestinationTableName;
				progBar.Max = oDataSet.Tables[0].Rows.Count;
				progBar.Increment = 150;
				progBar.Init();

				foreach(DataRow dRow in oDataSet.Tables[0].Rows)
				{
					progBar.Step();
					for(int k=0; k<sqlParams.Length; k++)
					{
						sqlParams[k].Value = dRow[k];
					}
					data.ExecuteDelete("Delete" + this.DestinationTableName, sqlParams);
				}

				progBar.Finished();
			}



			sqlParams = data.GetParameters("Update" + this.DestinationTableName);

			string accessTableName = "";
			accessTableName = ((DataRowView)accessTable.Items[accessTable.SelectedIndex])[2].ToString();
			oDataSet = accessData.getDataSet("SELECT * FROM [" + accessTableName + "]");

			progBar.Title = "Loading " + accessTableName + " to " + this.DestinationTableName;
			progBar.Max = oDataSet.Tables[0].Rows.Count;
			progBar.Increment = 150;
			progBar.Init();

			string parameterName;
			foreach(DataRow dRow in oDataSet.Tables[0].Rows)
			{
				progBar.Step();
				foreach(SqlParameter sqlParam in sqlParams)
				{
					parameterName = sqlParam.ParameterName.ToString();
					parameterName = parameterName.Substring(1, parameterName.Length-1);
					if (mappingTable.ContainsKey(parameterName))
					{	sqlParam.Value = dRow[mappingTable[parameterName].ToString()].ToString();	}
					else
					{	sqlParam.Value = null;		}
				}

				data.ExecuteUpdate("Update" + this.DestinationTableName, sqlParams);
			}

			progBar.Finished();
		}





	}
}
