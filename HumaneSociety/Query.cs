using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {
        static HumaneSocietyDataContext db;

        static Query()
        {
            db = new HumaneSocietyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();
            ///Like a good neighbor, allStates is there...
            return allStates;
        }

        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            // find corresponding Client from Db
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }

            // update clientFromDb information with the values on clientWithUpdates (aside from address)
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;

            // get address object from clientWithUpdates
            Address clientAddress = clientWithUpdates.Address;

            // look for existing Address in Db (null will be returned if the address isn't already in the Db
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            // if the address isn't found in the Db, create and insert it
            if (updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            // attach AddressId to clientFromDb.AddressId
            clientFromDb.AddressId = updatedAddress.AddressId;

            // submit changes
            db.SubmitChanges();
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName == null;
        }


        //// TODO Items: ////

        // TODO: Allow any of the CRUD operations to occur here
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="crudOperation"></param>
        /// Create(Insert) , Read (Select), Update(Update), Delete(Delete)
        /// AND & OR ops will help with this
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            var cruddyEmployee = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).Single();

            switch (crudOperation)
            {
                case "create":
                    db.Employees.InsertOnSubmit(employee);
                    break;
                case "read":
                    Console.WriteLine(cruddyEmployee.FirstName);
                    Console.WriteLine(cruddyEmployee.LastName);
                    Console.WriteLine(cruddyEmployee.EmployeeNumber);
                    Console.WriteLine(cruddyEmployee.Email);


                    break;

                case "update":
                    cruddyEmployee.FirstName = employee.FirstName;
                    cruddyEmployee.LastName = employee.LastName;
                    cruddyEmployee.EmployeeNumber = employee.EmployeeNumber;
                    cruddyEmployee.Email = employee.Email;

                    break;
                case "delete":
                    db.Employees.DeleteOnSubmit(cruddyEmployee);
                    break;
                default: break;
            }
        }
        //clientFromDb.FirstName = clientWithUpdates.FirstName;
        //    clientFromDb.LastName = clientWithUpdates.LastName;

        // TODO: Animal CRUD Operations
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
        }

        internal static Animal GetAnimalByID(int id)
        {
            return db.Animals.Where(a => a.AnimalId == id).Single();
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {
            var animal = GetAnimalByID(animalId);
            foreach (var item in updates)
            {
                updates.TryGetValue(item.Key, out string data);
                switch (item.Key)
                {
                    case 1:
                        animal.CategoryId = db.Categories.Where(c => c.Name == data).Select(c => c.CategoryId).Single();
                        break;
                    case 2:
                        animal.Name = data;
                        break;
                    case 3:
                        animal.Age = int.Parse(data);
                        break;
                    case 4:
                        animal.Demeanor = data;
                        break;
                    case 5:
                        animal.KidFriendly = bool.Parse(data);
                        break;
                    case 6:
                        animal.PetFriendly = bool.Parse(data);
                        break;
                    case 7:
                        animal.Weight = int.Parse(data);
                        break;
                    case 8:
                        animal.AnimalId = int.Parse(data);
                        break;
                    default:
                        break;
                }
            }
        }

        internal static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
        }

        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {
            var animals = new List<Animal>().AsQueryable();
            foreach (var item in updates)
            {
                updates.TryGetValue(item.Key, out string data);
                switch (item.Key)
                {
                    case 1:
                        var foundCategoryId = db.Categories.Where(c => c.Name == data).Select(c => c.CategoryId).Single();
                        animals = db.Animals.Where(a => a.CategoryId == foundCategoryId);
                        break;
                    case 2:
                        animals = db.Animals.Where(a => a.Name == data);
                        break;
                    case 3:
                        animals = db.Animals.Where(a => a.Age == int.Parse(data));
                        break;
                    case 4:
                        animals = db.Animals.Where(a => a.Demeanor == data);
                        break;
                    case 5:
                        animals = db.Animals.Where(a => a.KidFriendly == bool.Parse(data));
                        break;
                    case 6:
                        animals = db.Animals.Where(a => a.PetFriendly == bool.Parse(data));
                        break;
                    case 7:
                        animals = db.Animals.Where(a => a.Weight == int.Parse(data));
                        break;
                    case 8:
                        animals = db.Animals.Where(a => a.AnimalId == int.Parse(data));
                        break;
                    default:
                        break;
                }
            }
            return animals;
        }

        // TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {
            throw new NotImplementedException();
        }

        internal static Room GetRoom(int animalId)
        {
            throw new NotImplementedException();
        }

        internal static int GetDietPlanId(string dietPlanName)
        {
            throw new NotImplementedException();
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            throw new NotImplementedException();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            throw new NotImplementedException();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            throw new NotImplementedException();
        }
    }
}