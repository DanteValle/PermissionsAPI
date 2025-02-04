using Microsoft.EntityFrameworkCore;
using PermissionsAPI.Model;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PermissionsAPI.DataAccess
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación entre Permission y PermissionType
            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionType)
                .WithMany(pt => pt.Permissions)
                .HasForeignKey(p => p.PermissionTypeId);
        }
    }

}
