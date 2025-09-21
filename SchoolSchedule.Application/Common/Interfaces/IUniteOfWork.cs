namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IUniteOfWork
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
