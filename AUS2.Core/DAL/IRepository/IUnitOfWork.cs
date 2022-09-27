using System;
using System.Threading.Tasks;

namespace AUS2.Core.DAL.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        public IApplication Application { get; }
        public IApplicationForm ApplicationForm { get; }
        public IAppHistory AppHistory { get; }
        public IAppointment Appointment { get; }
        public IBranch Branch { get; }
        public ICategory Category { get; }
        public ICompany Company { get; }
        public IExtraPayment ExtraPayment { get; }
        public IFaciltiy Facility { get; }
        public IFieldOffice FieldOffice { get; }
        public ILGA LGA { get; }
        public IMessage Message { get; }
        public IOutOfOffice OutOfOffice { get; }
        public IPayment Payment { get; }
        public IPermit Permit { get; }
        public IPhase Phase { get; }
        public IPhaseDocument PhaseDocument { get; }
        public IState State { get; }
        public INationality Nationality { get; }
        public IMissingDocument MissingDocument { get; }
        public ISubmittedDocument SubmittedDocument { get; }
        int Save();
        Task<int> SaveChangesAsync(string userId);
    }
}
