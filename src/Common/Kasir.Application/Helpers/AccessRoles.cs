using Kasir.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kasir.Application.Helpers
{
    public static class AccessRoles
    {
        public const string User = nameof(UserRole.User);

        public const string Admin = nameof(UserRole.Admin);

        public const string Developer = nameof(UserRole.Developer);
    }
}
