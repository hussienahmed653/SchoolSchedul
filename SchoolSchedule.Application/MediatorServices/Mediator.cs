using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using System;

namespace SchoolSchedule.Application.MediatorServices
{
    internal class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handllertype = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            dynamic handler = _serviceProvider.GetService(handllertype)!;
            return await handler.Handle((dynamic)request);
        }
    }
}
