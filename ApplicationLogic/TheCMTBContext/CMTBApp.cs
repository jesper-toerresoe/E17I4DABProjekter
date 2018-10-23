using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomaineModel.CraftMansToolBox;
using Infrastructure.CraftManDBADONET;


namespace ApplicationLogic.TheCMTBContext
{
    public class CMTBApp
    {
        public void TheApp()
        {
            CratftManDBUtil cmutil = new CratftManDBUtil();

            Håndværker hv = new Håndværker() { HID = 1 };
            var hvlist = cmutil.GetHåndværkere();
            cmutil.GetFullTreeHåndværkerDB(ref hv);
            var vtklist = cmutil.GetHåndværkers_Værktøjskasse(ref hv);

            Håndværker nyhv = new Håndværker() { Ansættelsedato = DateTime.Now, Efternavn = "Hansen", Fagområde = "Snedker", Fornavn = "Peter" };

            //cmutil.AddHåndværkerDB(ref nyhv);

            Håndværker hv1 = new Håndværker() { Fornavn = "Peter", Efternavn = "Hansen" };
            cmutil.GetHåndværkerByName(ref hv1);
            //cmutil.GetCurrentHåndværkerById(ref nyhv);
            Værktøjskasse vtk1 = new Værktøjskasse()
            {
                Anskaffet = DateTime.Now,
                Fabrikat = "Bosch",
                Model = "xxf6",
                Serienummer = "3475693",
                Håndværker = hv1.HID,
                EjesAf =hv1, //Scaffolding struktur opsættes
                Farve = "Grøn",
                Værktøj_i_kassen = new List<Værktøj>()
            };
            hv1.Ejer_Værktøjskasser = new List<Værktøjskasse>();
            hv1.Ejer_Værktøjskasser.Add(vtk1);//Scaffolding struktur opsættes
            cmutil.AddVærktøjskasseDB(ref vtk1);
            Værktøj vt = new Værktøj()
            {
                Fabrikat = "ToolMate",
                Seriennr = "46745",
                Anskaffet = DateTime.Now,
                Model = "Hammer",
                Værktøjskasse = vtk1.VKID,
                LiggerI = vtk1, //Scaffolding struktur opsættes
                Type = "Lægtehammer"
            };
            vtk1.Værktøj_i_kassen = new List<Værktøj>(); 
            vtk1.Værktøj_i_kassen.Add(vt);//Scaffolding struktur opsættes
            cmutil.AddVærktøjDB(ref vt);

            nyhv.Ejer_Værktøjskasser=cmutil.GetHåndværkers_Værktøjskasse(ref hv1);


            



        }
    }
}
