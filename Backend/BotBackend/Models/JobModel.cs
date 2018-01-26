using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;
using System;

namespace LuisBot.Models
{
    public class JobModel
    {
        public const string ID = "JobModel";
        public string SearchTerm { get; set; }
        public string ResolutionTerm { get; set; }

        public static JobModel GetContextData(IDialogContext context)
        {
            JobModel model;
            context.ConversationData.TryGetValue<JobModel>(JobModel.ID, out model);
            if (model == null)
            {
                model = new JobModel();
                SetContextData(context, model);
            }

            return model;
        }

        public static void SetContextData(IDialogContext context, JobModel model)
        {
            context.ConversationData.SetValue<JobModel>(JobModel.ID, model);
        }

        public static void ClearContextData(IDialogContext context)
        {
            context.ConversationData.RemoveValue(JobModel.ID);
        }
    }

    public enum JobType
    {
        Installation,
        Repair,
        Service
    }

    public enum JobStatus

    {
        Waiting,
        InProgress,
        Complete
    }
}