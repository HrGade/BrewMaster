using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BrewMaster.Models;

public partial class BrewMasterContext : DbContext
{
    public BrewMasterContext()
    {
    }

    public BrewMasterContext(DbContextOptions<BrewMasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Reciefe> Recieves { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=BrewMaster;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Employee__1788CCACF05F9549");

            entity.ToTable("Employee");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasMany(d => d.Machines).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "GivesService",
                    r => r.HasOne<Machine>().WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GivesService_MachineID"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GivesService_UserID"),
                    j =>
                    {
                        j.HasKey("UserId", "MachineId");
                        j.ToTable("GivesService");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<int>("MachineId").HasColumnName("MachineID");
                    });
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.MachineId).HasName("PK__Machine__44EE5B58E34296A8");

            entity.ToTable("Machine");

            entity.Property(e => e.MachineId).HasColumnName("MachineID");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32D0ECBCA5");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.MachineId).HasColumnName("MachineID");
            entity.Property(e => e.NotTypeId).HasColumnName("NotTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.NotTypeId).HasName("PK__Notifica__8C36C59C8196AA8B");

            entity.ToTable("NotificationType");

            entity.Property(e => e.NotTypeId).HasColumnName("NotTypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.NotType)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reciefe>(entity =>
        {
            entity.HasKey(e => new { e.Date, e.MachineId }).HasName("PK__Recieves__E37698B37026D633");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.MachineId).HasColumnName("MachineID");

            entity.HasOne(d => d.Machine).WithMany(p => p.Recieves)
                .HasForeignKey(d => d.MachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recieves__Machin__300424B4");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB0EA3F2FF038");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Services)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Service__UserID__2F10007B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
