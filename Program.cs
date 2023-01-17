using Microsoft.EntityFrameworkCore;
using PinchuckLab;

namespace BankDB
{

    class Program
    {
        
        static async Task Main(string[] args)
        {
            LinqExample();
        }

        // За допомогою Linq, вивожду усі MailBranch
        // просортувавши їх за сумарнною вагою відправлень
        static void LinqExample()
        {
            var db = new MailContext();
            var mailBranches = db.MailBranches
                .Include(mb => mb.Parcels)
                .OrderBy(mb => mb.Parcels.Sum(p => p.Weight))
                .ToList();

            mailBranches.ForEach(mb =>
            {
                Console.WriteLine(mb.BranchName + " " + mb.Parcels.Sum(p => p.Weight));
            });

            db.Dispose();
        }
    }
}