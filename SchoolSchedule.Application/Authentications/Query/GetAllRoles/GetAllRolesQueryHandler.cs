using ErrorOr;
using SchoolSchedule.Application.Authentications.Common;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.Common.Interfaces.MediatorInterfaces;
using SchoolSchedule.Application.Mapping.Roles;

namespace SchoolSchedule.Application.Authentications.Query.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ErrorOr<RolesResponse>>
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IRolesRepository _roleRepository;

        public GetAllRolesQueryHandler(IUniteOfWork uniteOfWork,
                                       IRolesRepository roleRepository)
        {
            _uniteOfWork = uniteOfWork;
            _roleRepository = roleRepository;
        }

        public async Task<ErrorOr<RolesResponse>> Handle(GetAllRolesQuery request)
        {
            try
            {
                await _uniteOfWork.BeginTransactionAsync();
                var roles = await _roleRepository.GetAllRolesAsync();
                if (roles.Count is 0)
                    return Error.NotFound(description: "لا يوجد صلاحيات لعرضها");

                var rolesmapping = roles.GetAllRolesMapper();
                await _uniteOfWork.CommitAsync();
                return rolesmapping;
            }
            catch
            {
                await _uniteOfWork.RollbackAsync();
                return Error.Failure(description: ".حدث خطأ اثناء استرجاع الداتا");
            }
        }
    }
}
