namespace SchoolSchedule.Application.Common.Interfaces
{
    public interface IUniteOfWork
    {
        Task BegingTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
