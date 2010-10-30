using System;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for CoverageBO.
	/// </summary>
	public class CoverageBO
	{
		#region "Variables"

		int var_RateScheduleID;

		DateTime var_StartDate;
		DateTime var_EndDate;

		Entity_Collection var_Entities;
		InsurancePlan_Collection var_InsurancePlans;

		#endregion

		#region "Constructors"

		public CoverageBO() : this(0)
		{}

		public CoverageBO(int in_RateScheduleID)
		{
			this.RateScheduleID = in_RateScheduleID;
			var_Entities = new Entity_Collection();
		}

		#endregion

		#region "Properties"

		public int RateScheduleID
		{
			get{return var_RateScheduleID;}
			set{var_RateScheduleID = value;}
		}

			public DateTime StartDate
		{
			get{return var_StartDate;}
			set{var_StartDate = value;}
		}

		public DateTime EndDate
		{
			get{return var_EndDate;}
			set{var_EndDate = value;}
		}

		public Entity_Collection Entities
		{
			get{return var_Entities;}
			set{var_Entities = value;}
		}

		public InsurancePlan_Collection InsurancePlans
		{
			get{return var_InsurancePlans;}
			set{var_InsurancePlans = value;}
		}

		#endregion

		
	}
}
