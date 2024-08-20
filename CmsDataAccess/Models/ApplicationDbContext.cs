using CmsDataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;

namespace CmsDataAccess
{
	public class ApplicationDbContext : IdentityDbContext
	{

		public ApplicationDbContext()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

            optionsBuilder.UseSqlServer("Data Source=SQL6031.site4now.net,1433;Initial Catalog=db_aa21a1_alketbi;User Id=db_aa21a1_alketbi_admin;Password=alketbi_125874;TrustServerCertificate=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-NT0OFC8\\MSSQLSERVER01;Initial Catalog=ALKETBI;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
            optionsBuilder.ConfigureWarnings(w => w.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));

            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-NT0OFC8\\MSSQLSERVER01;Initial Catalog=ALKETBI;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
                optionsBuilder.UseSqlServer("Data Source=SQL6031.site4now.net,1433;Initial Catalog=db_aa21a1_alketbi;User Id=db_aa21a1_alketbi_admin;Password=alketbi_125874;TrustServerCertificate=True;MultipleActiveResultSets=true");
                optionsBuilder.ConfigureWarnings(w => w.Ignore(SqlServerEventId.SavepointsDisabledBecauseOfMARS));
                //// Loacal
            }
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Patient>()
				.ToTable("Patient");

            modelBuilder.Entity<CenterTutor>()
    .ToTable("CenterTutor");
            modelBuilder.Entity<CenterSupervisor>()
    .ToTable("CenterSupervisor");
            modelBuilder.Entity<SysAdmin>()
    .ToTable("SysAdmin");
            modelBuilder.Entity<Student>()
.ToTable("Student");
            modelBuilder.Entity<Parent>()
.ToTable("Parent");

            modelBuilder.Entity<RoleMenuPermission>()
				.HasKey(c => new { c.RoleId, c.NavigationMenuId });

            // Configure other properties and relationships

            base.OnModelCreating(modelBuilder);
		}

        public DbSet<Address> Address { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<AddressTranslation> AddressTranslation { get; set; }
        public DbSet<MedicalCenter> MedicalCenter { get; set; }
        public DbSet<MedicalCenterTranslation> MedicalCenterTranslation { get; set; }
        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<ClinicTranslation> ClinicTranslation { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<DoctorBasicInfoTranslations> DoctorBasicInfoTranslations { get; set; }
        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<DoctorSpeciality> DoctorSpeciality { get; set; }
        public DbSet<DoctorSpecialityTranslations> DoctorSpecialityTranslations { get; set; }
        public DbSet<ClinicSpeciality> ClinicSpeciality { get; set; }
        public DbSet<ClinincSpecialityTranslations> ClinincSpecialityTranslations { get; set; }
        public DbSet<DoctorRating> DoctorRating { get; set; }
        public DbSet<ClinicRating> ClinicRating { get; set; }
        public DbSet<CenterRating> CenterRating { get; set; }
        public DbSet<AppointmentSetting> AppointmentSetting { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
        public DbSet<FAQTranslation> FAQTranslation { get; set; }
        public DbSet<MedicalVisit> MedicalVisit { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<ArchivedNotification> ArchivedNotification { get; set; }
        public DbSet<NavigationMenu> NavigationMenu { get; set; }
        public DbSet<RoleMenuPermission> RoleMenuPermission { get; set; }
        public DbSet<AttendanceTable> AttendanceTable { get; set; }
        public DbSet<ValidQR> ValidQR { get; set; }
        public DbSet<EmployeeModel> EmployeeModel { get; set; }
        public DbSet<CenterSettings> CenterSettings { get; set; }
        public DbSet<CenterSettingsTranslation> CenterSettingsTranslation { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<LogInPolicy> LogInPolicy { get; set; }
        public DbSet<BookingPolicy> BookingPolicy { get; set; }
        public DbSet<NotificationPolicy> NotificationPolicy { get; set; }
        public DbSet<ShiftTable> ShiftTable { get; set; }
        public DbSet<CenterAddress> CenterAddress { get; set; }
        public DbSet<MarsCenter> MarsCenter { get; set; }
        public DbSet<CenterTutor> CenterTutor { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<CenterSupervisor> CenterSupervisor { get; set; }
        public DbSet<SysAdmin> SysAdmin { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<CourseBase> CourseBase { get; set; }
        public DbSet<CourseLevel> CourseLevel { get; set; }
        public DbSet<CourseSession> CourseSession { get; set; }
        public DbSet<DailyRecurrence> DailyRecurrence { get; set; }
        public DbSet<WeeklyRecurrence> WeeklyRecurrence { get; set; }
        public DbSet<MyDayOfWeek> MyDayOfWeek { get; set; }
        public DbSet<CourseAttendanceType> CourseAttendanceType { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }

        public DbSet<CompanyEmployee> CompanyEmployee { get; set; }
        public DbSet<ConnectionGroup> ConnectionGroup { get; set; }
        public DbSet<NewRequest> NewRequest { get; set; }
        public DbSet<ChatMessages> ChatMessages { get; set; }
        public DbSet<UserResetToken> UserResetToken { get; set; }




    }
}