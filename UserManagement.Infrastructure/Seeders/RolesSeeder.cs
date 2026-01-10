using Common.Domain.Repositories;
using UserManagement.Application.Abstractions;
using UserManagement.Domain.Abstraction;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Seeders
{
    public class RolesSeeder : ISeeder
    {
        private readonly IUserRepository<Role> _roleRepo;

        public RolesSeeder(IUserRepository<Role> roleRepo)
        {
            _roleRepo = roleRepo;
        }
        public int ExecutionOrder { get; set; } = 1;
        public async Task SeedAsync()
        {
            var roles = new List<Role>()
            {
                new Role("مدير", "Admin"),
                new Role("مشرف", "Supervisor"),
                new Role("زبون", "Customer"),
            };

            var existingRoles = _roleRepo.Get();

            var newRoles = roles
                .Where(r => !existingRoles
                    .Any(er => er.NameEn == r.NameEn && er.NameAr == r.NameAr))
                .ToList();

            if (newRoles.Any())
            {
                await _roleRepo.AddRangeAsync(newRoles);
                await _roleRepo.SaveChangesAsync();
            }

        }
    }
}
