using System;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for InsurancePlanBO.
	/// </summary>
	public class InsurancePlanBO
	{
		#region "Variables"

		string var_InsurancePlanCode;

		string var_Description;
		string var_NetRevenueContinuum;
		string var_Tier;
		string var_FinancialClass;

		#endregion

		#region "Constructors"

		public InsurancePlanBO(){}

		public InsurancePlanBO(string in_InsurancePlanCode)
		{
			this.InsurancePlanCode = in_InsurancePlanCode;
		}

		#endregion

		#region "Properties"

		public string InsurancePlanCode
		{
			get{return var_InsurancePlanCode;}
			set{var_InsurancePlanCode = value;}
		}

		public string Description
		{
			get{return var_Description;}
			set{var_Description = value;}
		}

		public string NetRevenueContinuum
		{
			get{return var_NetRevenueContinuum;}
			set{var_NetRevenueContinuum = value;}
		}

		public string Tier
		{
			get{return var_Tier;}
			set{var_Tier = value;}
		}

		public string FinancialClass
		{
			get{return var_FinancialClass;}
			set{var_FinancialClass = value;}
		}


		#endregion
	}
}
