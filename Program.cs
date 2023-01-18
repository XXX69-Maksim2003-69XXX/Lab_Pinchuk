using Microsoft.EntityFrameworkCore;
using PinchuckLab;

namespace BankDB
{
    /*  Використовуючи багатопоточний підхід реалізувати генерацію в циклі тестових екзе?мплярів сутностей з попередньої роботи
         (наприклад. Студент1,Студент2, ... СтудентN, Предмет1, Предмет2, .... ПредметN) і зберегти їх в БД. 
         Використати синхронізуючі примітиви для забезпечення унікальності згенерованих імен. 
         Реалізувати багатопоточне читання та відображення даних записаних в БД. 
         Завдання виконати двома способами - з використанням класу Thread та Task
              При роботі з EntityFramework та LINQ використовувати Async варіанти методів.
          */

    class Program
    {
        static async Task Main(string[] args)
        {
            // //вибираю всіх клієнтів з певними полями з БД і вивожу їх на екран 
            //  using (var db = new MailContext())
            //  {
            //    
            //      var result = db.Clients.Select(x => new
            //      {
            //          FullName = x.FirstName + " " + x.LastName,
            //          Passport = x.Passport,
            //      });
            //      foreach (var item in result)
            //      {
            //          Console.WriteLine("**********************************************");
            //          Console.WriteLine($"FullName: {item.FullName}");
            //          Console.WriteLine($": {item.Passport}");
            //          Console.WriteLine("**********************************************");
            //      }
            //  }
            //
            //
            // //приклади використання union, except, intersect, join, distinct, group by, агрегатних функцій.
            // using (var db = new MailContext())
            // {
            //     // //union
            //     // // ef core не може не об'єднати дві таблиці, тому 
            //     // // var result = db.Clients.Where(x => x.FirstName == "Ivan")
            //     // //     .Union(db.Employees.Where(x => x.FirstName == "Ivan").ToList();
            //     //
            //     // var result = db.Clients.Where(x=> x.FirstName == "Ivan")
            //     //     .Select(x=> new {FullName = x.FirstName + " " + x.LastName, Role = "Client"})
            //     //     .Union(db.Employees.Where(x=> x.FirstName == "Ivan")
            //     //         .Select(x=> new {FullName = x.FirstName + " "+ x.LastName, Role = "Employee"})).ToList();
            //     //
            //     // foreach (var person in result )
            //     // {
            //     //     Console.WriteLine("**********************************************");
            //     //     Console.WriteLine($"FullName: {person.FullName}");
            //     //     Console.WriteLine($"Role: {person.Role}");
            //     //     Console.WriteLine("**********************************************");
            //     // }
            //
            //
            //     // //except
            //     // //виберу всіх клієнтів, прізвища котрих, закінчуються на "ko"
            //     // // проте виключу з них тих, хто має +380 в номері телефону
            //     // var result = db.Clients.Where(x => x.LastName.EndsWith("ko"));
            //     // var result2 = db.Clients.Where(x=> x.PhoneNumber.Contains("380"));
            //     // var result3 = result.Except(result2).ToList();
            //     // foreach (var person in result3)
            //     // {
            //     //     Console.WriteLine("**********************************************");
            //     //     Console.WriteLine($"FullName: {person.FirstName} {person.LastName}");
            //     //     Console.WriteLine($"PhoneNumber: {person.PhoneNumber}");
            //     //     Console.WriteLine("**********************************************");
            //     // }
            //
            //
            //     // // //intersect
            //     // // //виберу всіх клієнтів, прізвища котрих, закінчуються на "ko",
            //     // // та номера є українськими
            //     //  var result = db.Clients.Where(x => x.LastName.EndsWith("ko"))
            //     //      .Intersect(db.Clients.Where(x => x.PhoneNumber.Contains("380")))
            //     //      .ToList();
            //     //  foreach (var person in result)
            //     // {
            //     //     Console.WriteLine("**********************************************");
            //     //        Console.WriteLine($"FullName: {person.FirstName} {person.LastName}");
            //     //        Console.WriteLine($"PhoneNumber: {person.PhoneNumber}");
            //     //        Console.WriteLine("**********************************************");
            //     // }
            //
            //
            //     // // join отримую усі відправлені посилки для певного клієнта
            //     // var result = db.Parcels.Join(db.Clients, // другий набір даних
            //     //     x => x.SenderId, // властивість, за якою вибираємо у першому наборі даних
            //     //     u => u.Id, // властивість, за якою вибираємо у першому наборі даних
            //     //     (x, u) => new
            //     //     {
            //     //         Name = u.FirstName + u.LastName,
            //     //         PriceOfParcel = x.Price,
            //     //         WeightOfParcel = x.Weight,
            //     //     }
            //     // );
            //     // foreach (var clientForParcel in result)
            //     // {
            //     //     Console.WriteLine("**********************************************");
            //     //        Console.WriteLine($"FullName: {clientForParcel.Name}");
            //     //        Console.WriteLine($"PriceOfParcel: {clientForParcel.PriceOfParcel}");
            //     //        Console.WriteLine($"WeightOfParcel: {clientForParcel.WeightOfParcel}");
            //     //        Console.WriteLine("**********************************************");
            //     // }
            //
            //
            //     // //distinct uniq values
            //     // // дістану всі унікальні серії паспортів:
            //     // var result = db.Clients.Select(x => x.Passport.Substring(0,2))
            //     //     .Distinct()
            //     //     .ToList();
            //     //
            //     // foreach (var forPassport in result)
            //     // {
            //     //     Console.WriteLine("**********************************************");
            //     //     Console.WriteLine($"Citizenship: {forPassport}");
            //     //     Console.WriteLine("**********************************************");
            //     // }
            //
            //
            //     // //group by та Sum
            //     // // згрупую для кожного клієнта, всі його посилки які він надіслав
            //     // var result = db.Parcels
            //     //     .GroupBy(x => x.SenderId)
            //     //     .Select(x => new
            //     //     {
            //     //         SenderId = x.Key,
            //     //         CountOfParcels = x.Count(),
            //     //         SumOfParcels = x.Sum(y => y.Price)
            //     //     }).ToList();
            //     //  foreach (var account in result)
            //     // {
            //     //     Console.WriteLine("**********************************************");
            //     //     Console.WriteLine($"SenderId: {account.SenderId}");
            //     //     Console.WriteLine($"Count: {account.CountOfParcels}");
            //     //     Console.WriteLine($"Sum: {account.SumOfParcels}");
            //     // }
            // }
            //
            //
            // // //Навести приклади різних стратегій завантаження зв'язаних даних (Eager, Explicit, Lazy)
            // // //Eager
            // // using (var db = new MailContext())
            // // {
            // //     // явно вказую, що хочу завантажити дані з таблиці Clients
            // //     var result = db.Clients.Include(x => x.Parcels)
            // //         .ToList();
            // //     foreach (var client in result)
            // //     {
            // //         Console.WriteLine($"FullName: {client.FirstName} {client.LastName}");
            // //         Console.WriteLine($"PhoneNumber: {client.PhoneNumber}");
            // //         Console.WriteLine($"Accounts: ");
            // //         
            // //         // виведу всі посилки для клієнта
            // //         foreach (var account in client.Parcels)
            // //         {
            // //             Console.WriteLine($"Price: {account.Price}");
            // //             Console.WriteLine($"Weight: {account.Weight}");
            // //         }
            // //         Console.WriteLine("**********************************************");
            // //     }
            // // }
            //
            //
            // // //Explicit
            // // using (var db = new MailContext())
            // // {
            // //     //пошук по id
            // //     var client = db.Clients.Find(1) 
            // //                  ?? throw new Exception("Client not found");
            // //     // явно вказую, що хочу завантажити дані з таблиці Accounts
            // //     db.Entry(client).Collection(x => x.Parcels).Load();
            // //     Console.WriteLine("**********************************************");
            // //     Console.WriteLine($"FullName: {client.FirstName} {client.LastName}");
            // //     Console.WriteLine($"PhoneNumber: {client.PhoneNumber}");
            // //     Console.WriteLine($"Parcels: ");
            // //
            // //     // виведу всі посилки для клієнта
            // //     foreach (var account in client.Parcels)
            // //     {
            // //         Console.WriteLine($"Price: {account.Price}");
            // //         Console.WriteLine($"Weight: {account.Weight}");
            // //     }
            // //
            // //     Console.WriteLine("**********************************************");
            // // }
            //
            // //Lazy
            // // Для цього потрібно у властивості вказати virtual та використати FrameworkCore.Proxies
            // using (var db = new MailContext())
            // {
            //     //пошук по id
            //     var client = db.Clients.Find(1)
            //                  ?? throw new Exception("Client not found");
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
            //
            //
            // // // Навести приклад завантаження даних що не відслідковуються, їх зміни та збереження
            // // using (var db = new MailContext())
            // // {
            // //     db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; // відключаємо відслідковування для всіх запитів
            // //     Console.WriteLine(db.ChangeTracker.Entries().Count()); // 0
            // //     var client = db.Clients.FirstOrDefault(x => x.Id==4);
            // //     Console.WriteLine($"{client.FirstName} {client.LastName}");
            // //     client.FirstName = "Vasya";
            // //     client.LastName = "Ivanov";
            // //     db.SaveChanges();
            // // }
            // //
            // // using (var db = new MailContext())
            // // {
            // //     var client = db.Clients.Where(x=> x.Id==4).AsNoTracking().FirstOrDefault();
            // //     Console.WriteLine($"{client.FirstName} {client.LastName}");
            // //     db.SaveChanges();
            // // }
            //
            //
            //
            //
            // // //Навести приклади виклику збережених процедур та функцій за допомогою Entity Framework
            // // using (var db = new MailContext())
            // // {
            // //     var blogs = db.Clients.FromSqlRaw("select * from select_my_table(2);").ToList();
            // //     blogs.ForEach(x => Console.WriteLine($"ID:{x.PersonId}  {x.FirstName} {x.LastName}"));
            // // }
            
            
            // // // lock
            // // // використовуючи lock, забезпечити доступ до ресурсу тільки одному потоку
            // // і виведу на екран поток, який отримав доступ
            // var locker = new object();
            // int clientIndex = 0;
            // var clientThread = new ThreadStart(() =>
            // {
            //     Console.WriteLine($"Client thread started{Thread.CurrentThread.ManagedThreadId}");
            //     using (var context = new MailContext())
            //     {
            //         for (int i = 0; i < 10; i++)
            //         {
            //           // явно блокую доступ до ресурсу
            //             lock (locker)
            //             {
            //                 clientIndex++;
            //                 context.Clients.Add(new Client
            //                 {
            //                     FirstName = $"client:FirstName{clientIndex}",
            //                     LastName = $"client:LastName{clientIndex}",
            //                 });
            //             }
            //         }
            //         context.SaveChanges();
            //     }
            // });
            //
            // // виконаю цей код в 4 окремих потоках
            // var thread1 = new Thread(clientThread);
            // var thread2 = new Thread(clientThread);
            // var thread3 = new Thread(clientThread);
            // var thread4 = new Thread(clientThread);
            //
            // thread1.Start();
            // thread2.Start();
            // thread3.Start();
            // thread4.Start();
            //
            //
            // // очікую завершення роботи потоків
            // var allThreadsAreDone = false;
            // while (!allThreadsAreDone) {
            //     allThreadsAreDone = thread1.ThreadState == System.Threading.ThreadState.Stopped &&
            //                         thread2.ThreadState == System.Threading.ThreadState.Stopped &&
            //                         thread3.ThreadState == System.Threading.ThreadState.Stopped &&
            //                         thread4.ThreadState == System.Threading.ThreadState.Stopped;
            // }
            // Console.WriteLine(clientIndex);

            
            // // паралельно зчитаю з БД
            // // використовуючи Thread виведу на екран паралельно клієнтів з двух вибірок
            //  var db = new MailContext();
            //  var clients = await db.Clients.ToListAsync();
            //  
            //  // перший делегат, перша половина клієнтів
            //  ThreadStart action = () =>
            //  {
            //      for (int i = 0; i < (clients.Count-1)/2; i++)
            //      {
            //          Console.WriteLine(clients[i].FirstName + " " + clients[i].LastName +" " + Thread.CurrentThread.ManagedThreadId);
            //          Thread.Sleep(500);
            //      }
            //  };
            //  
            //  // другий делегат, перша половина клієнтів
            //  ThreadStart action2 = () =>
            //  {
            //      for (int i = (clients.Count-1)/2; i < clients.Count-1; i++)
            //      {
            //          Console.WriteLine(clients[i].FirstName + " " + clients[i].LastName +" " + Thread.CurrentThread.ManagedThreadId);
            //          Thread.Sleep(500);
            //      }
            //  };
            //
            //  // виконаю цей код в 2 окремих потоках
            //  var thread1 = new Thread(action);
            //  var thread2 = new Thread(action2);
            //
            //  
            //  thread1.Start();
            //  thread2.Start();
            //
            //  await db.DisposeAsync();
            
           // використовуючи Task виведу на екран паралельно клієнтів з двух вибірок
           // за аналогією з попереднім завданням
             var db = new MailContext();
             var clients = await db.Clients.ToListAsync();
             
             // Task.Run виконує код в окремому потоці, тому у консолі буде виводитися код з двух потоків *паралельно
             var task1 = Task.Run(()=> 
             {
                 for (int i = 0; i < (clients.Count-1)/2; i++)
                 {
                     Console.WriteLine(clients[i].FirstName + " " + clients[i].LastName + " " + Thread.CurrentThread.ManagedThreadId);
                     Task.Delay(500).Wait();
                 }
             });
             
             var task2 = Task.Run(()=> 
             {
                 for (int i = (clients.Count-1)/2; i < clients.Count-1; i++)
                 {
                     Console.WriteLine(clients[i].FirstName + " " + clients[i].LastName + " " + Thread.CurrentThread.ManagedThreadId);
                     Task.Delay(500).Wait();
                 }
             });
             
             // очікую завершення виконання всіх задач
             await Task.WhenAll(task1, task2);
             
             // звільняю ресурси
             await db.DisposeAsync();
            
        }
    }
}