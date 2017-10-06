using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomaineModel.CraftMansToolBox;


namespace Infrastructure.CraftManDBADONET
{
    /// <summary>
    /// The naming concention in this class is both English and Danish. 
    /// The danish terms for entities has been preserved in the code
    /// to be in accordance with RDB.
    /// </summary>
    public class CratftManDBUtil
    {
        private Håndværker currentHåndværker;
        /// <summary>
        /// Constructor may be use to initialize the connection string and likely setup things 
        /// </summary>
        public CratftManDBUtil()
        {
            currentHåndværker = new Håndværker() { HID = 0, Fornavn = "", Efternavn = "", Ejer_Værktøjskasser = null, Fagområde = "", Ansættelsedato = new DateTime() };
        }

        /// <summary>
        /// This a local utility method providing an opne ADO.NET SQL connection
        /// Examples of connection strings are given like below:
        /// new SqlConnection("Data Source=(localdb)\\Projects;Initial Catalog=Opgave1;Integrated Security=True");
        /// new SqlConnection("Data Source=(local);Initial Catalog=Northwind;Integrated Security=SSPI");
        /// new SqlConnection("Data Source=webhotel10.iha.dk;Initial Catalog=E13I4DABH0Gr1;User ID=E13I4DABH0Gr1; Password=E13I4DABH0Gr1");
        /// </summary>
        private SqlConnection OpenConnection
        {
            get
            {
                var con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=CraftManDB;Integrated Security=True");
                con.Open();
                return con;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hv">Håndværker som skal tilføjes påvirker ikke currentHåndværker</param>
        public void AddHåndværkerDB(ref Håndværker hv)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [Håndværker] (Ansættelsedato, Efternavn, Fagområde, Fornavn)
                                                    OUTPUT INSERTED.HåndværkerId  
                                                    VALUES (@Ansættelsedato, @Efternavn,@Fagområde,@Fornavn)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@Ansættelsedato", hv.Ansættelsedato.Date);//.ToString("yyyy-MM-dd HH:mm:ss"); ;
                cmd.Parameters.AddWithValue("@Efternavn", hv.Efternavn);
                cmd.Parameters.AddWithValue("@Fagområde", hv.Fagområde);
                cmd.Parameters.AddWithValue("@Fornavn", hv.Fornavn);
                hv.HID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void UpdateHåndværkerDB(ref Håndværker hv)
        {
            // prepare command string
            string updateString =
               @"UPDATE Håndværker
                        SET Ansættelsedato= @Ansættelsedato, Efternavn = @Efternavn, Fagområde = @Fagområde, Fornavn = @Fornavn
                        WHERE HåndværkerId = @HåndværkerId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@Ansættelsedato", hv.Ansættelsedato.Date);
                cmd.Parameters.AddWithValue("@Fagområde", hv.Fagområde);
                cmd.Parameters.AddWithValue("@Fornavn", hv.Fornavn);
                cmd.Parameters.AddWithValue("@HåndværkerId", hv.HID);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteHåndværkerDB(ref Håndværker hv)
        {
            string deleteString = @"DELETE FROM Håndværker WHERE (HåndværkerId = @HåndværkerId)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@HåndværkerId", hv.HID);

                var id = (int)cmd.ExecuteNonQuery();
                hv = null;
            }
        }

        /// <summary>
        /// Indlæser Håndværkeren som der er fokus på ud fra for- og efternavn. Tager først kommende håndværker (TOP 1)
        /// Sætter currentHåndværker til den entity som findes i databasen
        /// </summary>
        /// <param name="hv">Instans af Hændværker hvor Fornavn og Efternavn er sat</param>
        public void GetHåndværkerByName(ref Håndværker hv)
        {
            string sqlcmd = @"SELECT  TOP 1 * FROM Håndværker WHERE (Fornavn = @fnavn) AND (Efternavn=@enavn)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@fnavn", hv.Fornavn);
                cmd.Parameters.AddWithValue("@enavn", hv.Efternavn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                    currentHåndværker.HID = (int)rdr["HåndværkerId"];
                    currentHåndværker.Ansættelsedato = (DateTime)rdr["Ansættelsedato"];
                    currentHåndværker.Efternavn = (string)rdr["Efternavn"];
                    currentHåndværker.Fagområde = (string)rdr["Fagområde"];
                    currentHåndværker.Fornavn = (string)rdr["Fornavn"];
                    hv = currentHåndværker;
                }
            }
        }



        /// <summary>
        /// Indlæser Håndværkeren som der er fokus på ud fra Primærnøglen!
        /// Sætter currentHåndværker til den entity som findes i databasen

