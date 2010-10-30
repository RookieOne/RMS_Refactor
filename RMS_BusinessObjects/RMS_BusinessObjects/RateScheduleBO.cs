using System;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Rate Schedule Business Object
	/// </summary>
	/// 

	[Serializable]
	public class RateScheduleBO : baseBusinessObject
	{
		#region "Variables"

		private int var_ContractID;
		private string var_Name;
		private string var_StatusTypeCode;

		private CoverageBO var_Coverage;

		private Rate_Collection var_Rates;



		#endregion

		#region "Constructors"

		public RateScheduleBO():this(0, ""){}
  
		public RateScheduleBO(int id, string input_Name):base(id)
	{
		this.var_Name = input_Name;
			this.Coverage = new CoverageBO();
	}

	#endregion
	
		#region "Properties"

		public string RateScheduleName
		{
			get{return var_Name;}
			set{var_Name = value;}
		}

		public CoverageBO Coverage
		{
			get{return var_Coverage;}
			set{var_Coverage = value;}
		}

		public string Status
		{
			get{return var_StatusTypeCode;}
			set{var_StatusTypeCode = value;}
		}

		public int ContractID
		{
			get{return var_ContractID;}
			set{var_ContractID = value;}
		}

		public Rate_Collection Rates
		{
			get{return var_Rates;}
			set{var_Rates = value;}
		}


		#endregion
	}
}
