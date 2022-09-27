using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AUS2.Core.DAL.IRepository
{
    public class CompanyRepository : Repository<Company>, ICompany
    {
        public CompanyRepository(ApplicationContext context) : base(context)
        {

        }
    }

    public interface ICompany : IServices<Company>
    {
    }
}
