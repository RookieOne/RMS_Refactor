using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

using RMS_DALObjects;

namespace RMSCtrl_Library
{
	/// <summary>
	/// Summary description for RateSchedule_Selection_Ctrl.
	/// </summary>
	public class RateSchedule_Selection_Ctrl : System.Windows.Forms.UserControl
	{
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.ComboBox statusTypeComboBx;
		internal System.Windows.Forms.ComboBox contractComboBx;
		internal System.Windows.Forms.ListBox rateScheduleListBx;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		#region "Variables"

		baseDALObject data;

		#endregion

		#region "Constructors"

		public RateSchedule_Selection_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			data = new baseDALObject();

      DataSet oDataSet = data.getDataSet("SELECT * FROM StatusType_View");

      statusTypeComboBx.DisplayMember = "StatusTypeDescr";
      statusTypeComboBx.ValueMember = "StatusTypeCode";
      statusTypeComboBx.DataSource = oDataSet.Tables[0];

			statusTypeComboBx.SelectedIndex=0;
		}

		#endregion

		#region "UI Setup"

		private void setupContracts()
		{
			SqlDataReader sqlDataRdr = data.getDataReader("SELECT ContrctID.ContrctIDNum as ContrctIDNum, ContrctIDDescr FROM ContrctID, Contrct_RateSched, RateSched WHERE ContrctID.ContrctIDNum=Contrct_RateSched.ContrctIDNum AND Contrct_RateSched.RateSchedSeqNum=RateSched.RateSchedSeqNum AND StatusTypeCode='" + statusTypeComboBx.SelectedValue + "' GROUP BY ContrctID.ContrctIDNum, ContrctIDDescr ORDER BY ContrctID.ContrctIDDescr ASC");

			contractComboBx.Items.Clear();
			while(sqlDataRdr.Read())
			{
				contractComboBx.Items.Add(sqlDataRdr["ContrctIDDescr"].ToString().Trim() + " (#" + sqlDataRdr["ContrctIDNum"].ToString().Trim() + ")");
			}
			contractComboBx.SelectedIndex=0;

			data.closeConnection();	
		}

		private void setupRateSchedules()
		{
      string selectedContract = contractComboBx.Items[contractComboBx.SelectedIndex].ToString();
			int begin = selectedContract.LastIndexOf("#") + 1;
			int length = selectedContract.Length - selectedContract.LastIndexOf("#") - 2;
			string contractNum = selectedContract.Substring(begin, length).Trim();

			SqlDataReader sqlDataRdr = data.getDataReader("SELECT RateSched.RateSchedSeqNum as RateSchedSeqNum, RateSchedName FROM RateSched, Contrct_RateSched WHERE RateSched.RateSchedSeqNum=Contrct_RateSched.RateSchedSeqNum AND StatusTypeCode='" + statusTypeComboBx.SelectedValue + "' AND ContrctIDNum=" + contractNum + " ORDER BY RateSchedName ASC");

			rateScheduleListBx.Items.Clear();
			while(sqlDataRdr.Read())
			{
				rateScheduleListBx.Items.Add(sqlDataRdr["RateSchedName"].ToString().Trim() + " (#" + sqlDataRdr["RateSchedSeqNum"].ToString().Trim() + ")");
			}
			rateScheduleListBx.SelectedIndex = 0;

			data.closeConnection();
		}


		#endregion

		#region "Methods"

		public ArrayList getRateSchedules()
		{
			ArrayList rateSchedules = new ArrayList();

			foreach(int index in rateScheduleListBx.SelectedIndices)
			{
				rateSchedules.Add(rateScheduleListBx.Items[index]);
			}

			return rateSchedules;
		}

		public string getRateScheduleName(string rateScheduleSelection)
		{
			int begin = rateScheduleSelection.LastIndexOf("#") + 1;
			int length = rateScheduleSelection.Length - rateScheduleSelection.LastIndexOf("#") - 2;

			return rateScheduleSelection.Substring(0, rateScheduleSelection.Length - (rateScheduleSelection.Length - rateScheduleSelection.LastIndexOf("#") + 1)).Trim();
		}

