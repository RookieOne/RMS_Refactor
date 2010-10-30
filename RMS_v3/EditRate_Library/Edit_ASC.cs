using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using RMS_BusinessObjects;

namespace EditRate_Library
{
	/// <summary>
	/// Summary description for Edit_ASC.
	/// </summary>
	public class Edit_ASC : System.Windows.Forms.UserControl
	{
		internal System.Windows.Forms.Panel groupPanel;
		internal System.Windows.Forms.Label Label12;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.TextBox threshldBx;
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.ListBox rateListBx;
		internal System.Windows.Forms.Button removeBtn;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Button upBtn;
		internal System.Windows.Forms.Button downBtn;
		internal System.Windows.Forms.TextBox rateBx;
		internal System.Windows.Forms.Button addBtn;
		internal System.Windows.Forms.Button setRatesBtn;
		internal System.Windows.Forms.CheckBox standardBx;
		internal System.Windows.Forms.ComboBox codeTableBx;
		internal System.Windows.Forms.TextBox priorityBx;
		internal System.Windows.Forms.GroupBox codesGroupBox;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private CodeControl codeCtrl;
		//private PassThruControl passThruCtrl;

		private CodesManager cManager;

		public Edit_ASC(ref CodesManager in_cManager)
		{
			cManager = in_cManager;
			
			InitializeComponent();	// This call is required by the Windows.Forms Form Designer.

			codeCtrl = new CodeControl(ref cManager);
			codeCtrl.Dock = DockStyle.Top;
			codesGroupBox.Controls.Add(codeCtrl);

/*
			passThruCtrl = new PassThruControl();
			passThruCtrl.Dock = DockStyle.Top;
			codesGroupBox.Controls.Add(passThruCtrl);
*/

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Edit_ASC));
			this.groupPanel = new System.Windows.Forms.Panel();
			this.Label12 = new System.Windows.Forms.Label();
			this.Label11 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.threshldBx = new System.Windows.Forms.TextBox();
			this.GroupBox3 = new System.Windows.Forms.GroupBox();
			this.Label5 = new System.Windows.Forms.Label();
			this.rateListBx = new System.Windows.Forms.ListBox();
			this.removeBtn = new System.Windows.Forms.Button();
			this.Label4 = new System.Windows.Forms.Label();
			this.upBtn = new System.Windows.Forms.Button();
			this.downBtn = new System.Windows.Forms.Button();
			this.rateBx = new System.Windows.Forms.TextBox();
			this.addBtn = new System.Windows.Forms.Button();
			this.setRatesBtn = new System.Windows.Forms.Button();
			this.codesGroupBox = new System.Windows.Forms.GroupBox();
			this.standardBx = new System.Windows.Forms.CheckBox();
			this.codeTableBx = new System.Windows.Forms.ComboBox();
			this.priorityBx = new System.Windows.Forms.TextBox();
			this.GroupBox3.SuspendLayout();
			this.codesGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupPanel
			// 
			this.groupPanel.AutoScroll = true;
			this.groupPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupPanel.BackgroundImage")));
			this.groupPanel.Location = new System.Drawing.Point(16, 40);
			this.groupPanel.Name = "groupPanel";
			this.groupPanel.Size = new System.Drawing.Size(384, 384);
			this.groupPanel.TabIndex = 83;
			// 
			// Label12
			// 
			this.Label12.BackColor = System.Drawing.Color.Transparent;
			this.Label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label12.ForeColor = System.Drawing.Color.Black;
			this.Label12.Location = new System.Drawing.Point(360, 8);
			this.Label12.Name = "Label12";
			this.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label12.Size = new System.Drawing.Size(48, 14);
			this.Label12.TabIndex = 86;
			this.Label12.Text = "Priority";
			// 
			// Label11
			// 
			this.Label11.BackColor = System.Drawing.Color.Transparent;
			this.Label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label11.ForeColor = System.Drawing.Color.Black;
			this.Label11.Location = new System.Drawing.Point(8, 8);
			this.Label11.Name = "Label11";
			this.Label11.Size = new System.Drawing.Size(96, 14);
			this.Label11.TabIndex = 84;
			this.Label11.Text = "Grouping Table";
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.Color.Transparent;
			this.Label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label1.ForeColor = System.Drawing.Color.Black;
			this.Label1.Location = new System.Drawing.Point(448, 8);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(80, 14);
			this.Label1.TabIndex = 91;
			this.Label1.Text = "Max Amount";
			// 
			// threshldBx
			// 
			this.threshldBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.threshldBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.threshldBx.ForeColor = System.Drawing.Color.Black;
			this.threshldBx.Location = new System.Drawing.Point(528, 8);
			this.threshldBx.Name = "threshldBx";
			this.threshldBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.threshldBx.Size = new System.Drawing.Size(68, 21);
			this.threshldBx.TabIndex = 90;
			this.threshldBx.Text = "0";
			// 
			// GroupBox3
			// 
			this.GroupBox3.BackColor = System.Drawing.Color.Transparent;
			this.GroupBox3.Controls.Add(this.Label5);
			this.GroupBox3.Controls.Add(this.rateListBx);
			this.GroupBox3.Controls.Add(this.removeBtn);
			this.GroupBox3.Controls.Add(this.Label4);
			this.GroupBox3.Controls.Add(this.upBtn);
			this.GroupBox3.Controls.Add(this.downBtn);
			this.GroupBox3.Controls.Add(this.rateBx);
			this.GroupBox3.Controls.Add(this.addBtn);
			this.GroupBox3.Location = new System.Drawing.Point(408, 48);
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.Size = new System.Drawing.Size(380, 136);
			this.GroupBox3.TabIndex = 89;
			this.GroupBox3.TabStop = false;
			this.GroupBox3.Text = "Rate Reimbursement";
			// 
			// Label5
			// 
			this.Label5.BackColor = System.Drawing.Color.Transparent;
			this.Label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label5.ForeColor = System.Drawing.Color.Black;
			this.Label5.Location = new System.Drawing.Point(4, 24);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(104, 14);
			this.Label5.TabIndex = 38;
			this.Label5.Text = "Rate Percentage";
			// 
			// rateListBx
			// 
			this.rateListBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rateListBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateListBx.ForeColor = System.Drawing.Color.Black;
			this.rateListBx.Location = new System.Drawing.Point(212, 56);
			this.rateListBx.Name = "rateListBx";
			this.rateListBx.Size = new System.Drawing.Size(68, 67);
			this.rateListBx.TabIndex = 1;
			// 
			// removeBtn
			// 
			this.removeBtn.BackColor = System.Drawing.Color.White;
			this.removeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.removeBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.removeBtn.ForeColor = System.Drawing.Color.Black;
			this.removeBtn.Location = new System.Drawing.Point(308, 56);
			this.removeBtn.Name = "removeBtn";
			this.removeBtn.Size = new System.Drawing.Size(64, 24);
			this.removeBtn.TabIndex = 3;
			this.removeBtn.Text = "remove";
			// 
			// Label4
			// 
			this.Label4.BackColor = System.Drawing.Color.Transparent;
			this.Label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Label4.ForeColor = System.Drawing.Color.Black;
			this.Label4.Location = new System.Drawing.Point(24, 56);
			this.Label4.Name = "Label4";
			this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label4.Size = new System.Drawing.Size(184, 64);
			this.Label4.TabIndex = 37;
			this.Label4.Text = "Per procedure reimbursement percentage. Ranked from first (top) to last (bottom)";
			// 
			// upBtn
			// 
			this.upBtn.BackColor = System.Drawing.Color.White;
			this.upBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.upBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.upBtn.ForeColor = System.Drawing.Color.Black;
			this.upBtn.Location = new System.Drawing.Point(284, 56);
			this.upBtn.Name = "upBtn";
			this.upBtn.Size = new System.Drawing.Size(20, 20);
			this.upBtn.TabIndex = 31;
			this.upBtn.Text = "^";
			// 
			// downBtn
			// 
			this.downBtn.BackColor = System.Drawing.Color.White;
			this.downBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.downBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.downBtn.ForeColor = System.Drawing.Color.Black;
			this.downBtn.Location = new System.Drawing.Point(284, 80);
			this.downBtn.Name = "downBtn";
			this.downBtn.Size = new System.Drawing.Size(20, 20);
			this.downBtn.TabIndex = 32;
			this.downBtn.Text = "v";
			// 
			// rateBx
			// 
			this.rateBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rateBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rateBx.ForeColor = System.Drawing.Color.Black;
			this.rateBx.Location = new System.Drawing.Point(108, 24);
			this.rateBx.Name = "rateBx";
			this.rateBx.Size = new System.Drawing.Size(68, 21);
			this.rateBx.TabIndex = 0;
			this.rateBx.Text = "";
			// 
			// addBtn
			// 
			this.addBtn.BackColor = System.Drawing.Color.White;
			this.addBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.addBtn.ForeColor = System.Drawing.Color.Black;
			this.addBtn.Location = new System.Drawing.Point(184, 24);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(64, 24);
			this.addBtn.TabIndex = 2;
			this.addBtn.Text = "add";
			// 
			// setRatesBtn
			// 
			this.setRatesBtn.BackColor = System.Drawing.Color.White;
			this.setRatesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.setRatesBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.setRatesBtn.ForeColor = System.Drawing.Color.Black;
			this.setRatesBtn.Location = new System.Drawing.Point(608, 8);
			this.setRatesBtn.Name = "setRatesBtn";
			this.setRatesBtn.Size = new System.Drawing.Size(180, 28);
			this.setRatesBtn.TabIndex = 87;
			this.setRatesBtn.Text = "Set Rates";
			// 
			// codesGroupBox
			// 
			this.codesGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.codesGroupBox.Controls.Add(this.standardBx);
			this.codesGroupBox.Location = new System.Drawing.Point(408, 192);
			this.codesGroupBox.Name = "codesGroupBox";
			this.codesGroupBox.Size = new System.Drawing.Size(384, 236);
			this.codesGroupBox.TabIndex = 88;
			this.codesGroupBox.TabStop = false;
			this.codesGroupBox.Text = "Codes";
			// 
			// standardBx
			// 
			this.standardBx.BackColor = System.Drawing.Color.Transparent;
			this.standardBx.Dock = System.Windows.Forms.DockStyle.Top;
			this.standardBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.standardBx.ForeColor = System.Drawing.Color.Black;
			this.standardBx.Location = new System.Drawing.Point(3, 16);
			this.standardBx.Name = "standardBx";
			this.standardBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.standardBx.Size = new System.Drawing.Size(378, 20);
			this.standardBx.TabIndex = 72;
			this.standardBx.Text = "Standard ASC CPTs (10000-69999)";
			// 
			// codeTableBx
			// 
			this.codeTableBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.codeTableBx.ForeColor = System.Drawing.Color.Black;
			this.codeTableBx.Location = new System.Drawing.Point(104, 8);
			this.codeTableBx.Name = "codeTableBx";
			this.codeTableBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.codeTableBx.Size = new System.Drawing.Size(256, 21);
			this.codeTableBx.TabIndex = 82;
			// 
			// priorityBx
			// 
			this.priorityBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.priorityBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.priorityBx.ForeColor = System.Drawing.Color.Black;
			this.priorityBx.Location = new System.Drawing.Point(408, 8);
			this.priorityBx.Name = "priorityBx";
			this.priorityBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.priorityBx.Size = new System.Drawing.Size(33, 21);
			this.priorityBx.TabIndex = 85;
			this.priorityBx.Text = "0";
			// 
			// Edit_ASC
			// 
			this.Controls.Add(this.groupPanel);
			this.Controls.Add(this.Label12);
			this.Controls.Add(this.Label11);
			this.Controls.Add(this.Label1);
			this.Controls.Add(this.threshldBx);
			this.Controls.Add(this.GroupBox3);
			this.Controls.Add(this.setRatesBtn);
			this.Controls.Add(this.codesGroupBox);
			this.Controls.Add(this.codeTableBx);
			this.Controls.Add(this.priorityBx);
			this.Name = "Edit_ASC";
			this.Size = new System.Drawing.Size(808, 480);
			this.GroupBox3.ResumeLayout(false);
			this.codesGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
