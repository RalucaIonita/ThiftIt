using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;

namespace Services
{
    public interface IProductService : IBaseService
    {

    }
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
