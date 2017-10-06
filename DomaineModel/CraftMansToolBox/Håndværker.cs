namespace DomaineModel.CraftMansToolBox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Håndværker
	{
		public virtual string Fornavn
		{
			get;
			set;
		}

		public virtual string Efternavn
		{
			get;
			set;
		}

		public virtual string Fagområde
		{
			get;
			set;
		}

		public virtual DateTime Ansættelsedato
		{
			get;
			set;
		}

		public virtual long HID
		{
			get;
			set;
		}

		public virtual ICollection<Værktøjskasse> Ejer_Værktøjskasser
		{
			get;
			set;
		}

	}
}

