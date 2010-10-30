using System;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for RateCodesBO.
	/// </summary>
	public class RateCodesBO
	{
		#region "Variables"

		private string Codes;
		private ArrayList CodesList;

		#endregion

		public RateCodesBO(string in_Codes, ArrayList in_CodesList)
		{
			this.Codes = in_Codes;
			this.CodesList = in_CodesList;
		}

		#region "OVERRIDES"

		public override string ToString()
		{
			return Codes;
		}

		#endregion

	}
}
