namespace WalletApp.Data.Seed
{
    public interface IAsyncSeeder
    {
        Task SeedAsync(WalletDbContext dbContext);
    }
}
