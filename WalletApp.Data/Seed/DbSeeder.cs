using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace WalletApp.Data.Seed
{
    public class DbSeeder : IAsyncSeeder
    {
        public async Task SeedAsync(WalletDbContext dbContext)
        {
            // Check if the database has been seeded already
            if (dbContext.Accounts.Any() || dbContext.RechargeOperations.Any())
            {
                return;
            }

            // Seed accounts
            var accounts = new List<Account>
            {
                new Account { AccountNumber = "123456",  IsIdentified = true, Balance = 5000 },
                new Account { AccountNumber = "789012", IsIdentified = false, Balance = 3000 },
                // Add more accounts as needed
            };

            await dbContext.Accounts.AddRangeAsync(accounts);
            await dbContext.SaveChangesAsync();

            // Seed recharge operations
            var rechargeOperations = new List<RechargeOperation>
            {
                new RechargeOperation { AccountId = accounts[0].Id, Amount = 1000, Date = DateTime.Now },
                new RechargeOperation { AccountId = accounts[0].Id, Amount = 2000, Date = DateTime.Now },
                // Add more recharge operations as needed
            };

            await dbContext.RechargeOperations.AddRangeAsync(rechargeOperations);
            await dbContext.SaveChangesAsync();
        }
    }
}
