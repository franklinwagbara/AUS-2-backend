using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DAL.IRepository
{
    public class NationalityRepository : Repository<Nationality>, INationality
    {
        public NationalityRepository(ApplicationContext context) : base(context)
        {
        }
    }
    public interface INationality : IServices<Nationality>
    {
    }
}
