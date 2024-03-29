﻿using System;
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

            return allStates;
        }
            
        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).SingleOrDefault();

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
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).SingleOrDefault();
            }
            catch(InvalidOperationException e)
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
            if(updatedAddress == null)
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
        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            var cruddyEmployee = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).SingleOrDefault();

            switch (crudOperation)
            {
                case "create":
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
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
                    db.SubmitChanges();
                    break;
                default: break;
            }
        }

        // TODO: Animal CRUD Operations
        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Animal GetAnimalByID(int id)
        {
            return db.Animals.Where(a => a.AnimalId == id).SingleOrDefault();
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
                        animal.CategoryId = db.Categories.Where(c => c.Name == data).Select(c => c.CategoryId).SingleOrDefault();
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
            db.Adoptions.DeleteAllOnSubmit(db.Adoptions.Where(a => a.AnimalId == animal.AnimalId));
            db.Rooms.DeleteAllOnSubmit(db.Rooms.Where(r => r.AnimalId == animal.AnimalId));
            db.AnimalShots.DeleteAllOnSubmit(db.AnimalShots.Where(s => s.AnimalId == animal.AnimalId));
            db.SubmitChanges();
        }

        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates) // parameter(s)?
        {
            IQueryable<Animal> animals = db.Animals;
            foreach (var item in updates)
            {
                updates.TryGetValue(item.Key, out string data);
                switch (item.Key)
                {
                    case 1:
                        var foundCategoryId = db.Categories.Where(c => c.Name == data).Select(c => c.CategoryId).SingleOrDefault();
                        animals = animals.Where(a => a.CategoryId == foundCategoryId);
                        break;
                    case 2:
                        animals = animals.Where(a => a.Name == data);
                        break;
                    case 3:
                        animals = animals.Where(a => a.Age == int.Parse(data));
                        break;
                    case 4:
                        animals = animals.Where(a => a.Demeanor == data);
                        break;
                    case 5:
                        animals = animals.Where(a => a.KidFriendly == bool.Parse(data));
                        break;
                    case 6:
                        animals = animals.Where(a => a.PetFriendly == bool.Parse(data));
                        break;
                    case 7:
                        animals = animals.Where(a => a.Weight == int.Parse(data));
                        break;
                    case 8:
                        animals = animals.Where(a => a.AnimalId == int.Parse(data));
                        break;
                    default:
                        break;
                }
            }
            foreach (var item in animals)
            {
                Console.WriteLine($"{item.AnimalId}, {item.Name}");
            }
            return animals.AsQueryable();
        }

        // TODO: Misc Animal Things
        internal static int GetCategoryId(string animalCategoryName)
        {

            return db.Categories.Where(c => c.Name == animalCategoryName).Select(c => c.CategoryId).SingleOrDefault();

            //return db.something = categoryName
            //
        }

        internal static Room GetRoom(int animalId)
        {
            return db.Rooms.Where(r => r.AnimalId == animalId).SingleOrDefault();
        }

        internal static int GetDietPlanId(string dietPlanName)
        {
            return db.DietPlans.Where(d => d.Name == dietPlanName).Select(d => d.DietPlanId).SingleOrDefault();
        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            Adoption NewAdoption = new Adoption();
            NewAdoption.ApprovalStatus = "pending";
            NewAdoption.AdoptionFee = 75;
            NewAdoption.PaymentCollected = false;
            NewAdoption.ClientId = client.ClientId;
            NewAdoption.AnimalId = animal.AnimalId;
            //db.Animals.Where(a => NewAdoption.AnimalId == animal.AnimalId).SingleOrDefault().AdoptionStatus = "pending";
            animal.AdoptionStatus = "pending";
            db.Adoptions.InsertOnSubmit(NewAdoption);
            db.SubmitChanges();
        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
            return db.Adoptions.Where(a => a.ApprovalStatus == "pending");
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {
            db.Adoptions.Where(a => a.AnimalId == adoption.AnimalId && a.ClientId == adoption.ClientId).SingleOrDefault().ApprovalStatus = isAdopted ? "approved" : "rejected";
            db.Animals.Where(a => a.AnimalId == adoption.AnimalId).SingleOrDefault().AdoptionStatus = isAdopted ? "adopted" : "available";
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            var adoptionToRemove = db.Adoptions.Where(a => a.AnimalId == animalId && a.ClientId == clientId).SingleOrDefault();
            db.Adoptions.DeleteOnSubmit(adoptionToRemove);
            db.SubmitChanges();
        }

        // TODO: Shots Stuff
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            return db.AnimalShots.Where(a => a.AnimalId == animal.AnimalId);
        }
        internal static void UpdateShot(string shotName, Animal animal)
        {
            AnimalShot newAnimalShot = new AnimalShot();
            newAnimalShot.AnimalId = animal.AnimalId;
            newAnimalShot.ShotId = db.Shots.Where(s => s.Name == shotName).Select(s => s.ShotId).SingleOrDefault();
            db.AnimalShots.InsertOnSubmit(newAnimalShot);
            db.SubmitChanges();
        }
    }
}