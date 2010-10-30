using System;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Recieves codes and does all validation and string editting.
	/// </summary>
	public class CodesManager
	{
		#region "Variables"

		Hashtable codesTable;

		#endregion


		#region "Constructors"

		public CodesManager()
		{ codesTable = new Hashtable();	}

		#endregion

		#region "Methods"

		public void addCodeList(string codeType, ArrayList codeList)
		{
			codesTable.Add(codeType, codeList);
		}

		public bool validateCode(string codeType, string code)
		{
			ArrayList codeList = (ArrayList) codesTable[codeType];
			if (codeList.Contains(code))
			{	return true;	}
			else
			{
				switch(codeType)
				{
					case "DRG":
					switch (code)
					{
						case "General" : return true;
						default: return false;
					}

					case "RevCode":
					switch (code)
					{
						case "Medical" : return true;
						case "Surgical" : return true;
						default: return false;
					}

					default: return false;
				}
			}
		}

		public int indexOfCode(string codeType, string code)
		{
			ArrayList masterCodesList = (ArrayList) codesTable[codeType];
			return masterCodesList.BinarySearch(code);
		}

		public ArrayList getMasterCodesList(string codeType)
		{
			return (ArrayList) codesTable[codeType];
		}

		public ArrayList getCodesList(string codeType, string in_codes)
		{
			if (! (in_codes=="") )
			{
				ArrayList codesList = new ArrayList();

				string[] codes = in_codes.Split(',');
				string[] codeItems;
				string codeGroup;

				ArrayList masterCodesList = this.getMasterCodesList(codeType);
				int firstIndex, lastIndex;

				foreach(string group in codes)
				{
					codeGroup = group.Trim();
					codeItems = codeGroup.Split('-');

					if (codeItems.Length==2)
					{
						codeItems[0] = codeItems[0].Trim();
						codeItems[1] = codeItems[1].Trim();

						if (codeType=="DRG")
						{
							codeItems[0] = adjustDRG(codeItems[0]);
							codeItems[1] = adjustDRG(codeItems[1]);
						}

						firstIndex = masterCodesList.BinarySearch(codeItems[0]);
						lastIndex = masterCodesList.BinarySearch(codeItems[1]);

						for(int k=firstIndex; k<=lastIndex; k++)
						{
							codesList.Add(masterCodesList[k].ToString());
						}
					}
					else
					{
						if (codeType=="DRG")
						{
							codeGroup = adjustDRG(group);
						}

						codesList.Add(codeGroup);
					}
				}

				return codesList;
			}
			return new ArrayList();
		}

		public string adjustDRG(string code)
		{
			if (code.Length==1)
			{	code = code.Insert(0, "00");	}
			if (code.Length==2)
			{	code = code.Insert(0, "0");	}

			return code;
		}




		#endregion

	}
}
