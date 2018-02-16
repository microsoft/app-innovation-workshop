using CognitiveServicesBot.Services;
using LuisBot.Models;
using LuisBot.Utils;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuisBot.Dialogs
{
    [Serializable]
    public class SearchServiceDialog : IDialog<object>
    {
        private readonly AzureSearchService searchService = new AzureSearchService();

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            //You can indicate to the user you are running the query :)
            //await context.PostAsync("Hold on one second!");

            var model = JobModel.GetContextData(context);

            
            if(false)
            {
                //Here you can check if the intention is not found so you can list all the available options
            }
            else
            {
                var results = await searchService.FilterByStatus(model.ResolutionTerm);
                var channelID = message.ChannelId;

                //Check weather we have values in the search result
                if (results.Values.Length > 0)
                {
                    List<Attachment> foundItems = new List<Attachment>();

                    //To display the result in a nice card like boxes, we use custom CardUtil which provide a nice channel specific render of a card using Microsoft.Bot.Connector.Attachment
                    for (int i = 0; i < results.Values.Length; i++)
                    {
                        var attachment = CardUtil.CreateCardAttachment(channelID, results.Values[i]);
                        foundItems.Add(attachment);
                    }

                    var reply = context.MakeMessage();
                    reply.AttachmentLayout = AttachmentLayoutTypes.List;
                    reply.Attachments = foundItems;

                    await context.PostAsync(reply);

                    context.Done<object>(null);
                }
                else
                {
                    await context.PostAsync($"Sorry! I couldn't find anything that matched the search '{model.SearchTerm}'");
                    context.Done<object>(null);
                }
            }
        }

        private async Task AfterDialog(IDialogContext context, IAwaitable<object> result)
        {
            var messageHandled = (string)await result;

            if (!string.IsNullOrEmpty(messageHandled))
            {
                context.Done(messageHandled);
            }
            else
            {
                context.Done<object>(null);
            }
        }
    }
}
