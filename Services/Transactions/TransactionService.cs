using GameStoresBlazor.Models.User;
using GameStoresBlazor.Services.DataBase;
using Microsoft.EntityFrameworkCore;

namespace GameStoresBlazor.Services.Transactions
{
    public class TransactionService
    {
        private readonly DataBaseContext _dataBaseContext;

        public TransactionService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        

        public async Task<decimal> GetBalance(SteamIdentityUserModel steamIdentityUserModel)
        {
            var transactions = await _dataBaseContext.Transactions.AsNoTracking().Where(s => s.UserId == steamIdentityUserModel.Id).SumAsync(s => s.Payment);

            return transactions;
        }
    }
}
