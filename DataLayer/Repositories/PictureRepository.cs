using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public interface IPictureRepository : IRepositoryBase<Picture>
    {

    }
    public class PictureRepository : RepositoryBase<Picture>, IPictureRepository
    {
        public PictureRepository(Context db) : base(db)
        {
        }
    }
}
