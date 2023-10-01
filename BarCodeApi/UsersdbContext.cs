using System;
using System.Collections.Generic;
using BarCodeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarCodeApi;

public partial class UsersdbContext : DbContext
{
    public UsersdbContext()
    {
    }

    public UsersdbContext(DbContextOptions<UsersdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSustainLogRoom> UserSustainLogRooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=Chmonya;Database=ScudCRM");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.IdRoom).HasName("Room_pkey");

            entity.ToTable("Room");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("userdb_pkey");

            entity.ToTable("User");

            entity.Property(e => e.IdUser).ValueGeneratedNever();
        });

        modelBuilder.Entity<UserSustainLogRoom>(entity =>
        {
            entity.HasKey(e => e.IdSustainLogRoom).HasName("UserSustainLogRoom_pkey");

            entity.ToTable("UserSustainLogRoom");

            entity.HasOne(d => d.IdRoomNavigation).WithMany(p => p.UserSustainLogRooms)
                .HasForeignKey(d => d.IdRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserSustainLogRoom_IdRoom_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserSustainLogRooms)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserSustainLogRoom_IdUser_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
