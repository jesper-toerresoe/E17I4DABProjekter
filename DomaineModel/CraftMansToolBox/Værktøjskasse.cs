namespace DomaineModel.CraftMansToolBox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Værktøjskasse
    {
        public virtual DateTime Anskaffet
        {
            get;
            set;
        }

        public virtual string Fabrikat
        {
            get;
            set;
        }

        public virtual string Model
        {
            get;
            set;
        }

        public virtual string Serienummer
        {
            get;
            set;
        }

        public virtual long Håndværker
        {
            get;
            set;
        }

        public virtual long VKID
        {
            get;
            set;
        }
        public virtual string Farve { get; set; }

        public virtual ICollection<Værktøj> Værktøj_i_kassen
        {
            get;
            set;
        }

    }
}

