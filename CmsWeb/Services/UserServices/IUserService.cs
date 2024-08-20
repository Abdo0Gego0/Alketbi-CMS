namespace CmsWeb.Services.UserServices
{
    public interface IUserService
    {
        object GetPatientInfo();
        Guid? GetMyId();
        string GetMyRole();
		object GetPatientDetails();

        string GetMyLanguage();

        Guid GetMyCenterId();
        string GetUserFirstName();

        string GetUserName();

        public int? GetJobType();

        public DateTime getGulfTime();


    }
}
