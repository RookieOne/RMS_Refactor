using System;
using System.Collections;

namespace RMS_BusinessObjects
{
	/// <summary>
	/// Summary description for Entity_Collection.
	/// </summary>
	public class Entity_Collection
	{
		#region "Variables"

		ArrayList entitiesList;

		#endregion

		#region "Constructors"

		public Entity_Collection()		{ entitiesList = new ArrayList();		}

		#endregion

		#region "Properties"

		public int Count
		{	get{ return entitiesList.Count;	}	}

		#endregion

		#region "Methods"

		public void addEntity(EntityBO entityToAdd)
		{	entitiesList.Add(entityToAdd);	}

		public EntityBO getEntityAt(int index)
		{	return (EntityBO) entitiesList[index];	}


		public string getEntityFilter()
		{
			if (entitiesList.Count == 0)
			{
				return "";
			}
			else
			{
				string filter = "(";
				for(int k=0; k<entitiesList.Count; k++)
				{	filter += "'" + ( (EntityBO) entitiesList[k]).CompanyCode + "',";		}

				filter = filter.Substring(0, filter.Length-1);
				filter += ")";

				return filter;
			}
		}

		#endregion

		#region "Overrides"


		#endregion
	}
}
