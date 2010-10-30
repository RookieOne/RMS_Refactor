using System;
using System.Data;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for EncounterDataBO.
	/// </summary>
	public class EncounterDataBO
	{
		#region "Variables"

		DataSet var_Data;

		#endregion

		#region "Constructors"

		public EncounterDataBO()
		{		}

		#endregion

		#region "Properties"

		public DataSet Data
		{
			get{return var_Data;	}
			set{var_Data = value;}
		}

		#endregion



	}
}
