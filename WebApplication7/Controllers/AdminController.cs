using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Controllers
{
    public class AdminController : Controller
    {
        #region fullfill database functions
        public void FillCities()
        {
            List<City> cities = new List<City>();
            City city = new City("Москва");
            cities.Add(city);
            City city1 = new City("Санкт-Петербург");
            cities.Add(city1);
            City city2 = new City("Новосибирск");
            cities.Add(city2);
            City city3 = new City("Екатеринбург");
            cities.Add(city3);
            City city4 = new City("Нижний Новгород");
            cities.Add(city4);
            City city5 = new City("Челябинск");
            cities.Add(city5);
            City city6 = new City("Омск");
            cities.Add(city6);
            City city7 = new City("Ростов-на-Дону");
            cities.Add(city7);
            City city8 = new City("Уфа");
            cities.Add(city8);
            City city9 = new City("Красноярск");
            cities.Add(city9);
            foreach (var item in cities)
            {
                database.cities.Add(item);

            }
            database.SaveChanges();
        }

        public void FillClients(DbSet<City> cities)
        {
            List<City> ClientCities = new List<City>();
            for (int i = 0; i < 20; i++)
            {
                Client client = new Client();
                client.Name = $"Client_withName {i}";
                client.Surname = $"Client_withSurname {i * i}";
                client.Money = rnd.Next(10000, 1000000);
                foreach (var item in cities)
                {
                    ClientCities.Add(item);
                }
                client.City = ClientCities[rnd.Next(0, ClientCities.Count() - 1)].Name;
                string personalCode = Guid.NewGuid().ToString().Substring(0, 13).Remove(8, 1);
                client.OwnerCode = personalCode;
                database.Add(client);



            }
            database.SaveChanges();
        }

        public void FillAdmins()
        {
            for (int i = 0; i < 8; i++)
            {
                Admin admin = new Admin();
                admin.Name = $"Admin's name {i}";
                admin.Surname = $"Admin's surname {i * i}";
                admin.Salary = rnd.Next(100000, 2000000);
                database.Admins.Add(admin);
            }
            database.SaveChanges();
        }

        public void FillBancomats(DbSet<City> cities)
        {
            List<City> cities1 = new List<City>();
            foreach (var item in cities)
            {
                cities1.Add(item);
            }
            for (int i = 0; i < 200; i++)
            {
                Bankomat bankomat = new Bankomat();
                bankomat.City = cities1[rnd.Next(0, cities1.Count())].Name;
                bankomat.CurrentMoney = rnd.Next(50000, 10000000);
                bankomat.Working = (rnd.Next(0,2) == 1);
                database.Bankomats.Add(bankomat);

                
            }
            database.SaveChanges();

        }


        public void FillCards(DbSet<Client> clients)
        {
            
          
            List<Card> cards = new List<Card>();
            foreach (var item in clients)
            {
                Card card = new Card();
                
                card.OwnerCode = item.OwnerCode;
                card.CardNumber = rnd.Next(100000000, 999999999);
                database.Cards.Add(card);
            }
            database.SaveChanges();
            

        }
        #endregion

        DB database = new DB();
        Random rnd = new Random();
        static Client client;
        public IActionResult ShowClients()
        {
            
            return View();
        }

       
        public IActionResult FullBase()
        {
            List < Client > clients = new List<Client>();
            #region fullFilldatabases
            if (database.cities.Count() == 0)
            {
                FillCities();
            }
            if (database.Clients.Count()==0)
            {
                FillClients(database.cities);
            }
            if (database.Admins.Count()==0)
            {
                FillAdmins();
            }
            if (database.Bankomats.Count() == 0)
            {
                FillBancomats(database.cities);
            }
            if (database.Cards.Count()==0)
            {
                FillCards(database.Clients);
            }

            #endregion
            foreach (var item in database.Clients)
            {
                clients.Add(item);
            }

            return View(clients);
        }

        
        public IActionResult AddClientForm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddClient(string Name, string Surname, int Money, string City)
        {
            Client client = new Client()
            {
                Name = Name,
                Surname = Surname,
                City = City,
                Money = Money,
                OwnerCode = Guid.NewGuid().ToString().Substring(0, 13).Remove(8, 1),
            };

            database.Clients.Add(client);
            database.SaveChanges();
            return Redirect("~/Admin/FullBase");
        }

        
        public IActionResult Delete(string OwnerCode)
        {
            var clients = database.Clients.Where(e => e.OwnerCode == OwnerCode).ToList(); 
            database.Clients.Remove(clients[0]);
            database.SaveChanges();

            return Redirect("~/Admin/FullBase");
        }
        [HttpGet]
        public IActionResult ClientInformation(string OwnerCode)
        {
            var clients = database.Clients.Where(e => e.OwnerCode == OwnerCode).ToList();
            return View(clients[0]);
        }
        public IActionResult SaveChanges(string Name, string Surname, string City, string OwnerCode)
        {
            foreach (var item in database.Clients)
            {
                if (item.OwnerCode == OwnerCode)
                {
                    item.Name = Name;
                    item.Surname = Surname;
                    item.City = City;
                }
            }
            database.SaveChanges();

            return Redirect("~/Admin/FullBase");
        }
        public IActionResult Send( string OwnerCode)
        {
            client = database.Clients.Where(e => e.OwnerCode == OwnerCode).First();
            return View(client);
        }

        public IActionResult SendMoney(string money, string OwnerCode)
        {
            database.Clients.Where(e => e.OwnerCode == OwnerCode).First().Money += Convert.ToInt32(money);
            database.Clients.Find(client.Id).Money -= Convert.ToInt32(money);
            database.SaveChanges();
            return Redirect("~/Admin/FullBase");
        }
    }
}