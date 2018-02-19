using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Configuration;

namespace ContosoMaintenance.Bot.WebApp.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        //private IConfiguration configuration;
        private MicrosoftAppCredentials appCredentials;

        public MessagesController(IConfiguration configuration)
        {
            //this.configuration = configuration;
            appCredentials = new MicrosoftAppCredentials(configuration);
        }
        [Authorize(Roles = "Bot")]
        [HttpPost]
        public async Task<OkResult> Post([FromBody] Activity activity)
        {
            if (activity.GetActivityType() == ActivityTypes.Message)
            {
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl), appCredentials);
                //connector.Conversations.SendToConversationAsync(activity, () => new BasicLuisDialog());
                //await Conversation.SendAsync(activity, () => new BasicLuisDialog());
                // return our reply to the user
                var reply = activity.CreateReply("HelloWorld");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            return Ok();
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}