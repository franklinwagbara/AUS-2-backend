using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;

namespace AUS2.Core.DAL.IRepository
{
    public class FacilityRepository : Repository<Facility>, IFaciltiy
    {
        public FacilityRepository(ApplicationContext context) : base(context)
        {

        }
    }
    public interface IFaciltiy : IServices<Facility>
    {
    }
}
