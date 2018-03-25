using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ContosoFieldService.ViewModels.Android;
using FormsToolkit;
using FreshMvvm;
using Xamarin.Forms;

namespace ContosoFieldService.Helpers
{
    public class CustomAndroidNavigation : Xamarin.Forms.MasterDetailPage, IFreshNavigationService
    {
        List<Page> _pagesInner = new List<Page>();
        Dictionary<string, Page> _pages = new Dictionary<string, Page>();
        ObservableCollection<string> _pageNames = new ObservableCollection<string>();

        public Dictionary<string, Page> Pages { get { return _pages; } }
        protected ObservableCollection<string> PageNames { get { return _pageNames; } }

        public CustomAndroidNavigation() : this("CustomNavigation")
        {
        }

        public CustomAndroidNavigation(string navigationServiceName)
        {
            NavigationServiceName = navigationServiceName;
            RegisterNavigation();
        }

        public void Init(string menuTitle, string menuIcon = null)
        {
            CreateMenuPage(menuTitle, menuIcon);
            RegisterNavigation();
        }

        protected virtual void RegisterNavigation()
        {
            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);
        }

        public virtual void AddPage<T>(string title, object data = null) where T : FreshBasePageModel
        {
            var page = FreshPageModelResolver.ResolvePageModel<T>(data);
            page.GetModel().CurrentNavigationServiceName = NavigationServiceName;
            page.Icon = "icon_user.png";
            _pagesInner.Add(page);

            var navigationContainer = CreateContainerPage(page);
            navigationContainer.Icon = "icon_user.png";
            _pages.Add(title, navigationContainer);
            _pageNames.Add(title);

            if (_pages.Count == 1)
                Detail = navigationContainer;
        }

        internal Page CreateContainerPageSafe(Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage || page is TabbedPage)
                return page;

            return CreateContainerPage(page);
        }

        protected virtual Page CreateContainerPage(Page page)
        {
            return new NavigationPage(page);
        }

        protected virtual void CreateMenuPage(string menuPageTitle, string menuIcon = null)
        {
            var menuPage = FreshPageModelResolver.ResolvePageModel<MenuViewModel>();
            menuPage.Title = menuPageTitle;

            MessagingService.Current.Subscribe<string>("NavigationTriggered", (x, args) =>
            {
                if (_pages.ContainsKey((string)args))
                {
                    Detail = _pages[(string)args];
                }

                IsPresented = false;
            });

            Master = menuPage;
        }

        public Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
                return Navigation.PushModalAsync(CreateContainerPageSafe(page));
            return (Detail as NavigationPage).PushAsync(page, animate);
        }

        public Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
                return Navigation.PopModalAsync(animate);
            return (Detail as NavigationPage).PopAsync(animate);
        }

        public Task PopToRoot(bool animate = true)
        {
            return (Detail as NavigationPage).PopToRootAsync(animate);
        }

        public string NavigationServiceName { get; private set; }

        public void NotifyChildrenPageWasPopped()
        {
            if (Master is NavigationPage)
                ((NavigationPage)Master).NotifyAllChildrenPopped();
            foreach (var page in this.Pages.Values)
            {
                if (page is NavigationPage)
                    ((NavigationPage)page).NotifyAllChildrenPopped();
            }
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            var tabIndex = _pagesInner.FindIndex(o => o.GetModel().GetType().FullName == typeof(T).FullName);

            Detail = _pages.Values.ElementAt(tabIndex); ;

            return Task.FromResult((Detail as NavigationPage).CurrentPage.GetModel());
        }
    }
}
