using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;


namespace E17StartDocumentDB
{
    class Program
    {
        /*Eksempel fra tutorial
         * https://docs.microsoft.com/en-us/azure/cosmos-db/documentdb-get-started
         */
        private const string EndpointUrl = "https://e17i4dabdodb.documents.azure.com:443/";
        private const string PrimaryKey = "o7Ov8yK49lIACcHepJaO2t4AlDrrf06Yn8iJhOPDYLxlqJHV6AW56I2Zegs2MddKna3ELV3YKoT3ScCMDSHRww==";
        private const string EndpointUrlLocal = "https://localhost:8081/";
        private const string PrimaryKeyLocal = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private const string EndpointUrlI4DAB = "https://i4dab.ase.au.dk:8081/";

        private DocumentClient client;

        static void Main(string[] args)
        {
            try
            {
                Program p = new Program();
                //p.client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);
                p.client = new DocumentClient(new Uri(EndpointUrlLocal), PrimaryKeyLocal);
                //p.client = new DocumentClient(new Uri(EndpointUrlI4DAB), PrimaryKeyLocal);
                p.GetStartedDemo().Wait();
                p.GetStartedDemo2().Wait();
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
        }

        private async Task GetStartedDemo()
        {
            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = "FamilyDB" });

            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("FamilyDB"), new DocumentCollection { Id = "FamilyCollection" });

            Family andersenFamily = new Family
            {
                Id = "Andersen.1",
                LastName = "Andersen",
                Parents = new Parent[]
                    {
                new Parent { FirstName = "Thomas" },
                new Parent { FirstName = "Mary Kay" }
                    },
                Children = new Child[]
                    {
                new Child
                {
                        FirstName = "Henriette Thaulow",
                        Gender = "female",
                        Grade = 5,
                        Pets = new Pet[]
                        {
                                new Pet { GivenName = "Fluffy" }
                        }
                }
                    },
                Address = new Address { State = "WA", County = "King", City = "Seattle" },
                IsRegistered = true
            };

            await this.CreateFamilyDocumentIfNotExists("FamilyDB", "FamilyCollection", andersenFamily);

            Family wakefieldFamily = new Family
            {
                Id = "Wakefield.7",
                LastName = "Wakefield",
                Parents = new Parent[]
                    {
                new Parent { FamilyName = "Wakefield", FirstName = "Robin" },
                new Parent { FamilyName = "Miller", FirstName = "Ben" }
                    },
                Children = new Child[]
                    {
                new Child
                {
                        FamilyName = "Merriam",
                        FirstName = "Jesse",
                        Gender = "female",
                        Grade = 8,
                        Pets = new Pet[]
                        {
                                new Pet { GivenName = "Goofy" },
                                new Pet { GivenName = "Shadow" }
                        }
                },
                new Child
                {
                        FamilyName = "Miller",
                        FirstName = "Lisa",
                        Gender = "female",
                        Grade = 1
                }
                    },
                Address = new Address { State = "NY", County = "Manhattan", City = "NY" },
                IsRegistered = false
            };

            await this.CreateFamilyDocumentIfNotExists("FamilyDB", "FamilyCollection", wakefieldFamily);

