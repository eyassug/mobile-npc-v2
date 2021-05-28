using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using ReactiveUI;
using Sextant;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace MobileNPC.Views
{
    public partial class MainPage : ReactiveUI.XamForms.ReactiveTabbedPage<ViewModels.MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();

            // To put the tab bar on bottom, Android
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            NavigationPage.SetHasNavigationBar(this, false);

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                if (Children.Count == 0)
                {
                    ViewModel
                        .TabViewModels
                        .ForEach(x => Children.Add(InitializeTabNavigationService(x)));
                }
            });
        }

        private Page InitializeTabNavigationService(Func<IViewStackService, ViewModels.TabViewModel> createViewModelFunc)
        {
            var bgScheduler = RxApp.TaskpoolScheduler;
            var mScheduler = RxApp.MainThreadScheduler;
            var vLocator = Locator.Current.GetService<IViewLocator>();

            var navigationView = new Sextant.XamForms.NavigationView(mScheduler, bgScheduler, vLocator);
            var viewStackService = new ParameterViewStackService(navigationView);
            var model = createViewModelFunc(viewStackService);

            navigationView.Title = model.TabTitle;
            navigationView.IconImageSource = model.TabIcon;

            navigationView.PushPage(model, null, true, false).Subscribe();
            return navigationView;
        }
    }
}
