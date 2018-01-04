using System;
using FreshMvvm;
using Xamarin.Forms;

namespace ContosoFieldService.Helpers
{
 
    public class ContosoFieldServiceNavigationContainer : FreshNavigationContainer
    {
        public ContosoFieldServiceNavigationContainer(Page page, string navigationPageName) : base(page, navigationPageName)
        {
        }

        protected override Page CreateContainerPage(Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage || page is TabbedPage)
                return page;

            return new ContosoFieldServiceNavigationPage(page);
        }
    }

    public class ContosoFieldServiceNavigationPage : NavigationPage
    {
        public ContosoFieldServiceNavigationPage()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="page">Page.</param>
        public ContosoFieldServiceNavigationPage(Page page) : base(page)
        {
            // Set BarBackgroundColor to AON Red
            BarTextColor = Color.White;
            BarBackgroundColor = Color.Red;
        }
    }

}
