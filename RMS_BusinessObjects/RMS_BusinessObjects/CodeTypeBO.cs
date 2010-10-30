using System;
using System.Collections;
using System.Text;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for CodeTypeBO.
	/// </summary>
	public class CodeTypeBO
	{
		#region "Variables"

		private CodesManager codesMngr;

		private ArrayList var_CodesList;
		private string var_CodeType;
		private string var_Codes;

		#endregion

		#region "Constructors"

		public CodeTypeBO(ref CodesManager cManager, string in_CodeType)
		{	codesMngr = cManager;	 var_CodeType = in_CodeType;}

		public CodeTypeBO(ref CodesManager cManager, string in_CodeType, ArrayList in_CodesList)
		{	codesMngr = cManager; var_CodeType = in_CodeType; var_CodesList = in_CodesList;	}

		#endregion

		#region "Properties"

		public string CodeType
		{
			get{return var_CodeType;}
			set{var_CodeType = value;}
		}

		public int CodeCount
		{
			get{return var_CodesList.Count;}
		}

		#endregion


		#region "Methods"

		public void addCode(string code)
		{
			if (var_CodeType=="DRG")
			{
				code = codesMngr.adjustDRG(code);
			}

			if (codesMngr.validateCode(var_CodeType, code))
			{	var_CodesList.Add(code);	}

			setCodeString();
		}

		public void addCodes(ArrayList codeList)
		{
			var_CodesList = codeList;
			setCodeString();
		}

		private void setCodeString()
		{
			string firstCode = "";
			string lastCode = "";
			// firstCode is the first code entered as a possible interval
			// lastCode is the last code read

			StringBuilder displayStr = new StringBuilder();	// String of codes to be returned by the function

			bool first = true; // used to check if code is first to be read

			string currentCode;

			int count =  0;
			int currentIndex, nextIndex;

			ArrayList masterCodesList = this.codesMngr.getMasterCodesList(var_CodeType);

			for(int k=0; k<var_CodesList.Count; k ++)
			{
				currentCode = Convert.ToString(var_CodesList[k]).Trim();


				if (first)	// if the entry is the first code to be read
				{	firstCode = currentCode; lastCode = currentCode;	first = false;	}
				else if (! (lastCode == currentCode) )
				{
					currentIndex = masterCodesList.BinarySearch(lastCode);
					nextIndex = masterCodesList.BinarySearch(currentCode);

					if ( (nextIndex-currentIndex)==1 ) // Then the codes are connected
					{ count += 1;	 lastCode = currentCode;  }
					else	// the codes are not next to each other so end the interval
					{
						displayStr.Append(getDisplayString(firstCode, lastCode, count));
            
						firstCode = currentCode;
						lastCode = currentCode;
						count = 0;
					}
				}
			}

			displayStr.Append(getDisplayString(firstCode, lastCode, count));
			
			this.var_Codes = displayStr.ToString(0, displayStr.Length - 2); // Remove the last space and comma
		}


		private string getDisplayString(string firstCode, string lastCode, int count)
		{
			if (count==0)
			{	return firstCode + ", ";	}
			else if (count==1)
			{	return firstCode + ", " + lastCode + ", ";	}
			else
			{	return firstCode + "-" + lastCode + ", "; }
		}


		public string getCodesAsString()
		{
			return this.var_Codes;
		}


		public ArrayList getCodesList()
		{
			return var_CodesList;
		}

		#endregion

		#region "Override"

		public override string ToString()
		{
			return var_CodeType + " : " + var_Codes;			
		}





		#endregion
	}
}
