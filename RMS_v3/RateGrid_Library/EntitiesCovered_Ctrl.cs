using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using RMS_BusinessObjects;

namespace RateGrid_Library
{
	/// <summary>
	/// Summary description for EntitiesCovered_Ctrl.
	/// </summary>
	public class EntitiesCovered_Ctrl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label entitiesCoveredTitleLbl;
		private System.Windows.Forms.Label entitiesCoveredLbl;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Entity_Collection entitiesCovered;

		public EntitiesCovered_Ctrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}

		public void setEntitiesCovered(Entity_Collection in_EntitiesCovered)
		{
			entitiesCovered = in_EntitiesCovered;

			this.loadCtrl();
		}

		private void loadCtrl()
		{
			string entitiesCoveredStr = "";
			EntityBO entity;

			for(int k=0; k < entitiesCovered.Count; k++)
			{
				entity = entitiesCovered.getEntityAt(k);
				entitiesCoveredStr += entity.Name + ", ";
			}

			if (entitiesCoveredStr.Length>0)
			{	entitiesCoveredStr = entitiesCoveredStr.Substring(0, entitiesCoveredStr.Length - 2);	}

			entitiesCoveredLbl.Text = entitiesCoveredStr;
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
			this.entitiesCoveredTitleLbl = new System.Windows.Forms.Label();
			this.entitiesCoveredLbl = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// entitiesCoveredTitleLbl
			// 
			this.entitiesCoveredTitleLbl.BackColor = System.Drawing.Color.Transparent;
			this.entitiesCoveredTitleLbl.Dock = System.Windows.Forms.DockStyle.Top;
			this.entitiesCoveredTitleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.entitiesCoveredTitleLbl.Location = new System.Drawing.Point(0, 0);
			this.entitiesCoveredTitleLbl.Name = "entitiesCoveredTitleLbl";
			this.entitiesCoveredTitleLbl.Size = new System.Drawing.Size(480, 16);
			this.entitiesCoveredTitleLbl.TabIndex = 0;
			this.entitiesCoveredTitleLbl.Text = "Entities Covered";
			// 
			// entitiesCoveredLbl
			// 
			this.entitiesCoveredLbl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.entitiesCoveredLbl.ForeColor = System.Drawing.Color.DimGray;
			this.entitiesCoveredLbl.Location = new System.Drawing.Point(0, 16);
			this.entitiesCoveredLbl.Name = "entitiesCoveredLbl";
			this.entitiesCoveredLbl.Size = new System.Drawing.Size(480, 32);
			this.entitiesCoveredLbl.TabIndex = 1;
			// 
			// EntitiesCovered_Ctrl
			// 
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Controls.Add(this.entitiesCoveredLbl);
			this.Controls.Add(this.entitiesCoveredTitleLbl);
			this.Name = "EntitiesCovered_Ctrl";
			this.Size = new System.Drawing.Size(480, 48);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
