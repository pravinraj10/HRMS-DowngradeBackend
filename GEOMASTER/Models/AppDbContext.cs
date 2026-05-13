using System;
using System.Collections.Generic;
using GEOMASTER.Models.GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Tblbusinessentity> Tblbusinessentities { get; set; }
    public virtual DbSet<Tblcity> Tblcities { get; set; }
    public virtual DbSet<Tblcountry> Tblcountries { get; set; }
    public virtual DbSet<Tblstate> Tblstates { get; set; }
    public virtual DbSet<Tblbusinessunit> Tblbusinessunits { get; set; }
    public virtual DbSet<Tbldepartment> Tbldepartments { get; set; }
    public virtual DbSet<Tblholiday> Tblholidays { get; set; }
    public virtual DbSet<Tbldesignation> Tbldesignations { get; set; }
    public virtual DbSet<Tblrole> Tblroles { get; set; }
    public virtual DbSet<Tblemployee> Tblemployees { get; set; }
    public virtual DbSet<TblMenu> TblMenus { get; set; }
    public virtual DbSet<TblLogin> TblLogins { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblMenu>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.DisplayOrder)
                .HasDefaultValue(1);

            entity.Property(e => e.Icon)
                .HasMaxLength(50);

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.Label)
                .HasMaxLength(100);

            entity.Property(e => e.Url)
                .HasMaxLength(200);

            entity.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Tblbusinessentity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblbusin__3213E83F2985B272");

            entity.ToTable("tblbusinessentity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Address3)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gst)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Pincode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Website)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
               .HasMaxLength(50)
               .IsUnicode(false)
               .HasColumnName("created_by");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Tblcity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblcity__3213E83F1F18F7B8");

            entity.ToTable("tblcity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city_name");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.IsDelete)
                .HasDefaultValue(false)
                .HasColumnName("isDelete");
            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.HasOne(d => d.Country).WithMany(p => p.Tblcities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_City_Country");

            entity.HasOne(d => d.State).WithMany(p => p.Tblcities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK_City_State");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Tblcountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblcount__3213E83FC54F79F9");

            entity.ToTable("tblcountry");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("country_code");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("country_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.IsDelete)
                .HasDefaultValue(false)
                .HasColumnName("isDelete");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Tblstate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblstate__3213E83FD2717F96");

            entity.ToTable("tblstate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.IsDelete)
                .HasDefaultValue(false)
                .HasColumnName("isDelete");
            entity.Property(e => e.StateCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("state_code");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("state_name");

            entity.HasOne(d => d.Country).WithMany(p => p.Tblstates)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_State_Country");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Tblbusinessunit>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("tblbusinessunit");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.UnitCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("unit_id");

            entity.Property(e => e.UnitName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("unit_name");

            //entity.Property(e => e.IsActive)
            //    .HasDefaultValue(true)
            //    .HasColumnName("is_active");

            entity.Property(e => e.IsDelete)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Tbldepartment>(entity =>
        {
            entity.ToTable("tblDepartment");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.DepartmentName)
                .HasColumnName("department_name")
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(255);

            entity.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            entity.Property(e => e.IsDelete)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("GETDATE()");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by")
                .HasMaxLength(100);

            entity.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by")
                .HasMaxLength(100);
        });
        modelBuilder.Entity<Tbldesignation>(entity =>
        {
            entity.ToTable("tbldesignation");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.DesignationName)
                .HasColumnName("designation_name")
                .HasMaxLength(255);

            entity.Property(e => e.DepartmentId)
                .HasColumnName("department_id");

            entity.Property(e => e.Description)
                .HasColumnName("description");

            entity.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            entity.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by");

            //  FK Mapping
            entity.HasOne(d => d.Department)
            .WithMany(p => p.Tbldesignations)
            .HasForeignKey(d => d.DepartmentId)
            .HasConstraintName("FK_Designation_Department");
        });
        modelBuilder.Entity<Tblholiday>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("tblholiday");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.Property(e => e.HolidayDate)
                .HasColumnName("holiday_date");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            entity.Property(e => e.IsDelete)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Tblrole>(entity =>
        {
            entity.ToTable("tblroles");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.RoleName)
                .HasColumnName("role_name")
                .HasMaxLength(100);

            entity.Property(e => e.DepartmentId)
                .HasColumnName("department_id");

            entity.Property(e => e.RoleType)
                .HasColumnName("role_type")
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .HasColumnName("description");

            entity.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false);

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            entity.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by");

            //  FK RELATION
            entity.HasOne(d => d.Department)
                .WithMany(p => p.Tblroles) 
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Role_Department");
        });
        modelBuilder.Entity<Tblemployee>(entity =>
        {
            entity.ToTable("tblemployees");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(100);

            entity.Property(e => e.Gender)
                .HasColumnName("gender")
                .HasMaxLength(20);

            entity.Property(e => e.DateOfBirth)
                .HasColumnName("date_of_birth");

            entity.Property(e => e.PersonalEmail)
                .HasColumnName("personal_email")
                .HasMaxLength(100);

            entity.Property(e => e.PersonalPhone)
                .HasColumnName("personal_phone")
                .HasMaxLength(20);

            entity.Property(e => e.EmergencyContact)
                .HasColumnName("emergency_contact")
                .HasMaxLength(20);

            entity.Property(e => e.Address)
                .HasColumnName("address");

            entity.Property(e => e.DepartmentId)
                .HasColumnName("department_id");

            entity.Property(e => e.DesignationId)
                .HasColumnName("designation_id");

            entity.Property(e => e.JoiningDate)
                .HasColumnName("joining_date");

            entity.Property(e => e.EmployeeCode)
                .HasColumnName("employee_code")
                .HasMaxLength(50);

            // FIXED
            entity.Property(e => e.ReportingManagerId)
                .HasColumnName("reporting_manager");

            entity.Property(e => e.Shift)
                .HasColumnName("shift")
                .HasMaxLength(50);

            entity.Property(e => e.ProfilePhoto)
                .HasColumnName("profile_photo");

            entity.Property(e => e.IdProof)
                .HasColumnName("id_proof");

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at");

            entity.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");

            // Department FK
            entity.HasOne(e => e.Department)
                .WithMany(d => d.Tblemployees)
                .HasForeignKey(e => e.DepartmentId)
                .HasConstraintName("FK_emp_department");

            // Designation FK
            entity.HasOne(e => e.Designation)
                .WithMany(d => d.Tblemployees)
                .HasForeignKey(e => e.DesignationId)
                .HasConstraintName("FK_emp_designation");

            // Reporting Manager FK -> tblroles
            entity.HasOne(e => e.ReportingManager)
                .WithMany(r => r.Employees)
                .HasForeignKey(e => e.ReportingManagerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Employee_Role");
        });
        modelBuilder.Entity<TblLogin>(entity =>
        {
            entity.ToTable("TblLogin");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Username)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.Property(e => e.PasswordHash)
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(e => e.IsActive)
                  .HasDefaultValue(true);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(e => e.Employee)
                  .WithMany()
                  .HasForeignKey(e => e.EmployeeId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.PasswordResetToken)
                  .HasMaxLength(500);

            entity.Property(e => e.PasswordResetTokenExpiry);
        });

        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
