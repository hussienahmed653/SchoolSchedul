namespace SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}
