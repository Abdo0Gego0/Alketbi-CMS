// See https://aka.ms/new-console-template for more information
using CmsDataAccess;
using CmsDataAccess.Enums;
using CmsDataAccess.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");



var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//optionsBuilder.UseSqlServer("Data Source=DESKTOP-NT0OFC8\\MSSQLSERVER01;Initial Catalog=ALKETBI;Integrated Security=True;TrustServerCertificate=True");
optionsBuilder.UseSqlServer("Data Source=SQL6031.site4now.net,1433;Initial Catalog=db_aa21a1_alketbi;User Id=db_aa21a1_alketbi_admin;Password=alketbi_125874;TrustServerCertificate=True;MultipleActiveResultSets=true");

ApplicationDbContext ApplicationDbContext= new ApplicationDbContext(optionsBuilder.Options);

//ApplicationDbContext.Database.EnsureDeleted();

//ApplicationDbContext.Database.EnsureCreated();

//MedicalCenter MedicalCenter= new MedicalCenter
//{
//	Instagram="Instagram",
//	PhoneNumber="1",
//	MobileNumber="2",
//	WhatsApp="3",
//	Address=new Address
//	{
//		AddressTranslation=new List<AddressTranslation>
//		{
//			new AddressTranslation
//			{
//				Building="Building",
//				City="City",
//				Country="Country",
//				LangCode="en-US",
//				Street="Street"
//			}
//		}
//	},
//	MedicalCenterTranslation=new List<MedicalCenterTranslation>
//	{
//		new MedicalCenterTranslation{
//			LangCode="en-US",
//			Description="Description",
//			Name="Name"
//		}
//	}
//};

//ApplicationDbContext.MedicalCenter.Add(MedicalCenter);
//ApplicationDbContext.SaveChanges();



//MedicalCenter MedicalCenter= ApplicationDbContext.MedicalCenter.Include(a=>a.MedicalCenterTranslation).FirstOrDefault();

//MedicalCenter.MedicalCenterTranslation.Add(
//	new MedicalCenterTranslation
//	{
//		Name= "Name",
//		Description= "Description",
//		LangCode= "ar",

//	}
//	);

//ApplicationDbContext.MedicalCenter.Attach(MedicalCenter);
//ApplicationDbContext.SaveChanges();

//List<MedicalCenter> medicalCenter1 = ApplicationDbContext.MedicalCenter
//    .Include(a => a.Address).ThenInclude(a=>a.AddressTranslation)
//    .Include(a => a.MedicalCenterTranslation).ToList();


//ApplicationDbContext.MedicalCenter.RemoveRange(medicalCenter1);
//ApplicationDbContext.SaveChanges();

//MedicalCenter medicalCenter = new MedicalCenter
//{
//  CenterImage="1.png" ,
//   Instagram="Instagram"  ,
//   MobileNumber="Mnumber",
//   PhoneNumber="Pnumber",
//  WhatsApp="Wnumber"  ,
//  Address=new Address
//  {
//      AddressTranslation=new List<AddressTranslation>
//      {
//          new AddressTranslation{ LangCode="en-US",Building="Buidling", City="City",Country="Country",Street="Street"},
//          new AddressTranslation{ LangCode="ar",Building="البناء", City="المدينة",Country="البلد",Street="الشارع"},
//      }
//  },
//  MedicalCenterTranslation = new List<MedicalCenterTranslation>
//  { 
//      new MedicalCenterTranslation{ LangCode="en-US",Description="Description",Name="Name"},
//      new MedicalCenterTranslation{ LangCode="ar",Description="الوصف",Name="الاسم"},
//  }

//};

//ApplicationDbContext.MedicalCenter.Add(medicalCenter);
//ApplicationDbContext.SaveChanges();

/***********************/

//List<ClinicSpeciality> ClinicSpeciality1 = ApplicationDbContext.ClinicSpeciality
//    .Include(a => a.ClinincSpecialityTranslations).ToList();


//ApplicationDbContext.ClinicSpeciality.RemoveRange(ClinicSpeciality1);
//ApplicationDbContext.SaveChanges();


