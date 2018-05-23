using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Database
{
	public partial class GAPSegurosContext : DbContext
	{
		public virtual DbSet<CoverageType> CoverageType { get; set; }
		public virtual DbSet<CoverageTypeByPolicy> CoverageTypeByPolicy { get; set; }
		public virtual DbSet<Policy> Policy { get; set; }
		public virtual DbSet<PolicyByUser> PolicyByUser { get; set; }
		public virtual DbSet<RiskType> RiskType { get; set; }
		public virtual DbSet<Role> Role { get; set; }
		public virtual DbSet<RoleByUser> RoleByUser { get; set; }
		public virtual DbSet<User> User { get; set; }

		public GAPSegurosContext(DbContextOptions<GAPSegurosContext> options)
: base(options)
		{ }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
				optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS2014;Database=GAPSeguros;User Id=gapseguros;Password=gapseguros");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CoverageType>(entity =>
			{
				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<CoverageTypeByPolicy>(entity =>
			{
				entity.HasOne(d => d.CoverageType)
					.WithMany(p => p.CoverageTypeByPolicy)
					.HasForeignKey(d => d.CoverageTypeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CoverageTypeByPolicy_CoverageType");

				entity.HasOne(d => d.Policy)
					.WithMany(p => p.CoverageTypeByPolicy)
					.HasForeignKey(d => d.PolicyId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CoverageTypeByPolicy_Policy");
			});

			modelBuilder.Entity<Policy>(entity =>
			{
				entity.Property(e => e.Description)
					.IsRequired()
					.HasMaxLength(120);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(40);

				entity.Property(e => e.Price).HasColumnType("money");

				entity.Property(e => e.ValidityStart).HasColumnType("date");

				entity.HasOne(d => d.RiskType)
					.WithMany(p => p.Policy)
					.HasForeignKey(d => d.RiskTypeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Policy_RiskType");
			});

			modelBuilder.Entity<PolicyByUser>(entity =>
			{
				entity.HasOne(d => d.Policy)
					.WithMany(p => p.PolicyByUser)
					.HasForeignKey(d => d.PolicyId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_PolicyByUser_Policy");

				entity.HasOne(d => d.User)
					.WithMany(p => p.PolicyByUser)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_PolicyByUser_User");
			});

			modelBuilder.Entity<RiskType>(entity =>
			{
				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(30);
			});

			modelBuilder.Entity<Role>(entity =>
			{
				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<RoleByUser>(entity =>
			{
				entity.HasOne(d => d.Role)
					.WithMany(p => p.RoleByUser)
					.HasForeignKey(d => d.RoleId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_RoleByUser_Role");

				entity.HasOne(d => d.User)
					.WithMany(p => p.RoleByUser)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_RoleByUser_User");
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});
		}
	}
}
