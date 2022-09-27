using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;

namespace AUS2.Core.DAL.IRepository
{
    public class OutOfOfficeRepository : Repository<OutOfOffice>, IOutOfOffice
    {
        public OutOfOfficeRepository(DBObjects.ApplicationContext context) : base(context)
        {
        }
    }
    public interface IOutOfOffice : IServices<OutOfOffice>
    {
    }
}
