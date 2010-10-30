using System;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for EntityBO.
	/// </summary>
	public class EntityBO
	{
		#region "Variables"

		string var_CompanyCode;
		string var_EntityCode;
		string var_Name;

		#endregion

		#region "Constructors"

		public EntityBO(){}
		public EntityBO(string inCompanyCode, string inEntityCode, string inName)
		{
			this.CompanyCode = inCompanyCode;
			this.EntityCode = inEntityCode;
			this.Name = inName;
		}

		#endregion

		#region "Properties"

		public string CompanyCode
		{
			get{return var_CompanyCode;}
			set{var_CompanyCode = value;}
		}

		public string EntityCode
		{
			get{return var_EntityCode;}
			set{var_EntityCode = value;}
		}

		public string Name
		{
			get{return var_Name;}
			set{var_Name = value;}
		}

		#endregion
	}
}
