using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;

namespace AUS2.Core.DAL.IRepository
{
    public class CategoryRepository : Repository<Category>, ICategory
    {
        public CategoryRepository(ApplicationContext context) : base(context)
        {

        }
    }

    public interface ICategory : IServices<Category>
    {
    }
}
