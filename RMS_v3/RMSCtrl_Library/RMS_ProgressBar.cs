using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace RMSCtrl_Library
{
	/// <summary>
	/// Summary description for RMS_ProgressBar.
	/// </summary>
	public class RMS_ProgressBar : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label titleLbl;
		private System.Windows.Forms.ProgressBar progBar;
		private System.Windows.Forms.Label progLbl;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region "Variables"

		int var_Increment;
		int tempCount, incrementCount;

		#endregion


		#region "Constructors"

		public RMS_ProgressBar()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#endregion


		#region "Properties"

		public System.Windows.Forms.ProgressBar ProgressBar
		{
			get{return progBar;}
			set{progBar = value;}
		}

		public System.Windows.Forms.Label ProgressLabel
		{
			get{return progLbl;}
			set{progLbl = value;}
		}


		public int Max
		{
			get{return progBar.Maximum;}
			set{progBar.Maximum = value;}
		}

		public int Min
		{
			get{return progBar.Minimum;}
			set{progBar.Minimum = value;}
		}

		public int Increment
		{
			get{return var_Increment;}
			set
			{
				var_Increment = value;
				progBar.Step = value;
			}
		}

		public string Title
		{
			get{return titleLbl.Text;}
			set{titleLbl.Text = value;}
		}

		#endregion

		#region "Methods"

		public void Init()
		{
			tempCount = 0;
			incrementCount = 0;
			progLbl.Text = "0 of" + this.Max;
      
			progBar.Value=0;
			progBar.Refresh();
		}

		public void Finished()
		{
			progLbl.Text = "DONE";

			progBar.Value=0;
			progBar.Refresh();
		}

		public void setAlignment(string alignment)
		{
			switch(alignment)
			{
				case "Left" : 
					progLbl.TextAlign = System.Drawing.ContentAlignment.TopLeft;
					titleLbl.TextAlign = System.Drawing.ContentAlignment.TopLeft;
					break;

				case "Center" :
					progLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
					titleLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
					break;

				default: break;
			}
		}

		public void Step()
		{
			tempCount += 1;
			incrementCount +=1;

			if (incrementCount==this.Increment)
			{
				incrementCount=0;

				progBar.PerformStep();
				progBar.Refresh();

				progLbl.Text = tempCount + " of " + this.Max;
				progLbl.Refresh();
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
			this.titleLbl = new System.Windows.Forms.Label();
			this.progBar = new System.Windows.Forms.ProgressBar();
			this.progLbl = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// titleLbl
			// 
			this.titleLbl.BackColor = System.Drawing.Color.Transparent;
			this.titleLbl.Dock = System.Windows.Forms.DockStyle.Top;
			this.titleLbl.Location = new System.Drawing.Point(0, 0);
			this.titleLbl.Name = "titleLbl";
			this.titleLbl.Size = new System.Drawing.Size(760, 16);
			this.titleLbl.TabIndex = 3;
			// 
			// progBar
			// 
			this.progBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.progBar.Location = new System.Drawing.Point(0, 16);
			this.progBar.Name = "progBar";
			this.progBar.Size = new System.Drawing.Size(760, 8);
			this.progBar.TabIndex = 4;
			// 
			// progLbl
			// 
			this.progLbl.BackColor = System.Drawing.Color.Transparent;
			this.progLbl.Dock = System.Windows.Forms.DockStyle.Top;
			this.progLbl.Location = new System.Drawing.Point(0, 24);
			this.progLbl.Name = "progLbl";
			this.progLbl.Size = new System.Drawing.Size(760, 16);
			this.progLbl.TabIndex = 5;
			// 
			// RMS_ProgressBar
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.progLbl);
			this.Controls.Add(this.progBar);
			this.Controls.Add(this.titleLbl);
			this.Name = "RMS_ProgressBar";
			this.Size = new System.Drawing.Size(760, 40);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
