using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// This object will act as a collection type for Per Diem rates.
	/// </summary>
	/// 

	public class Rate_Collection
	{

		#region "Variables"

		private Hashtable Rates;
		private int var_Count;

		#endregion

		#region "Constructors"

		public Rate_Collection()
		{	Rates = new Hashtable(); var_Count=0;		}

		#endregion

		#region "Properties"

		public int Count
		{
			get { return var_Count;	}
		}

		#endregion


		#region "Methods"

		public void addRate(RateBO rateToAdd)
		{
			if (! (rateToAdd==null) )
			{

				Hashtable patientTable, rateCategoryTable;
				ArrayList ratesList;

				if (Rates.ContainsKey(rateToAdd.InOut) )
				{
					patientTable = (Hashtable) Rates[rateToAdd.InOut];
				}
				else
				{
					patientTable = new Hashtable();
					Rates.Add(rateToAdd.InOut, patientTable);
				}

				if (patientTable.ContainsKey(rateToAdd.RateCategory))
				{
					rateCategoryTable = (Hashtable) patientTable[rateToAdd.RateCategory];
				}
				else
				{
					rateCategoryTable = new Hashtable();
					patientTable.Add(rateToAdd.RateCategory, rateCategoryTable);
				}

				if (rateCategoryTable.ContainsKey(rateToAdd.RateType))
				{
					ratesList = (ArrayList) rateCategoryTable[rateToAdd.RateType];
				}
				else
				{
					ratesList = new ArrayList();
					rateCategoryTable.Add(rateToAdd.RateType, ratesList);
				}

				this.var_Count ++;
				ratesList.Add(rateToAdd);
			}
		}


		private ArrayList getKeys(Hashtable hTable)
		{
			ArrayList listToReturn = new ArrayList();

			foreach(object key in hTable.Keys)
			{
				listToReturn.Add(key);
			}

			return listToReturn;
		}

		public ArrayList getPatientTypes()
		{
			return getKeys(Rates);
		}

		public ArrayList getRateCategories(char inOut)
		{
      return getKeys((Hashtable) Rates[inOut]);
		}

		public ArrayList getRateTypes(char inOut, string rateCategory)
		{
      return getKeys(    ((Hashtable) ((Hashtable) Rates[inOut])[rateCategory]) );
		}


		public ArrayList getRateList()
		{
			ArrayList rateList = new ArrayList();

			ArrayList patientTypesList = this.getPatientTypes();
			ArrayList rateCategoriesList;
			ArrayList rateTypesList;
			ArrayList currentRatesList;

			foreach(char inOut in patientTypesList)
			{
				rateCategoriesList = this.getRateCategories(inOut);

				foreach(string rateCategory in rateCategoriesList)
				{
					rateTypesList = this.getRateTypes(inOut, rateCategory);

					foreach(string rateType in rateTypesList)
					{
						currentRatesList = (ArrayList) ((Hashtable)((Hashtable)Rates[inOut])[rateCategory])[rateType];

						for(int k=0; k<currentRatesList.Count; k++)
						{
							rateList.Add(currentRatesList[k]);
						}
					}
				}
			}

			return rateList;
		}


		public DataTable getDataTable(char inOut, string rateCategory, string rateType)
		{
			ArrayList ratesList = (ArrayList) ((Hashtable)((Hashtable)Rates[inOut])[rateCategory])[rateType];

			DataTable ratesTable = new DataTable();
			ratesTable.TableName = rateCategory;

			RateBO rateTemplate = (RateBO) ratesList[0];

			ArrayList columnsList = rateTemplate.getDataColumns();

			for(int k=0; k<columnsList.Count;k++)
			{
				ratesTable.Columns.Add( (DataColumn) columnsList[k]);
			}

			RateBO currentRate;	
			DataRow rateRow;

			for(int k=0; k<ratesList.Count; k++)
			{
				currentRate = (RateBO) ratesList[k];

				rateRow = ratesTable.NewRow();

				currentRate.fillRateRow(ref rateRow);

				ratesTable.Rows.Add(rateRow);
			}

			return ratesTable;
		}


		public void increaseRatesByPercent(double percentToIncrease, bool includePOCs, double chargeMasterIncrease)
		{
			ArrayList patientTypesList = this.getPatientTypes();
			ArrayList rateCategoriesList;
			ArrayList rateTypesList;
			ArrayList currentRatesList;

			foreach(char inOut in patientTypesList)
			{
				rateCategoriesList = this.getRateCategories(inOut);

				foreach(string rateCategory in rateCategoriesList)
				{
					rateTypesList = this.getRateTypes(inOut, rateCategory);

					foreach(string rateType in rateTypesList)
					{
						currentRatesList = (ArrayList) ((Hashtable)((Hashtable)Rates[inOut])[rateCategory])[rateType];

						if (!(rateType=="POC"))
						{
							for(int k=0; k<currentRatesList.Count; k++)
							{	((RateBO)currentRatesList[k]).increaseRateByPercent(percentToIncrease);	}
						}
						else if ( (rateType=="POC") && includePOCs)
						{
							double pocIncrease = percentToIncrease / chargeMasterIncrease;
							for(int k=0; k<currentRatesList.Count; k++)
							{	((RateBO)currentRatesList[k]).increaseRateByPercent(pocIncrease);	}
						}
					}
				}
			}
		}

		

		public ArrayList getRateList(char inOut, string rateCategory, string rateType)
		{
			return (ArrayList) ((Hashtable)((Hashtable)Rates[inOut])[rateCategory])[rateType];
		}

		#endregion


	}
}
