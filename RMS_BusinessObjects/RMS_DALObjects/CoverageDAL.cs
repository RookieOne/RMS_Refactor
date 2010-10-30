using System;
using System.Data;
using System.Data.SqlClient;

using RMS_BusinessObjects;

namespace RMS_DALObjects
{
	/// <summary>
	/// Summary description for CoverageDAL.
	/// </summary>
	public class CoverageDAL : BaseDALObject
	{
		#region "Data Fields"

		// Entity_RateSched Fields
		static int fld_Entity_EntityCode = 0;
		static int fld_Entity_RateSchedSeqNum = 1;

		// InsurncPlanCode Fields
		static int fld_InsurncPlanCode_InsurncPlanCode = 0;
		static int fld_InsurncPlanCode_RateSchedSeqNum = 1;

		#endregion

		#region "Constructors"

		public CoverageDAL()	{}

		#endregion

		#region "Methods"

		public CoverageBO getRateScheduleCoverage(int rateScheduleID)
		{
			SqlDataReader sqlDataRdr = this.GetDataReader("SELECT * FROM RateSched WHERE RateSchedSeqNum=" + rateScheduleID);

			CoverageBO coverage = new CoverageBO();

			if (sqlDataRdr.Read())
			{
				coverage.StartDate = (DateTime) sqlDataRdr["EffStartDate"];
				coverage.EndDate = (DateTime) sqlDataRdr["EffEndDate"];

				coverage.Entities = this.getEntitiesCovered(rateScheduleID);
				coverage.InsurancePlans = this.getInsurancePlansCovered(rateScheduleID);
			}

			this.CloseConnection();

			return coverage;
		}


		public Entity_Collection getEntitiesCovered(int rateScheduleID)
		{
			SqlDataReader sqlDataRdr = this.GetDataReader("SELECT Entity.EntityCode as EntityCode, CompanyCode, EntityDescr FROM Entity, Entity_RateSched WHERE Entity.EntityCode=Entity_RateSched.EntityCode AND RateSchedSeqNum=" + rateScheduleID);

      Entity_Collection entities = new Entity_Collection();
      EntityBO entityToAdd;

			while(sqlDataRdr.Read())
			{
				entityToAdd = new EntityBO();
				entityToAdd.CompanyCode = sqlDataRdr["CompanyCode"].ToString();
				entityToAdd.EntityCode = sqlDataRdr["EntityCode"].ToString();
				entityToAdd.Name = sqlDataRdr["EntityDescr"].ToString();

				entities.addEntity(entityToAdd);
			}

			this.CloseConnection();

			return entities;
		}


		public Entity_Collection getEntitiesCovered()
		{
			SqlDataReader sqlDataRdr = this.GetDataReader("SELECT EntityCode, CompanyCode, EntityDescr FROM Entity");

			Entity_Collection entities = new Entity_Collection();
			EntityBO entityToAdd;

			while(sqlDataRdr.Read())
			{
				entityToAdd = new EntityBO();
				entityToAdd.CompanyCode = sqlDataRdr["CompanyCode"].ToString();
				entityToAdd.EntityCode = sqlDataRdr["EntityCode"].ToString();
				entityToAdd.Name = sqlDataRdr["EntityDescr"].ToString();

				entities.addEntity(entityToAdd);
			}

			this.CloseConnection();

			return entities;
		}



		public InsurancePlan_Collection getInsurancePlansCovered(int rateScheduleID)
		{
			SqlDataReader sqlDataRdr = this.GetDataReader("SELECT PayorMaster.InsurncPlanCode as InsPlanCode, LongDescr, NRContinuumText, TierText, TSIFinclClassPlusText FROM InsurncPlanCode, PayorMaster WHERE InsurncPlanCode.InsurncPlanCode=PayorMaster.InsurncPlanCode AND RateSchedSeqNum=" + rateScheduleID);

			InsurancePlan_Collection insPlans = new InsurancePlan_Collection();
			InsurancePlanBO insPlanToAdd;

			while(sqlDataRdr.Read())
			{
				insPlanToAdd = new InsurancePlanBO();
				insPlanToAdd.InsurancePlanCode = sqlDataRdr["InsPlanCode"].ToString();
				insPlanToAdd.Description = sqlDataRdr["LongDescr"].ToString();
				insPlanToAdd.FinancialClass = sqlDataRdr["TSIFinclClassPlusText"].ToString();
				insPlanToAdd.NetRevenueContinuum = sqlDataRdr["NRContinuumText"].ToString();
				insPlanToAdd.Tier = sqlDataRdr["TierText"].ToString();

				insPlans.addInsurancePlan(insPlanToAdd);
			}

			this.CloseConnection();

			return insPlans;
		}


