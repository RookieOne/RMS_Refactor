using System;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for InsurancePlan_Collection.
	/// </summary>
	public class InsurancePlan_Collection
	{
		#region "Variables"

		ArrayList insPlanList;

		#endregion

		#region "Constructors"

		public InsurancePlan_Collection()		{ insPlanList = new ArrayList();		}

		#endregion

		#region "Properties"

		public int Count
		{	get{ return insPlanList.Count;	}	}

		#endregion

		#region "Methods"

		public void addInsurancePlan(InsurancePlanBO insPlanToAdd)
		{	insPlanList.Add(insPlanToAdd);	}

		public InsurancePlanBO getInsurancePlanAt(int index)
		{	return (InsurancePlanBO) insPlanList[index];	}


		public string getInsurancePlanFilter()
		{
			if (insPlanList.Count == 0)
			{
				return "";
			}
			else
			{
				string filter = "(";
				for(int k=0; k<insPlanList.Count; k++)
				{
					filter += "'" + ( (InsurancePlanBO) insPlanList[k]).InsurancePlanCode + "',";
				}

				filter = filter.Substring(0, filter.Length-1);
				filter += ")";

				return filter;
			}
		}

		#endregion
	}
}