//List<ClinicSpeciality> clinicSpecialities=new List<ClinicSpeciality>
//{
//    new ClinicSpeciality
//    {
//        ClinincSpecialityTranslations =new List<ClinincSpecialityTranslations>
//        {
//            new ClinincSpecialityTranslations {LangCode="en-US",Description="Speciality 1" },
//            new ClinincSpecialityTranslations {LangCode="ar",Description="التخصص 1" }
//        }
//    },
//        new ClinicSpeciality
//    {
//        ClinincSpecialityTranslations =new List<ClinincSpecialityTranslations>
//        {
//            new ClinincSpecialityTranslations {LangCode="en-US",Description="Speciality 2" },
//            new ClinincSpecialityTranslations {LangCode="ar",Description="التخصص 2" }
//        }
//    },
//            new ClinicSpeciality
//    {
//        ClinincSpecialityTranslations =new List<ClinincSpecialityTranslations>
//        {
//            new ClinincSpecialityTranslations {LangCode="en-US",Description="Speciality 2" },
//            new ClinincSpecialityTranslations {LangCode="ar",Description="التخصص 3" }
//        }
//    },
//};

//ApplicationDbContext.ClinicSpeciality.AddRange(clinicSpecialities);
//ApplicationDbContext.SaveChanges();


/***********************/

//List<Clinic> Clinic1 = ApplicationDbContext.Clinic
//    .Include(a => a.ClinicSpeciality)
//    .Include(a => a.ClinicTranslation)
//    .Include(a => a.OpeningHours)
//    .ToList();


//ApplicationDbContext.Clinic.RemoveRange(Clinic1);
//ApplicationDbContext.SaveChanges();


//List<Clinic> clinics = new List<Clinic>
//{
//    new Clinic {
//        ClinicImage="2.png",
//        ClinicSpeciality=new List<ClinicSpeciality> {ApplicationDbContext.ClinicSpeciality.ToList()[0]},
//        ClinicTranslation=new List<ClinicTranslation>
//        {
//            new ClinicTranslation{ LangCode="en-US",Description="Description 1",Name="Clinic 1"},
//            new ClinicTranslation{ LangCode="ar",Description="الوصف 1",Name="العيادة 1"}
//        },
//        OpeningHours= new List<OpeningHours>
//        {
//            new OpeningHours {DayOfWeek=DayOfWeek.Monday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Tuesday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Wednesday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Thursday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Friday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//        }
//    }
//    ,
//        new Clinic {
//        ClinicImage="3.png",
//        ClinicSpeciality=new List<ClinicSpeciality> {ApplicationDbContext.ClinicSpeciality.ToList()[1]},
//        ClinicTranslation=new List<ClinicTranslation>
//        {
//            new ClinicTranslation{ LangCode="en-US",Description="Description 2",Name="Clinic 2"},
//            new ClinicTranslation{ LangCode="ar",Description="الوصف 2",Name="العيادة 2"}
//        },
//        OpeningHours= new List<OpeningHours>
//        {
//            new OpeningHours {DayOfWeek=DayOfWeek.Monday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Tuesday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Wednesday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Thursday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Friday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//        }
//    }

//            ,
//        new Clinic {
//        ClinicImage="4.png",
//        ClinicSpeciality=new List<ClinicSpeciality> {ApplicationDbContext.ClinicSpeciality.ToList()[2]},
//        ClinicTranslation=new List<ClinicTranslation>
//        {
//            new ClinicTranslation{ LangCode="en-US",Description="Description 3",Name="Clinic 3"},
//            new ClinicTranslation{ LangCode="ar",Description="الوصف 3",Name="العيادة 3"}
//        },
//        OpeningHours= new List<OpeningHours>
//        {
//            new OpeningHours {DayOfWeek=DayOfWeek.Monday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Tuesday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Wednesday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Thursday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//            new OpeningHours {DayOfWeek=DayOfWeek.Friday,OpeningTime=new TimeSpan(9,0,0),ClosingTime=new TimeSpan(15,0,0)},
//        }
//    }
//};

//ApplicationDbContext.Clinic.AddRange(clinics);
//ApplicationDbContext.SaveChanges();


/***********************/

//List<Doctor> doctors1 = ApplicationDbContext.Doctor.Include(a => a.DoctorBasicInfoTranslations).Include(a => a.Certificates).ToList();


//ApplicationDbContext.Doctor.RemoveRange(doctors1);
//ApplicationDbContext.SaveChanges();

//List<DoctorSpeciality> DoctorSpeciality1 = ApplicationDbContext.DoctorSpeciality
//    .Include(a => a.DoctorSpecialityTranslations)
//    .ToList();


//ApplicationDbContext.DoctorSpeciality.RemoveRange(DoctorSpeciality1);
//ApplicationDbContext.SaveChanges();