        /// </summary>
        /// <param name="hv"> Instans af Hændværker hvor HID er sat</param>
        public void GetCurrentHåndværkerById(ref Håndværker hv)
        {
            string sqlcmd = @"SELECT [Ansættelsedato],[Efternavn],[Fagområde],[Fornavn] FROM Håndværker WHERE ([HåndværkerId] = @HåndværkerId)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@HåndværkerId", hv.HID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    currentHåndværker.HID = hv.HID;
                    //hv.HID = (int)rdr["HåndværkerId"];//superflous reading from DB and not in projection
                    currentHåndværker.Ansættelsedato = (DateTime)rdr["Ansættelsedato"];
                    currentHåndværker.Efternavn = (string)rdr["Efternavn"];
                    currentHåndværker.Fagområde = (string)rdr["Fagområde"];
                    currentHåndværker.Fornavn = (string)rdr["Fornavn"];
                    hv = currentHåndværker;
                }
            }
        }

        public List<Håndværker> GetHåndværkere()
        {
            string sqlcmd = @"SELECT * FROM Håndværker";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                List<Håndværker> hver = new List<Håndværker>();
                Håndværker hv = null;
                while (rdr.Read())
                {
                    hv = new Håndværker();
                    hv.HID = (int)rdr["HåndværkerId"];
                    hv.Ansættelsedato = (DateTime)rdr["Ansættelsedato"];
                    hv.Efternavn = (string)rdr["Efternavn"];
                    hv.Fagområde = (string)rdr["Fagområde"];
                    hv.Fornavn = (string)rdr["Fornavn"];
                    hver.Add(hv);
                }
                return hver;
            }

        }

        public List<Værktøjskasse> GetHåndværkers_Værktøjskasse(ref Håndværker hv)
        {
            string selectToolboxToolString = @"SELECT *
                                                  FROM [Værktøjskasse] 
                                                  WHERE ([Håndværker] = @HVId)";
            using (var cmd = new SqlCommand(selectToolboxToolString, OpenConnection))
            {

                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@HVId", hv.HID);
                rdr = cmd.ExecuteReader();
                List<Værktøjskasse> boxestohv = new List<Værktøjskasse>();
                Værktøjskasse vk = null;
                while (rdr.Read())
                {
                    vk = new Værktøjskasse(); // Ny instans i hver gennemløb!

                    vk.VKID = (int)rdr["VKasseId"];
                    vk.Anskaffet = (DateTime)rdr["Anskaffet"];
                    vk.Fabrikat = (string)rdr["Fabrikat"];
                    vk.Farve = (string)rdr["Farve"];
                    vk.Håndværker = (int)rdr["Håndværker"];
                    vk.Model = (string)rdr["Model"];
                    vk.Serienummer = (string)rdr["Serienummer"];
                    boxestohv.Add(vk);
                }
                return boxestohv;
            }
        }

        public List<Værktøj> GetVærktøj_In_Værktøjskasse(ref Værktøjskasse vtk)
        {
            string selectToolboxToolString = @"SELECT *
                                                  FROM Værktøj 
                                                  WHERE (Værktøjskasse = @VKId)";

            using (var cmd = new SqlCommand(selectToolboxToolString, OpenConnection))
            {
                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@VKId", vtk.VKID);
                rdr = cmd.ExecuteReader();
                List<Værktøj> vtinbox = new List<Værktøj>();
                Værktøj vt = null;
                while (rdr.Read())
                {
                    vt = new Værktøj(); // 

                    vt.VTID = (int)rdr["VærktøjsId"];
                    vt.Anskaffet = (DateTime)rdr["Anskaffet"];
                    vt.Fabrikat = (string)rdr["Fabrikat"];
                    vt.Værktøjskasse = (int)rdr["Værktøjskasse"];
                    vt.Model = (string)rdr["Model"];
                    vt.Seriennr = (string)rdr["Serienummer"];
                    vt.Type = (string)rdr["Type"];
                    vtinbox.Add(vt);
                }
                return vtinbox;
            }
        }

        public void AddVærktøjskasseDB(ref Værktøjskasse vtk)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [Værktøjskasse] (Anskaffet, Fabrikat,Håndværker,Farve, Model, Serienummer)
                                                    OUTPUT INSERTED.VKasseId
                                                    VALUES  (@Anskaffet, @Fabrikat,@Håndværker,@Farve,@Model,@Serienummer)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@Anskaffet", vtk.Anskaffet.Date);
                cmd.Parameters.AddWithValue("@Fabrikat", vtk.Fabrikat);
                cmd.Parameters.AddWithValue("@Håndværker", vtk.Håndværker);
                cmd.Parameters.AddWithValue("@Farve", vtk.Farve);
                cmd.Parameters.AddWithValue("@Model", vtk.Model);
                cmd.Parameters.AddWithValue("@Serienummer", vtk.Serienummer);
                vtk.VKID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }
        public void GetVærktøjskasseById(ref Værktøjskasse vk)
        {
            string sqlcmd = @"SELECT * FROM Værktøjskasse WHERE (VKasseID = @VKasseID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@VKasseID", vk.VKID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    //Værktøjskasse curvk = new Værktøjskasse();
                    vk.VKID = (long)rdr["VærktøjsId"];
                    vk.Anskaffet = (DateTime)rdr["Anskaffet"];
                    vk.Fabrikat = (string)rdr["Fabrikat"];
                    vk.Håndværker = (long)rdr["Håndværker"];
                    vk.Model = (string)rdr["Model"];
                    vk.Serienummer = (string)rdr["Serienummer"];
                    vk.Farve = (string)rdr["Farve"];
                }
            }
        }
        public void UpdateVærktøjskasse(ref Værktøjskasse vtk)
        {
            // prepare command string
            string updateString =
               @"UPDATE Værktøjskasse                 
                        SET Anskaffet= @Anskaffet, Fabrikat = @Fabrikat, Farve =@Farve,
                            Model = @Model, Serienummer = @Serienummer, Håndværker = @Håndværker 
                        WHERE VKasseId = @VKasseId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@Anskaffet", vtk.Anskaffet.Date);
                cmd.Parameters.AddWithValue("@Fabrikat", vtk.Fabrikat);
                cmd.Parameters.AddWithValue("@Farve", vtk.Farve);
                cmd.Parameters.AddWithValue("@Model", vtk.Model);
                cmd.Parameters.AddWithValue("@Serienummer", vtk.Serienummer);
                cmd.Parameters.AddWithValue("@Håndværker", vtk.Håndværker);
                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteVærktøjskasseDB(ref Værktøjskasse vtk)
        {
            string deleteString = @"DELETE FROM Værktøjskasse WHERE (VKasseId = @VKasseId)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@VKasseId", vtk.VKID);

                var id = (int)cmd.ExecuteNonQuery();
                vtk = null;
            }
        }

        public void AddVærktøjDB(ref Værktøj vt)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO [Værktøj] (Anskaffet, Fabrikat, Model, Serienr,Type,Værktøjskasse)
                                                    OUTPUT INSERTED.VærktøjsId
                                                    VALUES  (@Anskaffet, @Fabrikat,@Model,@Serienr,@Type,@Værktøjskasse)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@Anskaffet", vt.Anskaffet.Date);
                cmd.Parameters.AddWithValue("@Fabrikat", vt.Fabrikat);
                cmd.Parameters.AddWithValue("@Model", vt.Model);
                cmd.Parameters.AddWithValue("@Serienr", vt.Seriennr);
                cmd.Parameters.AddWithValue("@Type", vt.Type);
                cmd.Parameters.AddWithValue("@Værktøjskasse", vt.Værktøjskasse);
                vt.VTID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void UpdateVærktøjDB(ref Værktøj vt)
        {
            // prepare command string
            string updateString =
               @"UPDATE Værktøj                 
                        SET Anskaffet= @Anskaffet, Fabrikat = @Fabrikat, Model = @Model, 
                            Serienummer = @Serienummer, Type = @Type, Værktøjskasse = @Værktøjskasse
                        WHERE VKasseId = @VKasseId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@Anskaffet", vt.Anskaffet.Date);
                cmd.Parameters.AddWithValue("@Fabrikat", vt.Fabrikat);
                cmd.Parameters.AddWithValue("@Model", vt.Model);
                cmd.Parameters.AddWithValue("@Serienummer", vt.Seriennr);
                cmd.Parameters.AddWithValue("@Type", vt.Type);
                cmd.Parameters.AddWithValue("@Værktøjskasse", vt.Værktøjskasse);
                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteVærktøjDB(ref Værktøj vt)
        {
            string deleteString = @"DELETE FROM Værktøj WHERE (VærktøjsId = @VtId)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@VtId", vt.VTID);

                var id = (int)cmd.ExecuteNonQuery();
                vt = null;
            }
        }

        /*
         * To Do Udføre de to utility metoder som benytter følgende SELECT erklæringer
         * 
         SELECT        Håndværker.HåndværkerId, Håndværker.Ansættelsedato, Håndværker.Efternavn, Håndværker.Fagområde, Håndværker.Fornavn, Værktøjskasse.VKasseId, Værktøjskasse.Anskaffet, Værktøjskasse.Fabrikat, Værktøj.VærktøjsId, 
                       Værktøj.Anskaffet AS Expr1, Værktøj.Fabrikat AS Expr2, Værktøj.Model, Værktøj.Serienr, Værktøj.Type
         FROM            Håndværker LEFT OUTER JOIN
                         Værktøjskasse ON Håndværker.HåndværkerId = Værktøjskasse.Håndværker LEFT OUTER JOIN
                         Værktøj ON Værktøjskasse.VKasseId = Værktøj.Værktøjskasse
         * og
         * 
          SELECT        Håndværker.HåndværkerId, Håndværker.Ansættelsedato, Håndværker.Efternavn, Håndværker.Fagområde, Håndværker.Fornavn, Værktøjskasse.VKasseId, Værktøjskasse.Anskaffet, Værktøjskasse.Fabrikat, Værktøjskasse.Model, 
                        Værktøjskasse.Serienummer, Værktøjskasse.Farve, Værktøj.VærktøjsId, Værktøj.Anskaffet AS Expr1, Værktøj.Fabrikat AS Expr2, Værktøj.Model AS Expr3, Værktøj.Serienr, Værktøj.Type
          FROM          Håndværker INNER JOIN
                        Værktøjskasse ON Håndværker.HåndværkerId = Værktøjskasse.Håndværker INNER JOIN
                        Værktøj ON Værktøjskasse.VKasseId = Værktøj.Værktøjskasse
         * 
         */


    }
}
