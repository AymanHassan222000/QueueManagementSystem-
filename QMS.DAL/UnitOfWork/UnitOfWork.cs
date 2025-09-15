using QMS.DAL.Context;
using QMS.DAL.Models;
using QMS.DAL.Repositories;
using QMS.DAL.Repositories.Interfaces;


namespace QMS.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    #region Private BaseRepository
    private IBaseRepository<Company> _companies;
    private IBaseRepository<Branch> _branches;
    private IBaseRepository<Feedback> _feedbacks;
    private IBaseRepository<Subscription> _subscriptions;
    private IBaseRepository<IdentityType> _identityTypes;
    private IBaseRepository<Queue> _queues;
    private IBaseRepository<Service> _services;
    private IBaseRepository<Role> _roles;
    private IBaseRepository<User> _users;
    #endregion
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();


    #region Public BaseRepository
    public IBaseRepository<Company> Companies
    {
        get
        {
            if (_companies is null)
                _companies = new BaseRepository<Company>(_context);
            return _companies;
        }
    }
    public IBaseRepository<Branch> Branches
    {
        get
        {
            if (_branches is null)
                _branches = new BaseRepository<Branch>(_context);
            return _branches;
        }
    }
    public IBaseRepository<Feedback> Feedbacks
    {
        get
        {
            if (_feedbacks is null)
                _feedbacks = new BaseRepository<Feedback>(_context);
            return _feedbacks;
        }
    }
    public IBaseRepository<Subscription> Subscriptions
    {
        get
        {
            if (_subscriptions is null)
                _subscriptions = new BaseRepository<Subscription>(_context);
            return _subscriptions;
        }
    }
    public IBaseRepository<IdentityType> IdentityTypes
    {
        get
        {
            if (_identityTypes is null)
                _identityTypes = new BaseRepository<IdentityType>(_context);
            return _identityTypes;
        }
    }
    public IBaseRepository<Queue> Queues
    {
        get
        {
            if (_queues is null)
                _queues = new BaseRepository<Queue>(_context);
            return _queues;
        }
    }
    public IBaseRepository<Service> Services
    {
        get
        {
            if (_services is null)
                _services = new BaseRepository<Service>(_context);
            return _services;
        }
    }
    public IBaseRepository<User> Users
    {
        get
        {
            if (_users is null)
                _users = new BaseRepository<User>(_context);
            return _users;
        }
    }
    public IBaseRepository<Role> Roles
    {
        get
        {
            if (_roles is null)
                _roles = new BaseRepository<Role>(_context);
            return _roles;
        }
    }
    
    #endregion
}
