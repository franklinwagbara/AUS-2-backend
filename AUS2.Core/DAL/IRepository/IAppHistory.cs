using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DAL.IRepository
{
    public class AppHistoryRepository : Repository<AppHistory>, IAppHistory
    {
        public AppHistoryRepository(ApplicationContext context) : base(context)
        {
        }
    }
    public interface IAppHistory : IServices<AppHistory>
    {
    }
}