//List<DoctorSpeciality> doctorSpecialities = new List<DoctorSpeciality>
//{
//    new DoctorSpeciality
//    {
//        DoctorSpecialityTranslations =new List<DoctorSpecialityTranslations>
//        {
//            new DoctorSpecialityTranslations {LangCode="en-US",Description="Speciality 1" },
//            new DoctorSpecialityTranslations {LangCode="ar",Description="التخصص 1" }
//        }
//    },
//        new DoctorSpeciality
//    {
//        DoctorSpecialityTranslations =new List<DoctorSpecialityTranslations>
//        {
//            new DoctorSpecialityTranslations {LangCode="en-US",Description="Speciality 2" },
//            new DoctorSpecialityTranslations {LangCode="ar",Description="التخصص 2" }
//        }
//    },
//            new DoctorSpeciality
//    {
//        DoctorSpecialityTranslations =new List<DoctorSpecialityTranslations>
//        {
//            new DoctorSpecialityTranslations {LangCode="en-US",Description="Speciality 2" },
//            new DoctorSpecialityTranslations {LangCode="ar",Description="التخصص 3" }
//        }
//    },
//};

//ApplicationDbContext.DoctorSpeciality.AddRange(doctorSpecialities);
//ApplicationDbContext.SaveChanges();

/***********************/



//List<Doctor> doctors = new List<Doctor>
//{
//    new Doctor{ Certificates=new List<Certificate> {
//        new Certificate{CertificateImage="5.png", Name="Certificate 1"}},
//       DoctorSpeciality= ApplicationDbContext.DoctorSpeciality.ToList()[0],
//       ClinicId= ApplicationDbContext.Clinic.ToList()[0].Id,
//       DoctorBasicInfoTranslations=new List<DoctorBasicInfoTranslations>
//       {
//           new DoctorBasicInfoTranslations {LangCode="en-US",FirstName="First name 1",LastName="Last name 1"},
//           new DoctorBasicInfoTranslations {LangCode="ar",FirstName="الاسم الأول 1",LastName="الاسم الثاني 1"},
//       },

//       DoctorImage="6.png",



//    },

//        new Doctor{ Certificates=new List<Certificate> {
//        new Certificate{CertificateImage="7.png", Name="Certificate 2"}},
//       DoctorSpeciality= ApplicationDbContext.DoctorSpeciality.ToList()[1],
//       ClinicId= ApplicationDbContext.Clinic.ToList()[1].Id,
//       DoctorBasicInfoTranslations=new List<DoctorBasicInfoTranslations>
//       {
//           new DoctorBasicInfoTranslations {LangCode="en-US",FirstName="First name 2",LastName="Last name 2"},
//           new DoctorBasicInfoTranslations {LangCode="ar",FirstName="الاسم الأول 2",LastName="الاسم الثاني 2"},
//       },
//       DoctorImage="8.png",




//    }

//        ,

//        new Doctor{ Certificates=new List<Certificate> {
//        new Certificate{CertificateImage="9.png", Name="Certificate 3"}},
//       DoctorSpeciality= ApplicationDbContext.DoctorSpeciality.ToList()[2],
//       ClinicId= ApplicationDbContext.Clinic.ToList()[2].Id,
//       DoctorBasicInfoTranslations=new List<DoctorBasicInfoTranslations>
//       {
//           new DoctorBasicInfoTranslations {LangCode="en-US",FirstName="First name 3",LastName="Last name 3"},
//           new DoctorBasicInfoTranslations {LangCode="ar",FirstName="الاسم الأول 3",LastName="الاسم الثاني 3"},
//       },
//       DoctorImage="10.png",



//    }
//};

//ApplicationDbContext.Doctor.AddRange(doctors);
//ApplicationDbContext.SaveChanges();


/***********************/

//ApplicationDbContext.AppointmentSetting.Add(new AppointmentSetting());
//ApplicationDbContext.SaveChanges();


/***********************/

//for (int i = 0; i < 3; i++)
//{
//    ApplicationDbContext.FAQ.Add
//        (new FAQ
//        {
//            FAQTranslation = new
//            List<FAQTranslation>()
//            {
//                new FAQTranslation{LangCode="en-US",Question="Question "+i.ToString(),Answer="Answer "+i.ToString()},
//                new FAQTranslation{LangCode="ar",Question="السؤال "+i.ToString(),Answer="الجواب "+i.ToString()}
//            }
//        }
//    );
//    ApplicationDbContext.SaveChanges();
//}

/***********************/

//Guid dId = ApplicationDbContext.Doctor.FirstOrDefault().Id;
//Guid cId = ApplicationDbContext.Clinic.FirstOrDefault().Id;
//Guid pId = ApplicationDbContext.Patient.FirstOrDefault().Id;

//MedicalVisit medical1=new MedicalVisit
//    {
//   BloodPressure=14,
//   ClinicId=cId,
//    Cost=10,
//    Diagnosis= "Diagnosis 1",
//  DoctorId=dId,
//    VisitDate = DateTime.Now,
//    ExaminationStartDate = DateTime.Now.AddMinutes(5),

