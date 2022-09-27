using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;

namespace AUS2.Core.DAL.IRepository
{
    public class PermitRepository : Repository<Permit>, IPermit
    {
        public PermitRepository(ApplicationContext context) : base(context)
        {
        }
    }
    public interface IPermit : IServices<Permit>
    {
    }
}
