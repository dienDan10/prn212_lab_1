using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iaccountRepository;

        public AccountService()
        {
            iaccountRepository = new AccountRepository();
        }

        public AccountMember GetAccountById(string id)
        {
            return iaccountRepository.GetAccountById(id);
        }
    }
}
