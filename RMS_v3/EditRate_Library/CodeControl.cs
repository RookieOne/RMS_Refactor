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
	/// Summary description for codeControl.
	/// </summary>
	public class CodeControl : System.Windows.Forms.UserControl
	{
		internal System.Windows.Forms.GroupBox codesGroupBx;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.TextBox revCodeBx;
		internal System.Windows.Forms.TextBox drgBx;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.TextBox icd9Bx;
		internal System.Windows.Forms.Label Label3;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		internal System.Windows.Forms.TextBox icd9dBx;
		internal System.Windows.Forms.Label label2;
		internal System.Windows.Forms.TextBox cptBx;

		private CodesManager cManager;

		public CodeControl(ref CodesManager in_cManager)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.cManager = in_cManager;
		}

		#region "Methods"

		public void setCodes(CodesBO codes)
		{
			CodeTypeBO codeType;
 
			codeType = (CodeTypeBO) codes.getCodeType("DRG");
			if (! (codeType == null) )
			{	drgBx.Text = codeType.getCodesAsString();	}

			codeType = (CodeTypeBO) codes.getCodeType("RevCode");
			if (! (codeType == null) )
			{	revCodeBx.Text = codeType.getCodesAsString();	}

			codeType = (CodeTypeBO) codes.getCodeType("CPT");
			if (! (codeType == null) )
			{	cptBx.Text = codeType.getCodesAsString();	}

			codeType = (CodeTypeBO) codes.getCodeType("ICD9");
			if (! (codeType == null) )
			{	icd9Bx.Text = codeType.getCodesAsString();	}

			codeType = (CodeTypeBO) codes.getCodeType("ICD9D");
			if (! (codeType == null) )
			{	icd9dBx.Text = codeType.getCodesAsString();	}
			
		}


		public CodesBO getCodes()
		{
			CodesBO codes = new CodesBO(ref cManager);

			codes.addCodes("DRG", cManager.getCodesList("DRG", drgBx.Text));
			codes.addCodes("RevCode", cManager.getCodesList("RevCode", revCodeBx.Text));
			codes.addCodes("CPT", cManager.getCodesList("CPT", cptBx.Text));
			codes.addCodes("ICD9", cManager.getCodesList("ICD9", icd9Bx.Text));
			codes.addCodes("ICD9D", cManager.getCodesList("ICD9D", icd9dBx.Text));

			return codes;
		}


		public void Clear()
		{
			drgBx.Text = "";
			revCodeBx.Text = "";
			cptBx.Text = "";
			icd9Bx.Text = "";
			icd9dBx.Text = "";
		}

		#endregion

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.codesGroupBx = new System.Windows.Forms.GroupBox();
			this.Label1 = new System.Windows.Forms.Label();
			this.icd9dBx = new System.Windows.Forms.TextBox();
			this.revCodeBx = new System.Windows.Forms.TextBox();
			this.drgBx = new System.Windows.Forms.TextBox();
			this.Label9 = new System.Windows.Forms.Label();
			this.Label5 = new System.Windows.Forms.Label();
			this.icd9Bx = new System.Windows.Forms.TextBox();
			this.Label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cptBx = new System.Windows.Forms.TextBox();
			this.codesGroupBx.SuspendLayout();
			this.SuspendLayout();
			// 
			// codesGroupBx
			// 
			this.codesGroupBx.BackColor = System.Drawing.Color.WhiteSmoke;
			this.codesGroupBx.Controls.Add(this.Label1);
			this.codesGroupBx.Controls.Add(this.icd9dBx);
			this.codesGroupBx.Controls.Add(this.revCodeBx);
			this.codesGroupBx.Controls.Add(this.drgBx);
			this.codesGroupBx.Controls.Add(this.Label9);
			this.codesGroupBx.Controls.Add(this.Label5);
			this.codesGroupBx.Controls.Add(this.icd9Bx);
			this.codesGroupBx.Controls.Add(this.Label3);
			this.codesGroupBx.Controls.Add(this.label2);
			this.codesGroupBx.Controls.Add(this.cptBx);
			this.codesGroupBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.codesGroupBx.Location = new System.Drawing.Point(4, 4);
			this.codesGroupBx.Name = "codesGroupBx";
			this.codesGroupBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.codesGroupBx.Size = new System.Drawing.Size(448, 164);
			this.codesGroupBx.TabIndex = 90;
			this.codesGroupBx.TabStop = false;
			this.codesGroupBx.Text = "Codes";
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.Color.CornflowerBlue;
			this.Label1.ForeColor = System.Drawing.Color.White;
			this.Label1.Location = new System.Drawing.Point(228, 112);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(216, 14);
			this.Label1.TabIndex = 100;
			this.Label1.Text = "ICD9 Diagnosis";
			// 
			// icd9dBx
			// 
			this.icd9dBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.icd9dBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.icd9dBx.Location = new System.Drawing.Point(228, 128);
			this.icd9dBx.Multiline = true;
			this.icd9dBx.Name = "icd9dBx";
			this.icd9dBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.icd9dBx.Size = new System.Drawing.Size(216, 24);
			this.icd9dBx.TabIndex = 99;
			this.icd9dBx.Text = "";
			// 
			// revCodeBx
			// 
			this.revCodeBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.revCodeBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.revCodeBx.Location = new System.Drawing.Point(8, 108);
			this.revCodeBx.Multiline = true;
			this.revCodeBx.Name = "revCodeBx";
			this.revCodeBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.revCodeBx.Size = new System.Drawing.Size(216, 44);
			this.revCodeBx.TabIndex = 7;
			this.revCodeBx.Text = "";
			// 
			// drgBx
			// 
			this.drgBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.drgBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.drgBx.Location = new System.Drawing.Point(8, 32);
			this.drgBx.Multiline = true;
			this.drgBx.Name = "drgBx";
			this.drgBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.drgBx.Size = new System.Drawing.Size(217, 56);
			this.drgBx.TabIndex = 6;
			this.drgBx.Text = "";
			// 
			// Label9
			// 
			this.Label9.BackColor = System.Drawing.Color.CornflowerBlue;
			this.Label9.ForeColor = System.Drawing.Color.White;
			this.Label9.Location = new System.Drawing.Point(228, 68);
			this.Label9.Name = "Label9";
			this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label9.Size = new System.Drawing.Size(216, 14);
			this.Label9.TabIndex = 88;
			this.Label9.Text = "ICD9 Procedure";
			// 
			// Label5
			// 
			this.Label5.BackColor = System.Drawing.Color.CornflowerBlue;
			this.Label5.ForeColor = System.Drawing.Color.White;
			this.Label5.Location = new System.Drawing.Point(8, 92);
			this.Label5.Name = "Label5";
			this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label5.Size = new System.Drawing.Size(216, 14);
			this.Label5.TabIndex = 84;
			this.Label5.Text = "Rev Code";
			// 
			// icd9Bx
			// 
			this.icd9Bx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.icd9Bx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.icd9Bx.Location = new System.Drawing.Point(228, 84);
			this.icd9Bx.Multiline = true;
			this.icd9Bx.Name = "icd9Bx";
			this.icd9Bx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.icd9Bx.Size = new System.Drawing.Size(216, 24);
			this.icd9Bx.TabIndex = 8;
			this.icd9Bx.Text = "";
			// 
			// Label3
			// 
			this.Label3.BackColor = System.Drawing.Color.CornflowerBlue;
			this.Label3.ForeColor = System.Drawing.Color.White;
			this.Label3.Location = new System.Drawing.Point(8, 16);
			this.Label3.Name = "Label3";
			this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label3.Size = new System.Drawing.Size(217, 14);
			this.Label3.TabIndex = 83;
			this.Label3.Text = "DRG";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.CornflowerBlue;
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(228, 16);
			this.label2.Name = "label2";
			this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label2.Size = new System.Drawing.Size(216, 16);
			this.label2.TabIndex = 86;
			this.label2.Text = "CPT";
			// 
			// cptBx
			// 
			this.cptBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cptBx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cptBx.Location = new System.Drawing.Point(228, 36);
			this.cptBx.Multiline = true;
			this.cptBx.Name = "cptBx";
			this.cptBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cptBx.Size = new System.Drawing.Size(216, 28);
			this.cptBx.TabIndex = 9;
			this.cptBx.Text = "";
			// 
			// CodeControl
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.codesGroupBx);
			this.Name = "CodeControl";
			this.Size = new System.Drawing.Size(460, 176);
			this.codesGroupBx.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
