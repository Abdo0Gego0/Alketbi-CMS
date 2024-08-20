using CmsDataAccess;
using CmsDataAccess.Models;
using CmsWeb.Services.Chat;
using CmsWeb.Services.QRServices;
using CmsWeb.Services.UserServices;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SignalR;
using static QRCoder.PayloadGenerator;

namespace CmsWeb.Hubs
{
    public class MyHub:Hub
    {


        // connectionId: from the hub
        // clientId: from the cookie
        // user: is the username
        // message is the message body 

        private readonly IChatService chatService;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;



        private static Dictionary<string, List<string>> groupConnections = new Dictionary<string, List<string>>();


        public MyHub(IChatService chatService_,
            IEmailSender emailSender,
            IConfiguration config,
            IUserService userService

            )
        {
            _userService = userService;
            chatService = chatService_;
            _emailSender = emailSender;
            _config = config;
        }

        public async Task MessageFromClient (string connectionId ,string clientId, string message)
        {

            ApplicationDbContext db=new ApplicationDbContext();


            if (!db.ConnectionGroup.Where(a=>a.GroupName==clientId).Any() )
            {
                db.ConnectionGroup.Add(
                    new ConnectionGroup
                    {
                        GroupName = clientId,
                        CreateDate = _userService.getGulfTime()
                    }
                    ); ;

                db.SaveChanges();

                //< center >  < img src = '{_config.GetValue<string>("ApiUrl")}public/images/footerLogo.png' >  </ center >
                string subject = "New Chat Request";
                string body = $@"
        

        <h2>You have a new chat request.</h2>
        <center>  <img src='{_config.GetValue<string>("ApiUrl")}public/images/mailFooter.png'>  </center>

        </ul>";
                var d = _emailSender.SendEmailAsync("bookings@alketbitravelofficial.ae", subject, body);





                //await Groups.AddToGroupAsync(connectionId, clientId);

                ////await Clients.Group(clientId).SendAsync("ReceiveMessageFromClient", user, message); 
                ////await Clients.All.SendAsync("AddNewGroup",  clientId);
                //await Clients.All.SendAsync("AddNewGroup1", "ghj");

                //return;
            }

            await Groups.AddToGroupAsync(connectionId, clientId);



            if (!groupConnections.ContainsKey(clientId))
            {
                groupConnections[clientId] = new List<string>();
            }

            if(!groupConnections[clientId].Contains(connectionId))
            {
                groupConnections[clientId].Add(connectionId);
            }


            await Clients.All.SendAsync("AddNewGroup1", "ghj");


            if (string.IsNullOrEmpty(message))
                return;

            chatService.InsertMessage(clientId, message, 1,_userService.getGulfTime());

            await Clients.Group(clientId).SendAsync("ReceiveMessageFromClient", message,1);
            await Clients.Group(clientId).SendAsync("ReceiveMessageFromStaff",  message,1); // send to a client



        }
        
        public async Task MessageFromStaff(string connectionId, string clientId, string message)
        {

           

            Groups.AddToGroupAsync(connectionId, clientId);


            if (!groupConnections.ContainsKey(clientId))
            {
                groupConnections[clientId] = new List<string>();
            }

            if (!groupConnections[clientId].Contains(connectionId))
            {
                groupConnections[clientId].Add(connectionId);
            }


            if (string.IsNullOrEmpty(message))
                return;

            chatService.InsertMessage(clientId, message, 2, _userService.getGulfTime());


            //await Clients.Clients(connectionId).SendAsync("ReceiveMessageFromStaff", user, message); // send to a client
            await Clients.Group(clientId).SendAsync("ReceiveMessageFromClient", message,2);
            await Clients.Group(clientId).SendAsync("ReceiveMessageFromStaff", message,2); // send to a client
        }

        public async Task getGroups(string? blabla="")
        {
            List<ConnectionGroup> connectionGroups = new ApplicationDbContext().ConnectionGroup.ToList();

            //foreach (var item in connectionGroups)
            //{
            //    await Clients.All.SendAsync("AddNewGroup", "cccc", item.GroupName,"class");
            //}

            await Clients.All.SendAsync("AddNewGroup1", "ghj");
        }

        public async Task AddToGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);

        }


        public async Task RefreshAdminOrderGrid(string? blabla="blabla")
        {
            await Clients.All.SendAsync("RefreshAdminOrderGrid_", "ghj");
        }


        public async Task RefreshEmployeeOrderGrid(string? blabla = "blabla")
        {
            await Clients.All.SendAsync("RefreshEmployeeOrderGrid_", "ghj");
        }


        public async Task RefreshAdminChatGrid(string? blabla = "blabla")
        {
            await Clients.All.SendAsync("RefreshAdminChatGrid_", "ghj");
        }


        public async Task RefreshEmployeeChatGrid(string? blabla = "blabla")
        {
            await Clients.All.SendAsync("RefreshEmployeeChatGrid_", "ghj");
        }






        public async Task RemoveGroubFromHub(string groupName)
        {

            if (groupConnections.ContainsKey(groupName))
            {

                foreach (var item in groupConnections[groupName])
                {
                    await Groups.RemoveFromGroupAsync(item, groupName);
                    groupConnections[groupName].Remove(item);
                }

                
                if (groupConnections[groupName].Count == 0)
                {
                    groupConnections.Remove(groupName);
                }
            }
        }




    }
}
