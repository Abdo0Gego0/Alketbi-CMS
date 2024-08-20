using CmsDataAccess.Models;

namespace CmsWeb.Services.Chat
{
    public interface IChatService
    {
        public void InsertMessage(string GroupId, string message, int sender, DateTime createDate);
        public List<ChatMessages> GetGroupsMessage(Guid GroupId);

        public Guid GetGroupId(string gName);
    }
}