//    ExaminationEndDate = DateTime.Now.AddMinutes(15),
//  GlucoseLevel=125,
//  HeartBeat=72,
//  Height=183,
//  MedicalStatus="Good",
//  NextVisitDate= DateTime.Now.AddDays(15),
//  PatientId=pId,
//  ProcessingStatus=(int) VisitProcessingStatus.Finished,
//  Reason= "Reason 1",
//  Symptoms= "Symptoms 1",
//  Temperature=38,
//  Weight=75


//};

//MedicalVisit medical2 = new MedicalVisit
//{
//    BloodPressure = 12,
//    ClinicId = cId,
//    Cost = 10,
//    Diagnosis = "Diagnosis 2",
//    DoctorId = dId,
//    VisitDate = DateTime.Now.AddMinutes(15),
//    ExaminationStartDate = DateTime.Now.AddMinutes(5),

//    ExaminationEndDate = DateTime.Now.AddMinutes(15),
//    GlucoseLevel = 115,
//    HeartBeat = 90,
//    Height = 183,
//    MedicalStatus = "Good",
//    PatientId = pId,
//    ProcessingStatus = (int)VisitProcessingStatus.Finished,
//    Reason = "Reason 2",
//    Symptoms = "Symptoms 2",
//    Temperature = 38,
//    Weight = 75


//};

//MedicalVisit medical3 = new MedicalVisit
//{
//    BloodPressure = 12,
//    ClinicId = cId,
//    Cost = 10,
//    Diagnosis = "Diagnosis 3",
//    DoctorId = dId,
//    VisitDate = DateTime.Now.AddMinutes(30),
//    ExaminationStartDate = DateTime.Now.AddMinutes(5),

//    ExaminationEndDate = DateTime.Now.AddMinutes(15),
//    GlucoseLevel = 115,
//    HeartBeat = 90,
//    Height = 183,
//    MedicalStatus = "Good",
//    PatientId = pId,
//    ProcessingStatus = (int)VisitProcessingStatus.Finished,
//    Reason = "Reason 3",
//    Symptoms = "Symptoms 3",
//    Temperature = 38,
//    Weight = 75


//};

//ApplicationDbContext.MedicalVisit.Add(medical1);
//ApplicationDbContext.SaveChanges();
//ApplicationDbContext.MedicalVisit.Add(medical2);
//ApplicationDbContext.SaveChanges();
//ApplicationDbContext.MedicalVisit.Add(medical3);
//ApplicationDbContext.SaveChanges();

/***********************/

//ApplicationDbContext.Banner.Add(new Banner { BannerImage= "b1.JPG", StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(15)});
//ApplicationDbContext.SaveChanges();

//ApplicationDbContext.Banner.Add(new Banner { BannerImage="b2.JPG",StartDate=DateTime.Now,EndDate=DateTime.Now.AddDays(15)});
//ApplicationDbContext.SaveChanges();


/**********************************************/

//Guid pId = ApplicationDbContext.Patient.FirstOrDefault().Id;

//ApplicationDbContext.ArchivedNotification.Add(new ArchivedNotification { CreatedDate= DateTime.Now,IsAuto=true, IsRead=false,Message="first msg",NotiType=0,Title="CMS App",PatientId=pId });
//ApplicationDbContext.ArchivedNotification.Add(new ArchivedNotification { CreatedDate= DateTime.Now.AddHours(18),IsAuto=true, IsRead=false,Message="second msg",NotiType=1,Title="CMS App",PatientId=pId });
//ApplicationDbContext.ArchivedNotification.Add(new ArchivedNotification { CreatedDate= DateTime.Now.AddDays(2),IsAuto=true, IsRead=false,Message="third msg",NotiType=0,Title="CMS App",PatientId=pId });
//ApplicationDbContext.ArchivedNotification.Add(new ArchivedNotification { CreatedDate= DateTime.Now.AddDays(10), IsAuto=true, IsRead=false,Message="forth msg",NotiType=0,Title="CMS App",PatientId=pId });
//ApplicationDbContext.ArchivedNotification.Add(new ArchivedNotification { CreatedDate= DateTime.Now.AddDays(35),IsAuto=true, IsRead=false,Message="fifth msg",NotiType=0, Title="CMS App",PatientId=pId });

//ApplicationDbContext.SaveChanges();


//ApplicationDbContext applicationDbContext = new ApplicationDbContext();

//applicationDbContext.CenterSettings.Remove(applicationDbContext.CenterSettings.FirstOrDefault());

//applicationDbContext.SaveChanges();
