using System;
using System.Text;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for CodesBO.
	/// </summary>
	public class CodesBO
	{
		#region "Variables"

		private int var_RateScheduleID;
		private int var_RateID;
		private CodesManager var_CodesMngr;

		private Hashtable var_CodesTable;

		#endregion

		#region "Constructors"

		public CodesBO(ref CodesManager in_codesMngr)	
		{	this.CodesMngr = in_codesMngr; var_CodesTable = new Hashtable();		}

		public CodesBO(ref CodesManager in_codesMngr, int in_RateScheduleID, int in_RateID) : this(ref in_codesMngr)
		{
			this.RateID = in_RateID;
			this.RateScheduleID = in_RateScheduleID;
		}


		#endregion

		#region "Properties"
    
		public int RateID
		{
			get{return var_RateID;}
			set{var_RateID = value;}
		}

		public int RateScheduleID
		{
			get{return var_RateScheduleID;}
			set{var_RateScheduleID = value;}
		}

		public CodesManager CodesMngr
		{
			get{return var_CodesMngr;}
			set{var_CodesMngr = value;}
		}

		#endregion

		#region "Methods"

		public void addCode(string codeType, string code)
		{
			if (! (var_CodesTable.ContainsKey(codeType)) )
			{	var_CodesTable.Add(codeType, new CodeTypeBO(ref var_CodesMngr, codeType));	}


        CodeTypeBO codeTypeObj = (CodeTypeBO) var_CodesTable[codeType];
				codeTypeObj.addCode(code);
		}

		public void addCodes(string codeType, ArrayList codesList)
		{
			if (! (var_CodesTable.ContainsKey(codeType)) )
			{	var_CodesTable.Add(codeType, new CodeTypeBO(ref var_CodesMngr, codeType));	}

			CodeTypeBO codeTypeObj = (CodeTypeBO) var_CodesTable[codeType];
			codeTypeObj.addCodes(codesList);
		}

		public CodeTypeBO getCodeType(string codeType)
		{
			if (this.var_CodesTable.ContainsKey(codeType))
			{	return (CodeTypeBO) this.var_CodesTable[codeType];			}
			else
			{	return null;	}
		}



    #endregion

		public override string ToString()
		{
			StringBuilder codesStr = new StringBuilder();
			CodeTypeBO codeTypeObj;

			foreach(string codeType in var_CodesTable.Keys)
			{
				codeTypeObj = (CodeTypeBO) var_CodesTable[codeType];
				codesStr.Append(codeTypeObj.ToString());
			}

			return codesStr.ToString();
		}

	}
}
