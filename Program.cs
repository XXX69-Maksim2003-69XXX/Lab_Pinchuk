using System.Xml.XPath;
using PinchuckLab.Models;
using Microsoft.EntityFrameworkCore;
using PinchuckLab;

namespace BankDB
{
    ///union, except, intersect, join, distinct, group by, агрегатних функцій.
    class Program
    {
        static void Main(string[] args)
        {
            //вибираю всіх клієнтів з певними полями з БД і вивожу їх на екран 
             using (var db = new MailContext())
             {
               
                 var result = db.Clients.Select(x => new
                 {
                     FullName = x.FirstName + " " + x.LastName,
                     Passport = x.Passport,
                 });
                 foreach (var item in result)
                 {
                     Console.WriteLine("**********************************************");
                     Console.WriteLine($"FullName: {item.FullName}");
                     Console.WriteLine($": {item.Passport}");
                     Console.WriteLine("**********************************************");
                 }
             }


            //приклади використання union, except, intersect, join, distinct, group by, агрегатних функцій.
            using (var db = new MailContext())
            {
                // //union
                // // ef core не може не об'єднати дві таблиці, тому 
                // // var result = db.Clients.Where(x => x.FirstName == "Ivan")
                // //     .Union(db.Employees.Where(x => x.FirstName == "Ivan").ToList();
                //
                // var result = db.Clients.Where(x=> x.FirstName == "Ivan")
                //     .Select(x=> new {FullName = x.FirstName + " " + x.LastName, Role = "Client"})
                //     .Union(db.Employees.Where(x=> x.FirstName == "Ivan")
                //         .Select(x=> new {FullName = x.FirstName + " "+ x.LastName, Role = "Employee"})).ToList();
                //
                // foreach (var person in result )
                // {
                //     Console.WriteLine("**********************************************");
                //     Console.WriteLine($"FullName: {person.FullName}");
                //     Console.WriteLine($"Role: {person.Role}");
                //     Console.WriteLine("**********************************************");
                // }


                // //except
                // //виберу всіх клієнтів, прізвища котрих, закінчуються на "ko"
                // // проте виключу з них тих, хто має +380 в номері телефону
                // var result = db.Clients.Where(x => x.LastName.EndsWith("ko"));
                // var result2 = db.Clients.Where(x=> x.PhoneNumber.Contains("380"));
                // var result3 = result.Except(result2).ToList();
                // foreach (var person in result3)
                // {
                //     Console.WriteLine("**********************************************");
                //     Console.WriteLine($"FullName: {person.FirstName} {person.LastName}");
                //     Console.WriteLine($"PhoneNumber: {person.PhoneNumber}");
                //     Console.WriteLine("**********************************************");
                // }


                // // //intersect
                // // //виберу всіх клієнтів, прізвища котрих, закінчуються на "ko",
                // // та номера є українськими
                //  var result = db.Clients.Where(x => x.LastName.EndsWith("ko"))
                //      .Intersect(db.Clients.Where(x => x.PhoneNumber.Contains("380")))
                //      .ToList();
                //  foreach (var person in result)
                // {
                //     Console.WriteLine("**********************************************");
                //        Console.WriteLine($"FullName: {person.FirstName} {person.LastName}");
                //        Console.WriteLine($"PhoneNumber: {person.PhoneNumber}");
                //        Console.WriteLine("**********************************************");
                // }


                // // join отримую усі відправлені посилки для певного клієнта
                // var result = db.Parcels.Join(db.Clients, // другий набір даних
                //     x => x.SenderId, // властивість, за якою вибираємо у першому наборі даних
                //     u => u.Id, // властивість, за якою вибираємо у першому наборі даних
                //     (x, u) => new
                //     {
                //         Name = u.FirstName + u.LastName,
                //         PriceOfParcel = x.Price,
                //         WeightOfParcel = x.Weight,
                //     }
                // );
                // foreach (var clientForParcel in result)
                // {
                //     Console.WriteLine("**********************************************");
                //        Console.WriteLine($"FullName: {clientForParcel.Name}");
                //        Console.WriteLine($"PriceOfParcel: {clientForParcel.PriceOfParcel}");
                //        Console.WriteLine($"WeightOfParcel: {clientForParcel.WeightOfParcel}");
                //        Console.WriteLine("**********************************************");
                // }


                // //distinct uniq values
                // // дістану всі унікальні серії паспортів:
                // var result = db.Clients.Select(x => x.Passport.Substring(0,2))
                //     .Distinct()
                //     .ToList();
                //
                // foreach (var forPassport in result)
                // {
                //     Console.WriteLine("**********************************************");
                //     Console.WriteLine($"Citizenship: {forPassport}");
                //     Console.WriteLine("**********************************************");
                // }


                // //group by та Sum
                // // згрупую для кожного клієнта, всі його посилки які він надіслав
                // var result = db.Parcels
                //     .GroupBy(x => x.SenderId)
                //     .Select(x => new
                //     {
                //         SenderId = x.Key,
                //         CountOfParcels = x.Count(),
                //         SumOfParcels = x.Sum(y => y.Price)
                //     }).ToList();
                //  foreach (var account in result)
                // {
                //     Console.WriteLine("**********************************************");
                //     Console.WriteLine($"SenderId: {account.SenderId}");
                //     Console.WriteLine($"Count: {account.CountOfParcels}");
                //     Console.WriteLine($"Sum: {account.SumOfParcels}");
                // }
            }


            // //Навести приклади різних стратегій завантаження зв'язаних даних (Eager, Explicit, Lazy)
            // //Eager
            // using (var db = new MailContext())
            // {
            //     // явно вказую, що хочу завантажити дані з таблиці Clients
            //     var result = db.Clients.Include(x => x.Parcels)
            //         .ToList();
            //     foreach (var client in result)
            //     {
            //         Console.WriteLine($"FullName: {client.FirstName} {client.LastName}");
            //         Console.WriteLine($"PhoneNumber: {client.PhoneNumber}");
            //         Console.WriteLine($"Accounts: ");
            //         
            //         // виведу всі посилки для клієнта
            //         foreach (var account in client.Parcels)
            //         {
            //             Console.WriteLine($"Price: {account.Price}");
            //             Console.WriteLine($"Weight: {account.Weight}");
            //         }
            //         Console.WriteLine("**********************************************");
            //     }
            // }


            // //Explicit
            // using (var db = new MailContext())
            // {
            //     //пошук по id
            //     var client = db.Clients.Find(1) 
            //                  ?? throw new Exception("Client not found");
            //     // явно вказую, що хочу завантажити дані з таблиці Accounts
            //     db.Entry(client).Collection(x => x.Parcels).Load();
            //     Console.WriteLine("**********************************************");
            //     Console.WriteLine($"FullName: {client.FirstName} {client.LastName}");
            //     Console.WriteLine($"PhoneNumber: {client.PhoneNumber}");
            //     Console.WriteLine($"Parcels: ");
            //
            //     // виведу всі посилки для клієнта
            //     foreach (var account in client.Parcels)
            //     {
            //         Console.WriteLine($"Price: {account.Price}");
            //         Console.WriteLine($"Weight: {account.Weight}");
            //     }
            //
            //     Console.WriteLine("**********************************************");
            // }

            //Lazy
            // Для цього потрібно у властивості вказати virtual та використати FrameworkCore.Proxies
            using (var db = new MailContext())
            {
                //пошук по id
                var client = db.Clients.Find(1)
                             ?? throw new Exception("Client not found");
                Console.WriteLine("**********************************************");
                Console.WriteLine($"FullName: {client.FirstName} {client.LastName}");
                Console.WriteLine($"PhoneNumber: {client.PhoneNumber}");
                Console.WriteLine($"Parcels: ");
            
                // виведу всі посилки для клієнта
                foreach (var account in client.Parcels)
                {
                    Console.WriteLine($"Price: {account.Price}");
                    Console.WriteLine($"Weight: {account.Weight}");
                }
            
                Console.WriteLine("**********************************************");
            }
            
            
            // // Навести приклад завантаження даних що не відслідковуються, їх зміни та збереження
            // using (var db = new MailContext())
            // {
            //     db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; // відключаємо відслідковування для всіх запитів
            //     Console.WriteLine(db.ChangeTracker.Entries().Count()); // 0
            //     var client = db.Clients.FirstOrDefault(x => x.Id==4);
            //     Console.WriteLine($"{client.FirstName} {client.LastName}");
            //     client.FirstName = "Vasya";
            //     client.LastName = "Ivanov";
            //     db.SaveChanges();
            // }
            //
            // using (var db = new MailContext())
            // {
            //     var client = db.Clients.Where(x=> x.Id==4).AsNoTracking().FirstOrDefault();
            //     Console.WriteLine($"{client.FirstName} {client.LastName}");
            //     db.SaveChanges();
            // }
            
            


            // //Навести приклади виклику збережених процедур та функцій за допомогою Entity Framework
            // using (var db = new MailContext())
            // {
            //     var blogs = db.Clients.FromSqlRaw("select * from select_my_table(2);").ToList();
            //     blogs.ForEach(x => Console.WriteLine($"ID:{x.PersonId}  {x.FirstName} {x.LastName}"));
            // }
            
        }
    }
}