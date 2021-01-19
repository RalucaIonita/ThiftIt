using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;

namespace Services
{
    public interface IAccountService : IBaseService
    {

    }
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
