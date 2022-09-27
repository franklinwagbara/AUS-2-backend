using AUS2.Core.DAL.IRepository;
using AUS2.Core.DBObjects;
using System;
using System.Threading.Tasks;

namespace AUS2.Core.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Application = Application != null ? Application : new ApplicationRepository(_context);
            ApplicationForm = ApplicationForm != null ? ApplicationForm : new ApplicationFormRepository(_context);
            Appointment = Appointment != null ? Appointment : new AppointmentRepository(_context);
            Branch = Branch != null ? Branch : new BranchRepository(_context);
            Category = Category != null ? Category : new CategoryRepository(_context);
            Company = Company != null ? Company : new CompanyRepository(_context);
            ExtraPayment = ExtraPayment != null ? ExtraPayment : new ExtraPaymentRepository(_context);
            Facility = Facility != null ? Facility : new FacilityRepository(_context);
            LGA = LGA != null ? LGA : new LGARepository(_context);
            Message = Message != null ? Message : new MessageRepository(_context);
            Payment = Payment != null ? Payment : new PaymentRepository(_context);
            Permit = Permit != null ? Permit : new PermitRepository(_context);
            Phase = Phase != null ? Phase : new PhaseRepository(_context);
            PhaseDocument = PhaseDocument != null ? PhaseDocument : new PhaseDocumentRepository(_context);
            State = State != null ? State : new StateRepository(_context);
            AppHistory = AppHistory != null ? AppHistory : new AppHistoryRepository(_context);
            //this.disposed = disposed;
        }

        public IApplication Application { get; private set; }
        public IApplicationForm ApplicationForm { get; private set; }

        public IAppHistory AppHistory { get; private set; }

        public IAppointment Appointment { get; private set; }

        public IBranch Branch { get; private set; }

        public ICategory Category { get; private set; }

        public ICompany Company { get; private set; }

        public IExtraPayment ExtraPayment { get; private set; }

        public IFaciltiy Facility { get; private set; }

        public ILGA LGA { get; private set; }

        public IMessage Message { get; private set; }

        public IOutOfOffice OutOfOffice { get; private set; }

        public IPayment Payment { get; private set; }

        public IPermit Permit { get; private set; }

        public IPhaseDocument PhaseDocument { get; private set; }

        public IState State { get; private set; }

        public IPhase Phase { get; private set; }

        public IFieldOffice FieldOffice { get; private set; }

        public INationality Nationality { get; private set; }

        public IMissingDocument MissingDocument { get; private set; }

        public ISubmittedDocument SubmittedDocument { get; private set; }

        public int Save() => _context.SaveChanges();

        public async Task<int> SaveChangesAsync(string userId) => await _context.SaveChangesAsync(userId);

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    _context.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