		public string getRateScheduleID(string rateScheduleSelection)
		{
			int begin = rateScheduleSelection.LastIndexOf("#") + 1;
			int length = rateScheduleSelection.Length - rateScheduleSelection.LastIndexOf("#") - 2;

			return rateScheduleSelection.Substring(begin, length).Trim();
		}

		public string getContractName()
		{
			string selectedContract = contractComboBx.Items[contractComboBx.SelectedIndex].ToString();
			int begin = selectedContract.LastIndexOf("#") + 1;
			int length = selectedContract.Length - selectedContract.LastIndexOf("#") - 2;


			return selectedContract.Substring(0, selectedContract.Length - (selectedContract.Length - selectedContract.LastIndexOf("#") + 1)).Trim();
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
			this.Label4 = new System.Windows.Forms.Label();
			this.Label3 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.statusTypeComboBx = new System.Windows.Forms.ComboBox();
			this.contractComboBx = new System.Windows.Forms.ComboBox();
			this.rateScheduleListBx = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// Label4
			// 
			this.Label4.BackColor = System.Drawing.Color.Transparent;
			this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label4.ForeColor = System.Drawing.Color.ForestGreen;
			this.Label4.Location = new System.Drawing.Point(4, 68);
			this.Label4.Name = "Label4";
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.Size = new System.Drawing.Size(104, 16);
			this.Label4.TabIndex = 52;
			this.Label4.Text = "Rate Schedules";
			// 
			// Label3
			// 
			this.Label3.BackColor = System.Drawing.Color.Transparent;
			this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label3.ForeColor = System.Drawing.Color.RoyalBlue;
			this.Label3.Location = new System.Drawing.Point(12, 36);
			this.Label3.Name = "Label3";
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label3.Size = new System.Drawing.Size(64, 20);
			this.Label3.TabIndex = 51;
			this.Label3.Text = "Contracts";
			// 
			// Label5
			// 
			this.Label5.BackColor = System.Drawing.Color.Transparent;
			this.Label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label5.ForeColor = System.Drawing.Color.Black;
			this.Label5.Location = new System.Drawing.Point(4, 8);
			this.Label5.Name = "Label5";
			this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label5.Size = new System.Drawing.Size(72, 14);
			this.Label5.TabIndex = 50;
			this.Label5.Text = "Status Type";
			// 
			// statusTypeComboBx
			// 
			this.statusTypeComboBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.statusTypeComboBx.Location = new System.Drawing.Point(80, 8);
			this.statusTypeComboBx.Name = "statusTypeComboBx";
			this.statusTypeComboBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusTypeComboBx.Size = new System.Drawing.Size(134, 21);
			this.statusTypeComboBx.TabIndex = 49;
			this.statusTypeComboBx.SelectedIndexChanged += new System.EventHandler(this.statusTypeComboBx_SelectedIndexChanged);
			// 
			// contractComboBx
			// 
			this.contractComboBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.contractComboBx.Location = new System.Drawing.Point(80, 36);
			this.contractComboBx.Name = "contractComboBx";
			this.contractComboBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.contractComboBx.Size = new System.Drawing.Size(256, 21);
			this.contractComboBx.TabIndex = 47;
			this.contractComboBx.SelectedIndexChanged += new System.EventHandler(this.contractComboBx_SelectedIndexChanged);
			// 
			// rateScheduleListBx
			// 
			this.rateScheduleListBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rateScheduleListBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateScheduleListBx.Location = new System.Drawing.Point(8, 88);
			this.rateScheduleListBx.Name = "rateScheduleListBx";
			this.rateScheduleListBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.rateScheduleListBx.Size = new System.Drawing.Size(324, 80);
			this.rateScheduleListBx.TabIndex = 48;
			// 
			// RateSchedule_Selection_Ctrl
			// 
			this.Controls.Add(this.Label4);
			this.Controls.Add(this.Label3);
			this.Controls.Add(this.Label5);
			this.Controls.Add(this.statusTypeComboBx);
			this.Controls.Add(this.contractComboBx);
			this.Controls.Add(this.rateScheduleListBx);
			this.Name = "RateSchedule_Selection_Ctrl";
			this.Size = new System.Drawing.Size(344, 176);
			this.ResumeLayout(false);

		}
		#endregion

		private void statusTypeComboBx_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			setupContracts();
		}

		private void contractComboBx_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			setupRateSchedules();
		}
	}
}
