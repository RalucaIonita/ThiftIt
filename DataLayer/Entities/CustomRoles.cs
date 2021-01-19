using System;

namespace DataLayer.Entities
{
    public static class CustomRoles
    {
        public static Role BasicUser = new Role
        {
            Id = Guid.NewGuid(),
            Name = "BasicUser",
            NormalizedName = "BASICUSER"
        };

        public static Role SuperAdmin = new Role
        {
            Id = Guid.NewGuid(),
            Name = "SuperAdmin",
            NormalizedName = "SUPERADMIN"
        };

    }
}
