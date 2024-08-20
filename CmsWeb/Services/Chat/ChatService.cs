using CmsDataAccess;
using CmsDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CmsWeb.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void InsertMessage(string groupId, string message, int sender, DateTime createDate)
        {
            var group = _dbContext.ConnectionGroup.FirstOrDefault(a => a.GroupName == groupId);

            if (group == null)
            {
                throw new InvalidOperationException($"Group with name {groupId} not found.");
            }

            _dbContext.ChatMessages.Add(new ChatMessages
            {
                ConnectionGroupId = group.Id,
                Sender = sender,
                Message = message,
                CreateDate = createDate
            });

            _dbContext.SaveChanges();
        }

        public List<ChatMessages> GetGroupsMessage(Guid groupId)
        {
            return _dbContext.ChatMessages.Where(a => a.ConnectionGroupId == groupId).ToList();
        }

        public Guid GetGroupId(string gName)
        {
            var group = _dbContext.ConnectionGroup.FirstOrDefault(a => a.GroupName == gName);

            if (group == null)
            {
                throw new InvalidOperationException($"Group with name {gName} not found.");
            }

            return group.Id;
        }
    }
}
