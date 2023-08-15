using Contracts.Requests.Admin;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Admin
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator()
        {
            RuleFor(s => s.Role)
                .Must(s => s != "Admin")
                .WithMessage("Role should not be an admin");
        }
    }
}
