using System;
using FreshMvvm;

namespace ContosoFieldService.PageModels
{
    public class JobsPageModel : FreshBasePageModel
    {


        public JobsPageModel()
        {
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if(Helpers.Settings.UserIsLoggedIn == false)
                CoreMethods.PushPageModel<LoginPageModel>(null, true, false);
        }


    }
}
