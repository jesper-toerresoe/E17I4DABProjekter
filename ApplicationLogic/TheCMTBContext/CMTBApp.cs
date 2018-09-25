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
            cmutil.GetFullTreeHåndværkerDB(ref hv);
            return;
            Håndværker nyhv = new Håndværker() { Ansættelsedato = DateTime.Now, Efternavn = "Hansen", Fagområde = "Snedker", Fornavn = "Peter" };
           
            cmutil.AddHåndværkerDB(ref nyhv);

            Håndværker hv1 = new Håndværker() { Fornavn = "Peter", Efternavn = "Hansen" };
            cmutil.GetHåndværkerByName(ref hv1);
            cmutil.GetCurrentHåndværkerById(ref nyhv);
            Værktøjskasse vtk1 = new Værktøjskasse()
            {
                Anskaffet = DateTime.Now,
                Fabrikat = "Bosch",
                Model = "xxf6",
                Serienummer = "3475693",
                Håndværker = hv1.HID,
                Farve = "Grøn",
                Værktøj_i_kassen = new List<Værktøj>()
            };
            hv1.Ejer_Værktøjskasser = new List<Værktøjskasse>();
            hv1.Ejer_Værktøjskasser.Add(vtk1);
            cmutil.AddVærktøjskasseDB(ref vtk1);
            Værktøj vt = new Værktøj()
            {
                Fabrikat = "ToolMate",
                Seriennr = "46745",
                Anskaffet = DateTime.Now,
                Model = "Hammer",
                Værktøjskasse = vtk1.VKID,
                Type = "Lægtehammer"
            };
            vtk1.Værktøj_i_kassen = new List<Værktøj>();
            vtk1.Værktøj_i_kassen.Add(vt);
            cmutil.AddVærktøjDB(ref vt);

            nyhv.Ejer_Værktøjskasser=cmutil.GetHåndværkers_Værktøjskasse(ref hv1);


            



        }
    }
}
