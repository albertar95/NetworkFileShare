using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistence.Context
{
    public class NetworkFileDbContext : DbContext
    {
        public NetworkFileDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Domain.File> Files { get; set; }
        public DbSet<Domain.Folder> Folders { get; set; }
        public DbSet<Domain.AccessLevel> AccessLevels { get; set; }
        public DbSet<Domain.FolderColor> FolderColors { get; set; }
        public DbSet<Domain.FolderIcon> FolderIcons { get; set; }
        public DbSet<Domain.FolderType> FolderTypes { get; set; }
        public DbSet<Domain.SubFolder> SubFolders { get; set; }
        public DbSet<Domain.User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder) 
        {
            //on migration
            //builder.UseSqlServer("Server=.;Database=NetworkFileDb;User Id=sa;Password=safa@123;TrustServerCertificate=true;",
            //    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        }
        protected override void OnModelCreating(ModelBuilder builder) 
        {
            //primary keys
            builder.Entity<Domain.File>().HasKey(f => f.Id);
            builder.Entity<Domain.Folder>().HasKey(f => f.Id);
            builder.Entity<Domain.User>().HasKey(f => f.Id);
            builder.Entity<Domain.AccessLevel>().HasKey(f => f.Id);
            builder.Entity<Domain.FolderColor>().HasKey(f => f.Id);
            builder.Entity<Domain.FolderIcon>().HasKey(f => f.Id);
            builder.Entity<Domain.FolderType>().HasKey(f => f.Id);
            builder.Entity<Domain.SubFolder>().HasKey(f => f.Id);
            //foreign keys
            builder.Entity<Domain.AccessLevel>().HasMany(p => p.Files).WithOne(p => p.AccessLevel).HasForeignKey(f => f.AccessLevelId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Domain.AccessLevel>().HasMany(p => p.Folders).WithOne(p => p.AccessLevel).HasForeignKey(f => f.AccessLevelId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Domain.FolderColor>().HasMany(p => p.Folders).WithOne(p => p.FolderColor).HasForeignKey(f => f.FolderColorId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Domain.FolderIcon>().HasMany(p => p.Folders).WithOne(p => p.FolderIcon).HasForeignKey(f => f.FolderIconId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Domain.FolderType>().HasMany(p => p.Folders).WithOne(p => p.FolderType).HasForeignKey(f => f.FolderTypeId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Domain.Folder>().HasMany(p => p.Files).WithOne(p => p.Folder).HasForeignKey(f => f.FolderId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Domain.Folder>().HasMany(p => p.SubFolders).WithOne(p => p.Folder).HasForeignKey(f => f.RootFolderId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Domain.SubFolder>().HasMany(p => p.SubFolderFiles).WithOne(p => p.SubFolder).HasForeignKey(f => f.SubFolderId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Domain.User>().HasMany(p => p.Folders).WithOne(p => p.User).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
            //indexes
            builder.Entity<Domain.User>().HasIndex(p => p.Username).IsUnique().HasDatabaseName("IX_Username");
            base.OnModelCreating(builder);
        }
    }
}
