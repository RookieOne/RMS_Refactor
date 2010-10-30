using System;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for Rate_W_PassThruBO.
	/// </summary>
	public abstract class Rate_W_PassThruBO : RateBO
	{
		#region "Variables"

		PassThrusBO var_PassThrus;

		#endregion

		#region "Constructors"

		public Rate_W_PassThruBO() : base()	{this.HasPassThrus = true;}
		public Rate_W_PassThruBO(int id) : base(id) {this.HasPassThrus = true;}
		public Rate_W_PassThruBO(int id, int in_RateSchedID) : base(id, in_RateSchedID) {this.HasPassThrus = true;}
		public Rate_W_PassThruBO(int id, int in_RateSchedID, string in_RateName, string in_RateCategory, string in_RateType, char in_InOut, CodesBO in_RateCodes) : base(id, in_RateSchedID, in_RateName, in_RateCategory, in_RateType, in_InOut, in_RateCodes)
	{this.HasPassThrus = true;}

		#endregion


		#region "Properties"

		public PassThrusBO PassThrus
		{
			get{return var_PassThrus;}
			set{var_PassThrus = value;}
		}


		#endregion

	}
}