		public InsurancePlan_Collection getInsurancePlansCovered()
		{
			SqlDataReader sqlDataRdr = this.GetDataReader("SELECT InsurncPlanCode as InsPlanCode, LongDescr, NRContinuumText, TierText, TSIFinclClassPlusText FROM PayorMaster");

			InsurancePlan_Collection insPlans = new InsurancePlan_Collection();
			InsurancePlanBO insPlanToAdd;

			while(sqlDataRdr.Read())
			{
				insPlanToAdd = new InsurancePlanBO();
				insPlanToAdd.InsurancePlanCode = sqlDataRdr["InsPlanCode"].ToString();
				insPlanToAdd.Description = sqlDataRdr["LongDescr"].ToString();
				insPlanToAdd.FinancialClass = sqlDataRdr["TSIFinclClassPlusText"].ToString();
				insPlanToAdd.NetRevenueContinuum = sqlDataRdr["NRContinuumText"].ToString();
				insPlanToAdd.Tier = sqlDataRdr["TierText"].ToString();

				insPlans.addInsurancePlan(insPlanToAdd);
			}

			this.CloseConnection();

			return insPlans;
		}



		#endregion

		#region "Database Methods"

		public int insertCoverage(CoverageBO coverage)
		{

			return 0;
		}

		public int updateCoverage(CoverageBO coverage)
		{

			return 0;

		}

		public void deleteCoverage(CoverageBO coverage)
		{
			InsurancePlanBO insPlan;
			for(int i=0; i<coverage.InsurancePlans.Count; i++)
			{
				insPlan = (InsurancePlanBO) coverage.InsurancePlans.getInsurancePlanAt(i);
				this.deleteInsurancePlanCode(coverage.RateScheduleID, insPlan.InsurancePlanCode);
			}

			EntityBO entity;
			for(int e=0; e<coverage.Entities.Count; e++)
			{
				entity = (EntityBO) coverage.Entities.getEntityAt(e);
				this.deleteEntity(coverage.RateScheduleID, entity.EntityCode);
			}
		}

		public void deleteCoverage(int rateScheduleID)
		{
			SqlDataReader sqlDataRdr = base.GetDataReader("SELECT * FROM InsurncPlanCode WHERE RateSchedSeqNum=" + rateScheduleID);

			while(sqlDataRdr.Read())
			{
				deleteInsurancePlanCode(rateScheduleID, sqlDataRdr["InsurncPlanCode"].ToString());
			}


			sqlDataRdr = base.GetDataReader("SELECT * FROM Entity_RateSched WHERE RateSchedSeqNum=" + rateScheduleID);

			while(sqlDataRdr.Read())
			{
				deleteEntity(rateScheduleID, sqlDataRdr["EntityCode"].ToString());
			}

			base.CloseConnection();
		}

		private void deleteInsurancePlanCode(int rateScheduleID, string insurancePlanCode)
		{
			SqlParameter[] sqlParams = base.GetParameters("DeleteInsurncPlanCode");

			sqlParams[fld_InsurncPlanCode_RateSchedSeqNum].Value = rateScheduleID;
			sqlParams[fld_InsurncPlanCode_InsurncPlanCode].Value = insurancePlanCode;
			
			base.ExecuteDelete("DeleteInsurncPlanCode", sqlParams);
		}

		private void deleteEntity(int rateScheduleID, string entityCode)
		{
			SqlParameter[] sqlParams = base.GetParameters("DeleteEntity_RateSched");

			sqlParams[fld_Entity_RateSchedSeqNum].Value = rateScheduleID;
			sqlParams[fld_Entity_EntityCode].Value = entityCode;
			
			base.ExecuteDelete("DeleteEntity_RateSched", sqlParams);
		}

		#endregion

	}
}
