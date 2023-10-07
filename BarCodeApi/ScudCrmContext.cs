using System;
using System.Collections.Generic;
using BarCodeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarCodeApi;

public partial class ScudCrmContext : DbContext
{
    public ScudCrmContext()
    {
    }

    public ScudCrmContext(DbContextOptions<ScudCrmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Database=ScudCRM;Password=Chmonya");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.IdRoom).HasName("Room_pkey");

            entity.ToTable("Room");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("Status_pkey");

            entity.ToTable("Status");

            entity.Property(e => e.Status1).HasColumnName("Status");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Users_pkey");

            entity.ToTable("User");

            entity.Property(e => e.IdUser).HasDefaultValueSql("nextval('\"Users_Id_seq\"'::regclass)");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Status_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
