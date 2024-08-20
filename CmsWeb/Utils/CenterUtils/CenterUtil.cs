using CmsDataAccess;
using CmsDataAccess.Models;

namespace CmsWeb.Utils.CenterUtils
{
    public static class CenterUtil
    {

        public static void DeleteCenter(Guid id)
        {

            ApplicationDbContext cms = new ApplicationDbContext();
            MarsCenter marsCenter = cms.MarsCenter.Find(id);
            marsCenter.IsDeleted = true;
            cms.MarsCenter.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.IsDeleted).IsModified = true;
            cms.SaveChanges();

            List<CenterSupervisor> centerSupervisors = cms.CenterSupervisor.Where(a => a.MarsCenterId == id).ToList();
            List<CenterTutor> CenterTutor = cms.CenterTutor.Where(a => a.MarsCenterId == id).ToList();
            List<Student> CenterStudent = cms.Student.Where(a => a.MarsCenterId == id).ToList();


            foreach (var item in centerSupervisors)
            {
                DeleteCenterSupervisor(item.Id);
            }

            foreach (var item in CenterTutor)
            {
                DeleteCenterTutor(item.Id);
            }

            foreach (var item in CenterStudent)
            {
                DeleteCenterStudent(item.Id);
            }
        }

        public static void DeleteCenterSupervisor(Guid id)
        {
            ApplicationDbContext cms = new ApplicationDbContext();
            CenterSupervisor marsCenter = cms.CenterSupervisor.Find(id);
            marsCenter.IsDeleted = true;
            cms.CenterSupervisor.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.IsDeleted).IsModified = true;
            cms.SaveChanges();
        }

        public static void DeleteCenterTutor(Guid id)
        {
            ApplicationDbContext cms = new ApplicationDbContext();
            CenterTutor marsCenter = cms.CenterTutor.Find(id);
            marsCenter.IsDeleted = true;
            cms.CenterTutor.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.IsDeleted).IsModified = true;
            cms.SaveChanges();
        }

        public static void DeleteCenterStudent(Guid id)
        {
            ApplicationDbContext cms = new ApplicationDbContext();
            Student marsCenter = cms.Student.Find(id);
            marsCenter.IsDeleted = true;
            cms.Student.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.IsDeleted).IsModified = true;
            cms.SaveChanges();
        }


        public static bool ChangeCenterStatus(Guid id)
        {

            ApplicationDbContext cms = new ApplicationDbContext();
            MarsCenter marsCenter = cms.MarsCenter.Find(id);
            marsCenter.Status = !marsCenter.Status;
            cms.MarsCenter.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.IsDeleted).IsModified = true;
            cms.SaveChanges();

            List<CenterSupervisor> centerSupervisors = cms.CenterSupervisor.Where(a => a.MarsCenterId == id).ToList();
            List<CenterTutor> CenterTutor = cms.CenterTutor.Where(a => a.MarsCenterId == id).ToList();
            List<Student> CenterStudent = cms.Student.Where(a => a.MarsCenterId == id).ToList();


            foreach (var item in centerSupervisors)
            {
                ChangeCenterSupervisorStatusByCenter(item.Id);
            }

            foreach (var item in CenterTutor)
            {
                ChangeCenterTutorStatusByCenter(item.Id);
            }

            foreach (var item in CenterStudent)
            {
                ChangeCenterStudentStatusByCenter(item.Id);
            }

            return marsCenter.Status;
        }

        public static void ChangeCenterSupervisorStatusByCenter(Guid id)
        {
            ApplicationDbContext cms = new ApplicationDbContext();
            CenterSupervisor marsCenter = cms.CenterSupervisor.Find(id);
            marsCenter.CenterStatus = !marsCenter.CenterStatus;
            cms.CenterSupervisor.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.CenterStatus).IsModified = true;
            cms.SaveChanges();
        }

        public static void ChangeCenterTutorStatusByCenter(Guid id)
        {
            ApplicationDbContext cms = new ApplicationDbContext();
            CenterTutor marsCenter = cms.CenterTutor.Find(id);
            marsCenter.CenterStatus = !marsCenter.CenterStatus;
            cms.CenterTutor.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.CenterStatus).IsModified = true;
            cms.SaveChanges();
        }

        public static void ChangeCenterStudentStatusByCenter(Guid id)
        {
            ApplicationDbContext cms = new ApplicationDbContext();
            Student marsCenter = cms.Student.Find(id);
            marsCenter.CenterStatus = !marsCenter.CenterStatus;
            cms.Student.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.CenterStatus).IsModified = true;
            cms.SaveChanges();
        }


        public static bool ChangeCenterSupervisorStatus(Guid id)
        {
            ApplicationDbContext cms = new ApplicationDbContext();
            CenterSupervisor marsCenter = cms.CenterSupervisor.Find(id);
            marsCenter.Status = !marsCenter.Status;
            cms.CenterSupervisor.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.Status).IsModified = true;
            cms.SaveChanges();
            return marsCenter.Status;
        }

        public static bool ChangeCenterTutorStatus(Guid id)
        {
            ApplicationDbContext cms = new ApplicationDbContext();
            CenterTutor marsCenter = cms.CenterTutor.Find(id);
            marsCenter.Status = !marsCenter.Status;
            cms.CenterTutor.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.Status).IsModified = true;
            cms.SaveChanges();
            return marsCenter.Status;
        }

        public static bool ChangeCenterStudentStatus(Guid id)
        {

            ApplicationDbContext cms = new ApplicationDbContext();
            Student marsCenter = cms.Student.Find(id);
            marsCenter.Status = !marsCenter.Status;
            cms.Student.Attach(marsCenter);
            cms.Entry(marsCenter).Property(d => d.Status).IsModified = true;
            cms.SaveChanges();
            return marsCenter.Status;
        }


    }
}
