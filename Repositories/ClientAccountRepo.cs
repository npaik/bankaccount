using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.ViewModels;
using static WebApplication1.Data.ApplicationDbContext;

namespace WebApplication1.Repositories
{
    public class ClientAccountRepo : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientAccountRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ClientAccountVM> GetClientAccountByEmail(string email)
        {
            var clientAccount = await _context.ClientAccounts
                .Where(u => u.Client.Email == email)
                .Select(u => new ClientAccountVM()
                {
                    ClientId = u.Client.ClientId,
                    Email = u.Client.Email,
                    FirstName = u.Client.FirstName,
                    LastName = u.Client.LastName,
                    AccountNum = u.AccountNum,
                    AccountType = u.BankAccount.AccountType,
                    Balance = (decimal)u.BankAccount.Balance
                })
                .FirstOrDefaultAsync();

            return clientAccount;
        }

        public async Task<ClientAccountVM> GetClientAccountById(int clientId)
        {
            var clientAccount = await _context.ClientAccounts
                .Where(u => u.Client.ClientId == clientId)
                .Select(u => new ClientAccountVM()
                {
                    ClientId = u.Client.ClientId,
                    Email = u.Client.Email,
                    FirstName = u.Client.FirstName,
                    LastName = u.Client.LastName,
                    AccountNum = u.AccountNum,
                    AccountType = u.BankAccount.AccountType,
                    Balance = (decimal)u.BankAccount.Balance
                })
                .FirstOrDefaultAsync();

            return clientAccount;
        }

        public async Task<List<ClientAccountVM>> filterUser(string filter)
        {
            var clients = await _context.ClientAccounts
                .Where(u => u.BankAccount.AccountType == filter)
                .Select(u => new ClientAccountVM()
                {
                    ClientId = u.Client.ClientId,
                    FirstName = u.Client.FirstName,
                    LastName = u.Client.LastName,
                    AccountNum = u.AccountNum,
                    AccountType = u.BankAccount.AccountType,
                //    Balance = u.BankAccount.Balance.HasValue ? (decimal)u.BankAccount.Balance.Value : 0m
                })
                .ToListAsync();

            return clients;
        }

        public async Task<List<ClientAccountVM>> All()
        {
            return await _context.ClientAccounts
                .OrderBy(u => u.Client.LastName)
                .Select(u => new ClientAccountVM()
                {
                    ClientId = u.Client.ClientId,
                    Balance = (decimal)u.BankAccount.Balance,
                    FirstName = u.Client.FirstName,
                    LastName = u.Client.LastName,
                    AccountNum = u.AccountNum,
                    AccountType = u.BankAccount.AccountType
                })
                .ToListAsync();
        }

        public Task<bool> UpdateClientAccount(ClientAccountVM clientAccountVM)
        {
            return UpdateClientAccount(clientAccountVM, (decimal)clientAccountVM.Balance);
        }

        public async Task<bool> UpdateClientAccount(ClientAccountVM clientAccountVM, decimal balance)
        {
            try
            {
                var clientAccount = await _context.ClientAccounts
                    .Include(ca => ca.Client)
                    .Include(ca => ca.BankAccount)
                    .FirstOrDefaultAsync(ca => ca.Client.ClientId == clientAccountVM.ClientId);

                if (clientAccount != null)
                {
                    clientAccount.Client.FirstName = clientAccountVM.FirstName;
                    clientAccount.Client.LastName = clientAccountVM.LastName;
                    clientAccount.BankAccount.Balance = balance;

                    _context.Entry(clientAccount.Client).State = EntityState.Modified;
                    _context.Entry(clientAccount.BankAccount).State = EntityState.Modified;
                    _context.Entry(clientAccount).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

    }
}