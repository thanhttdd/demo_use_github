using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp1.GenericRepository;
using WebApp1.Models;
using WebApp1.UnitOfWork;

namespace WebApp1.Repository
{
    public class TblPCatRepository : GenericRepository<tblPCat>, ITblPCatRepository
    {
        public TblPCatRepository(IUnitOfWork<dbContext> unitOfWork) : base(unitOfWork)
        {
        }
    }
}