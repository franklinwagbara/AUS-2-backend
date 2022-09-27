using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;

namespace AUS2.Core.DAL.IRepository
{
    public class ApplicationRepository : Repository<Application>, IApplication
    {
        public ApplicationRepository(ApplicationContext context) : base(context)
        {

        }
    }

    public interface IApplication : IServices<Application>
    {

    }
}
