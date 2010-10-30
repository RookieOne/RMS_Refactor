using System;

namespace RMS_BusinessObjects
{

	/// <summary>
	/// This is the base business object used for the entire RMS business object library. 
	/// 
	/// Constructed with help using code and directions found at http://www.codeproject.com/dotnet/UIPAB1.asp
	/// 
	/// </summary>
	/// 
	[Serializable] public abstract class baseBusinessObject
	{
		private int id;    

		public baseBusinessObject():this(0){}
		public baseBusinessObject(int id)
		{
			this.id = id;
		}

		public int ID
		{
			get{return id;}
			set{id = value;}
		}

	}

}
