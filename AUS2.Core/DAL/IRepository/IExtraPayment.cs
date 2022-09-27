using AUS2.Core.DAL.Repository;
using AUS2.Core.DBObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.DAL.IRepository
{
    public class ExtraPaymentRepository : Repository<ExtraPayment>, IExtraPayment
    {
        public ExtraPaymentRepository(ApplicationContext context) : base(context)
        {

        }
    }
    public interface IExtraPayment : IServices<ExtraPayment>
    {
    }
}
