using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CognitiveServicesBot.Services;
using LuisBot.Definitions;
using LuisBot.Dialogs;
using LuisBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        private readonly AzureSearchService searchService = new AzureSearchService();
        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"], 
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            
            string response = "Sorry, what was that? I can only help w specific travels questions. Try asking me stuff like: what are my waiting jobs?";
            await this.ShowLuisResult(context, result, response);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        [LuisIntent("greeting")]
        public async Task GreetingIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result, "Hello there! Need maintenance jobs help? I've got you covered :)");
        }

        [LuisIntent("services.listjobs")]
        public async Task ListJobsIntent(IDialogContext context, IAwaitable<IMessageActivity> message, LuisResult result)
        {
            {
                var messageToForward = await message;

                EntityRecommendation jobSearch;
                if (result.TryFindEntity(ServiceEntities.ServiceStatus, out jobSearch))
                {
                    var model = JobModel.GetContextData(context);
                    // Title case the search entity for consistency
                    model.SearchTerm = new CultureInfo("en").TextInfo.ToTitleCase(jobSearch.Entity.ToLower());
                    var res = jobSearch.Resolution.Values;
                    var resV = res.ToList()[0] as List<object>;
                    model.ResolutionTerm = resV[0].ToString();
                    
                    JobModel.SetContextData(context, model);
                    await context.PostAsync($"OK, let me look for information on ({model.SearchTerm}) jobs.");
                    await context.Forward(new SearchServiceDialog(), AfterDialog, messageToForward, CancellationToken.None);
                }

                // If we cant identify a product entity, start an explore dialog
                else
                {
                    await context.PostAsync($"I couldn't find any jobs with the required status! Waiting, In Progress and Completed are the accepted status");
                }
            }
        }

        [LuisIntent("Cancel")]
        public async Task CancelIntent(IDialogContext context, LuisResult result)
        {
            var response = "Thank you!";
            await this.ShowLuisResult(context, result, response);
        }

        [LuisIntent("Help")]
        public async Task HelpIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result) 
        {
            //Sorry, what was that? I can only help w specific travels questions. Try asking me stuff like: ...
            await context.PostAsync($"I couldn't understand {result.Intents[0].Intent}. You said: {result.Query}. I can answer questions like (what are my jobs?).");
            context.Wait(MessageReceived);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result, string customMessaage)
        {
            await context.PostAsync($"{customMessaage}");
            context.Wait(MessageReceived);
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