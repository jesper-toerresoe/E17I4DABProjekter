namespace DomaineModel.CraftMansToolBox
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
  

	public class Værktøj
	{
		public virtual string Fabrikat
		{
			get;
			set;
		}

		public virtual string Seriennr
		{
			get;
			set;
		}

		public virtual DateTime Anskaffet
		{
			get;
			set;
		}

		public virtual string Model
		{
			get;
			set;
		}

		public virtual long Værktøjskasse
		{
			get;
			set;
		}

		public virtual string Type
		{
			get;
			set;
		}

		public virtual long VTID
		{
			get;
			set;
		}

	}
}

