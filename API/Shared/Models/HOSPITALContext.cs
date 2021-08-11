using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Shared.Models
{
    public partial class HOSPITALContext : DbContext
    {
        public HOSPITALContext()
        {
        }

        public HOSPITALContext(DbContextOptions<HOSPITALContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Nurse> Nurses { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=HOSPITAL;user id=PC\\Andres;password=;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.WorkerId)
                    .HasName("PK__DOCTOR__077C88265CD93F2F");

                entity.ToTable("DOCTOR");

                entity.Property(e => e.WorkerId).ValueGeneratedNever();

                entity.Property(e => e.Dspecialization)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DSpecialization");

                entity.HasOne(d => d.Worker)
                    .WithOne(p => p.Doctor)
                    .HasForeignKey<Doctor>(d => d.WorkerId)
                    .HasConstraintName("FK__DOCTOR__WorkerId__286302EC");
            });

            modelBuilder.Entity<Nurse>(entity =>
            {
                entity.HasKey(e => e.WorkerId)
                    .HasName("PK__NURSE__077C8826D2B144E1");

                entity.ToTable("NURSE");

                entity.Property(e => e.WorkerId).ValueGeneratedNever();

                entity.Property(e => e.RoomId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Nurses)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__NURSE__RoomID__33D4B598");

                entity.HasOne(d => d.Worker)
                    .WithOne(p => p.Nurse)
                    .HasForeignKey<Nurse>(d => d.WorkerId)
                    .HasConstraintName("FK__NURSE__WorkerId__32E0915F");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PnationalIdentificationNumber)
                    .HasName("PK__PATIENT__280816E112055FCA");

                entity.ToTable("PATIENT");

                entity.Property(e => e.PnationalIdentificationNumber)
                    .ValueGeneratedNever()
                    .HasColumnName("PNationalIdentificationNumber");

                entity.Property(e => e.PbirthDate)
                    .HasColumnType("date")
                    .HasColumnName("PBirthDate");

                entity.Property(e => e.Pcity)
                    .HasMaxLength(85)
                    .IsUnicode(false)
                    .HasColumnName("PCity");

                entity.Property(e => e.PentryDate)
                    .HasColumnType("date")
                    .HasColumnName("PEntryDate");

                entity.Property(e => e.PfirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PFirstName");

                entity.Property(e => e.PlastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PLastName");

                entity.Property(e => e.Pnumber).HasColumnName("PNumber");

                entity.Property(e => e.PphoneNumber).HasColumnName("PPhoneNumber");

                entity.Property(e => e.PpostCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PPostCode");

                entity.Property(e => e.Pstreet)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PStreet");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__PATIENT__RoomID__2E1BDC42");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__PATIENT__WorkerI__2F10007B");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("ROOM");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.Property(e => e.Rcapacity).HasColumnName("RCapacity");

                entity.Property(e => e.Rtype)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RType");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("WORKER");

                entity.Property(e => e.WorkerId).ValueGeneratedNever();

                entity.Property(e => e.Wcity)
                    .HasMaxLength(85)
                    .IsUnicode(false)
                    .HasColumnName("WCity");

                entity.Property(e => e.WfirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WFirstName");

                entity.Property(e => e.WlastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("WLastName");

                entity.Property(e => e.Wnumber).HasColumnName("WNumber");

                entity.Property(e => e.WphoneNumber).HasColumnName("WPhoneNumber");

                entity.Property(e => e.WpostCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("WPostCode");

                entity.Property(e => e.Wsex)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("WSex")
                    .IsFixedLength(true);

                entity.Property(e => e.Wstreet)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WStreet");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
