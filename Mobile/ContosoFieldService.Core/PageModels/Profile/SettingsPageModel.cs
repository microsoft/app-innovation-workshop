using System;
using FreshMvvm;
using Microsoft.AppCenter.Push;

namespace ContosoFieldService.PageModels
{
    public class SettingsPageModel : FreshBasePageModel
    {
        bool notificationsEnabled; 
        public bool NotificationsEnabled
        {
            get
            {
                return notificationsEnabled;
            }
            set
            {
                notificationsEnabled = value; 
                Push.SetEnabledAsync(true);
            }
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NotificationsEnabled = await Push.IsEnabledAsync();
        }
    }
}
