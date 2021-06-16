using System.Collections.Generic;
using WebApp1.GenericRepository;
using WebApp1.Models;

namespace WebApp1.Repository
{
    public interface ITblPCatRepository : IGenericRepository<tblPCat>
    {
        IEnumerable<tblPCat> GetByName(string inName);
    }
}