            this.ExecuteSimpleQuery("FamilyDB", "FamilyCollection");


        }
        private async Task GetStartedDemo2()
        {
            try
            {
                await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = "TeacherDB" });


                await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("TeacherDB"), new DocumentCollection { Id = "ASECollection" });
            }
            catch (Exception e)
            {

                this.WriteToConsoleAndPromptToContinue("Error accesing Cosmos DB", e);
            }
           


            Course[] insertcourse = new Course[] { new Course { CatalogName = "I4DAB", CourseNo = 33322 } };
            Teacher[] insertteachers = new Teacher[] { new Teacher { FamilyName = "Sørensen", FirstName = "Søren", Title = "Associted Professor", OfficeNo = 309, Courses = insertcourse } };
            Section[] insertsections = new Section[] { new Section { SectionName = "ECE", Building = new Building { BuildingName = "Edison 5125", Street = "Finlandsgade 22", ZipCode = "8200", City = "Aarhus N" },Teachers = insertteachers } };

            Faculty aseFaculty = new Faculty()
            {
                FacultyId = "ECE.ASE.AU.DK.8",
                FacultyName = "Aarhus University, Science and Technology Faculty",
                Departments = new Department[]
                    {
                new Department { DepartmentName ="ASE", Building = new Building {BuildingName ="Navitas 3210",Street ="nge Lehmanns Gade 10",ZipCode ="8000",City ="Aarhus C" },Sections= insertsections }
                    },
                Building = new Building { BuildingName = "Navitas", Street = "Inge Lehmans Alle 10", ZipCode = "8000", City = "Aarhus" }

            };


            await this.CreateFacultyDocumentIfNotExists("TeacherDB", "ASECollection", aseFaculty);
        }

        private void WriteToConsoleAndPromptToContinue(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        public class Family
        {
            [JsonProperty(PropertyName = "id")] //makes unique id/key to the document
            public string Id { get; set; }
            public string LastName { get; set; }
            public Parent[] Parents { get; set; }
            public Child[] Children { get; set; }
            public Address Address { get; set; }
            public bool IsRegistered { get; set; }
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        public class Parent
        {
            public string FamilyName { get; set; }
            public string FirstName { get; set; }
        }

        public class Child
        {
            public string FamilyName { get; set; }
            public string FirstName { get; set; }
            public string Gender { get; set; }
            public int Grade { get; set; }
            public Pet[] Pets { get; set; }
        }

        public class Pet
        {
            public string GivenName { get; set; }
        }

        public class Address
        {
            public string State { get; set; }
            public string County { get; set; }
            public string City { get; set; }
        }

        private async Task CreateFamilyDocumentIfNotExists(string databaseName, string collectionName, Family family)
        {
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, family.Id));
                this.WriteToConsoleAndPromptToContinue("Found {0}", family.Id);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), family);
                    this.WriteToConsoleAndPromptToContinue("Created Family {0}", family.Id);
                }
                else
                {
                    throw;
                }
            }
        }

        private void ExecuteSimpleQuery(string databaseName, string collectionName)
        {
            // Set some common query options
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            // Here we find the Andersen family via its LastName
            IQueryable<Family> familyQuery = this.client.CreateDocumentQuery<Family>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), queryOptions)
                    .Where(f => f.LastName == "Andersen");

            // The query is executed synchronously here, but can also be executed asynchronously via the IDocumentQuery<T> interface
            Console.WriteLine("Running LINQ query...");
            foreach (Family family in familyQuery)
            {
                Console.WriteLine("\tRead {0}", family);
            }

            // Now execute the same query via direct SQL
            IQueryable<Family> familyQueryInSql = this.client.CreateDocumentQuery<Family>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                    "SELECT * FROM Family WHERE Family.LastName = 'Andersen'",
                    queryOptions);

            Console.WriteLine("Running direct SQL query...");
            foreach (Family family in familyQueryInSql)
            {
                Console.WriteLine("\tRead {0}", family);
            }

            IQueryable<Family> AllQueryInSql = this.client.CreateDocumentQuery<Family>
                (
                   UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                   "SELECT * ",
                   queryOptions);

            Console.WriteLine("Running direct SQL query...");
            foreach (Family family in familyQueryInSql)
            {
                Console.WriteLine("\tRead {0}", family);
            }

            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
        public class Faculty
        {
            [JsonProperty(PropertyName = "id")]
            public string FacultyId { get; set; }
            public string FacultyName { get; set; }
            public Department[] Departments { get; set; }

            public Building Building { get; set; }
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        public class Department
        {
            public string DepartmentName { get; set; }
            public Building Building { get; set; }
            public Section[] Sections { get; set; }
        }

        public class Section
        {
            public string SectionName { get; set; }
            public Building Building { get; set; }
            public Teacher[] Teachers { get; set; }
        }

        public class Teacher
        {
            public string FamilyName { get; set; }
            public string FirstName { get; set; }
            public string Title { get; set; }
            public int OfficeNo { get; set; }
            public Course[] Courses { get; set; }
        }

        public class Course
        {
            public string CatalogName { get; set; }
            public int CourseNo { get; set; }
        }

        public class Building
        {
            public string BuildingName { get; set; }
            public string Street { get; set; }
            public string ZipCode { get; set; }
            public string City { get; set; }
        }

        private async Task CreateFacultyDocumentIfNotExists(string databaseName, string collectionName, Faculty faculty)
        {
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, faculty.FacultyId));
                this.WriteToConsoleAndPromptToContinue("Found {0}", faculty.FacultyId);
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    //await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), faculty);
                    await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), faculty, null, true);
                    this.WriteToConsoleAndPromptToContinue("Created Faculty {0}", faculty.FacultyId);
                }
                else
                {
                    throw;
                }
            }
        }
    }

}
