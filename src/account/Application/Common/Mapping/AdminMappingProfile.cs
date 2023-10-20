using Application.Contract.ApplicationRoles.Commands;
using Application.Contract.ApplicationRoles.Responses;
using AutoMapper;
using Account.Contracts.Claims.Commands;
using Account.Contracts.Claims.Responses;
using Account.Contracts.UserRoles.Commands;
using Account.Contracts.UserRoles.Responses;
using Domain;
using Account.Contracts.User.Response;

namespace Application.Common.Mapping
{
    public class AdminMappingProfile : Profile
    {
        public AdminMappingProfile()
        {
            #region Roles
            CreateMap<CreateRoleCommand, ApplicationRole>();
            CreateMap<ApplicationRole, RoleNameResponse>();
            CreateMap<ApplicationUserRole, UserRolesResponse>();
            CreateMap<CreateUserRolesCommand, ApplicationUserRole>();
            #endregion
            #region Claims
            CreateMap<CreateUserClaimCommand, ApplicationUserClaim>();
            CreateMap<ApplicationUserClaim, SingleClaimResponse>();
            #endregion
            #region Users
            CreateMap<ApplicationUser, UserResponse>()
                .ForMember(s => s.Name, x => x.MapFrom(f => f.UserName));
            #endregion
        }
    }
}
