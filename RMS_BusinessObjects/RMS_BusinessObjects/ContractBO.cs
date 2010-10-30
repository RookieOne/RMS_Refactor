using System;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// A contract is the top most level of rate schedule organization.
	/// A contract holds a list of rate schedules.
	/// </summary>
	/// 

	public struct RateScheduleStruct
  {
    int var_ID;
    string var_Name;

    public RateScheduleStruct(int in_ID, string in_Name)
		{
      var_ID = in_ID;
      var_Name = in_Name;		
		}

		#region "Properties"

		public int ID
		{
			get{return var_ID;}
			set{ var_ID = value; }
		}

		public string Name
		{
			get{return var_Name;}
			set{ var_Name = value; }
		}


		#endregion

		#region "Overrides"

		public override string ToString()
		{	return this.Name + " (#" + this.ID + ")";			}

		#endregion

  }


	public class ContractBO : baseBusinessObject
	{
		#region "Variables"

		private string var_Name;
		private ArrayList RateSchedulesList;

		#endregion

		#region "Constructors"

		public ContractBO():this(0, ""){RateSchedulesList = new ArrayList();}
  
		public ContractBO(int id) : base(id) {}

		public ContractBO(int id, string in_Name):base(id)
    {
      this.var_Name = in_Name;
			RateSchedulesList = new ArrayList();
    }

		#endregion

		#region "Properties"

		public string ContractName
		{
			get{return var_Name;}
			set{var_Name = value;}
		}

		#endregion


		#region "Methods"

		public int rateScheduleCount()
		{	return this.RateSchedulesList.Count;	}

		public void addRateSchedule(int in_RateScheduleNum, string in_RateScheduleName)
		{
			RateSchedulesList.Add(new RateScheduleStruct(in_RateScheduleNum, in_RateScheduleName));
		}

		public RateScheduleStruct getRateScheduleAt(int index)
		{	return (RateScheduleStruct) RateSchedulesList[index];	}

		public string getRateScheduleStringAt(int index)
		{
			RateScheduleStruct rateSchedule = (RateScheduleStruct) RateSchedulesList[index];
			return rateSchedule.ToString();
		}


		#endregion

		#region OVERRIDES

		public override string ToString()
		{
			return this.var_Name.ToString().Trim() + " (#" + this.ID + ")";
		}

		#endregion

	}
}
