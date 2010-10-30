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
	/// Summary description for PassThruControl.
	/// </summary>
	public class PassThruControl : System.Windows.Forms.UserControl
	{
		#region "Variables"

		internal System.Windows.Forms.GroupBox passThrusGroupBx;
		internal System.Windows.Forms.ListBox passThrusBx;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		RateSchedulePassThrusBO RateSchedulePassThrus;

		#endregion

		#region "Constructors"

		public PassThruControl(RateSchedulePassThrusBO in_RateSchedulePassThrus)
		{
			InitializeComponent();	// This call is required by the Windows.Forms Form Designer.

			RateSchedulePassThrus = in_RateSchedulePassThrus;

			loadRateSchedulePassThrus();

			passThrusBx.SelectedIndex = 0;
		}

		#endregion

		#region "Methods"

		public void loadRateSchedulePassThrus()
		{
			passThrusBx.Items.Clear();

			passThrusBx.Items.Add(new PassThruStruct(0, "All"));
			passThrusBx.Items.Add(new PassThruStruct(0, "None"));


			ArrayList rateScheduleList = RateSchedulePassThrus.getPassThrusList();
			
			for(int k=0; k<rateScheduleList.Count; k++)
			{	passThrusBx.Items.Add( (PassThruStruct) rateScheduleList[k] );	}
		}

		public PassThrusBO getPassThrus()
		{
			PassThrusBO passThrus = new PassThrusBO(RateSchedulePassThrus);

			for(int k=0; k<passThrusBx.SelectedIndices.Count; k++)
			{	passThrus.addPassThru( (PassThruStruct) passThrusBx.Items[passThrusBx.SelectedIndices[k]] );		}

      return passThrus;
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
			this.passThrusGroupBx = new System.Windows.Forms.GroupBox();
			this.passThrusBx = new System.Windows.Forms.ListBox();
			this.passThrusGroupBx.SuspendLayout();
			this.SuspendLayout();
			// 
			// passThrusGroupBx
			// 
			this.passThrusGroupBx.BackColor = System.Drawing.Color.Transparent;
			this.passThrusGroupBx.Controls.Add(this.passThrusBx);
			this.passThrusGroupBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.passThrusGroupBx.Location = new System.Drawing.Point(8, 8);
			this.passThrusGroupBx.Name = "passThrusGroupBx";
			this.passThrusGroupBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.passThrusGroupBx.Size = new System.Drawing.Size(256, 128);
			this.passThrusGroupBx.TabIndex = 92;
			this.passThrusGroupBx.TabStop = false;
			this.passThrusGroupBx.Text = "Select PassThrus";
			// 
			// passThrusBx
			// 
			this.passThrusBx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.passThrusBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.passThrusBx.Location = new System.Drawing.Point(4, 16);
			this.passThrusBx.Name = "passThrusBx";
			this.passThrusBx.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.passThrusBx.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.passThrusBx.Size = new System.Drawing.Size(244, 106);
			this.passThrusBx.TabIndex = 10;
			// 
			// PassThruControl
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.passThrusGroupBx);
			this.Name = "PassThruControl";
			this.Size = new System.Drawing.Size(280, 144);
			this.passThrusGroupBx.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
