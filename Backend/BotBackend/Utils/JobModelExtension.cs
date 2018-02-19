using LuisBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuisBot.Utils
{
    public static class JobModelExtension
    {
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
}
