using QMS.DAL.Models;
using QMS.DAL.Repositories.Interfaces;

namespace QMS.DAL.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Company> Companies { get; }
    IBaseRepository<Branch> Branches { get; }
    IBaseRepository<Feedback> Feedbacks { get; }
    IBaseRepository<Subscription> Subscriptions { get; }
    IBaseRepository<IdentityType> IdentityTypes { get; }
    IBaseRepository<Queue> Queues { get; }
    IBaseRepository<Role> Roles { get; }
    IBaseRepository<Service> Services { get; }
    IBaseRepository<User> Users { get; }
    Task<int> CompleteAsync();
}
