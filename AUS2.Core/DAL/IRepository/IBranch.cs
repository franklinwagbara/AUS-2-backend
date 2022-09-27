using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DAL.IRepository
{
    public class BranchRepository : Repository<Branch>, IBranch
    {
        public BranchRepository(ApplicationContext context) : base(context)
        {
        }
    }
    public interface IBranch : IServices<Branch>
    {
    }
}
