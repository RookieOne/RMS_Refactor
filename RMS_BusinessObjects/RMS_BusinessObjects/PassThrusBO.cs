using System;
using System.Text;
using System.Collections;

namespace RMS_BusinessObjects
{
	public struct PassThruStruct
	{
		public int RateID;
		public string RateName;

		public PassThruStruct(int in_RateID, string in_RateName)
		{	this.RateID = in_RateID;	this.RateName = in_RateName;	}

		public override string ToString()
		{
			return RateName;
		}

	}

	public class RateSchedulePassThrusBO
	{
		ArrayList passThrusTable;

		public RateSchedulePassThrusBO()	{	passThrusTable = new ArrayList();	}

		public void addPassThru(int rateID, string rateName)	{ passThrusTable.Add(new PassThruStruct(rateID, rateName));	}


		public bool Contains(int rateID)
		{
			bool boolToReturn = false;
			PassThruStruct passThru;

			for (int k=0; k<passThrusTable.Count; k++)
			{
				passThru = (PassThruStruct) passThrusTable[k];
				if ( passThru.RateID == rateID)
				{	boolToReturn = true;	}
			}

			return boolToReturn;	
		}

		public string getPassThruName(int rateID) 
		{
			string passThruName = "";
			PassThruStruct passThru;

			for (int k=0; k<passThrusTable.Count; k++)
			{
				passThru = (PassThruStruct) passThrusTable[k];
				if ( passThru.RateID == rateID)
				{	passThruName = passThru.RateName;	}
			}

			return passThruName;	
		}

		public ArrayList getPassThrusList(){	return passThrusTable;	}

	}


	/// <summary>
	/// Summary description for PassThrusBO.
	/// </summary>
	public class PassThrusBO
	{
		#region "Variables"

		private RateSchedulePassThrusBO var_RateSchedulePassThrus;

		private ArrayList var_PassThrus;

		#endregion

		#region "Constructors"

		public PassThrusBO()
		{
			this.RateSchedulePassThrus = new RateSchedulePassThrusBO();
			this.PassThrus = new ArrayList();
		}
		public PassThrusBO(RateSchedulePassThrusBO in_RateSchedulePassThrus)	
		{	
			this.RateSchedulePassThrus = in_RateSchedulePassThrus;
			this.PassThrus = new ArrayList();
		}
		public PassThrusBO(ArrayList in_PassThrus, RateSchedulePassThrusBO in_RateSchedulePassThrus)	
		{	
			this.RateSchedulePassThrus = in_RateSchedulePassThrus;
			this.addPassThrus(in_PassThrus);
		}

		#endregion

		#region "Properties"

		public RateSchedulePassThrusBO RateSchedulePassThrus
		{
			get{return var_RateSchedulePassThrus;}
			set{var_RateSchedulePassThrus = value;}
		}

		public ArrayList PassThrus
		{
			get{return var_PassThrus;}
			set{var_PassThrus = value;}
		}


		#endregion

		#region "Methods"

		public void Clear(){	this.PassThrus.Clear();	}

		public void addPassThru(int rateID, string PassThruName)
		{	this.PassThrus.Add(new PassThruStruct(rateID, PassThruName));	}

		public void addPassThru(PassThruStruct passThruToAdd)
		{	this.PassThrus.Add(passThruToAdd);	}

		public void addPassThrus(ArrayList PassThrusList)
		{
			for(int k=0; k<PassThrusList.Count; k++)
			{
				this.PassThrus.Add((PassThruStruct) PassThrusList[k]);
			}
		}


		public ArrayList getArrayListOfPassThrus()
		{
			return this.PassThrus;
		}


		#endregion

		public override string ToString()
		{
			StringBuilder passThrusStr = new StringBuilder();

			PassThruStruct passThru = new PassThruStruct();

			for(int k=0; k<this.PassThrus.Count; k++)
			{
				passThru = (PassThruStruct) this.PassThrus[k];

				if ( (passThru.RateName=="All") || (passThru.RateName=="None") )
				{
					switch(passThru.RateName)
					{
						case "All": passThrusStr.Append("All, "); break;
						case "None": passThrusStr.Append("None, "); break;
						default: break;
					}
				}
				else
				{
					if (this.RateSchedulePassThrus.Contains(passThru.RateID))
					{	passThrusStr.Append(this.RateSchedulePassThrus.getPassThruName(passThru.RateID) + ", ");	}
				}
			}

			if (passThrusStr.Length==0)
			{	return "Not Set";	}
			else
			{
				return passThrusStr.ToString(0, passThrusStr.Length - 2);
			}
		}
	}
}
