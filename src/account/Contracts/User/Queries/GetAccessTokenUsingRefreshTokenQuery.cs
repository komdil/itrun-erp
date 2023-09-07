using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.User.Queries
{
    public record GetAccessTokenUsingRefreshTokenQuery : IRequest<ApplicationResponse<JwtTokenResponse>>
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
