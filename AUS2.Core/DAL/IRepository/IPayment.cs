using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;

namespace AUS2.Core.DAL.IRepository
{
    public class PaymentRepository : Repository<Payment>, IPayment
    {
        public PaymentRepository(ApplicationContext context) : base(context)
        {
        }
    }
    public interface IPayment : IServices<Payment>
    {
    }
}
