using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using ReactiveUI.XamForms;
using MobileNPC.ViewModels;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Reactive.Disposables;
using Sextant;
using Splat;

namespace MobileNPC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : ReactiveTabbedPage<AppShellViewModel>
    {
        public AppShell()
        {
            InitializeComponent();

            // To put the tab bar on bottom, Android
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            NavigationPage.SetHasNavigationBar(this, false);

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                if (Children.Count == 0)
                {
                    Children.Add(InitializeTabNavigationService(ViewModel.ScanTab));
                    Children.Add(InitializeTabNavigationService(ViewModel.AboutTab));
                }
            });

        }

        private Page InitializeTabNavigationService(Func<IViewStackService, TabViewModel> createViewModelFunc)
        {
            var bgScheduler = RxApp.TaskpoolScheduler;
            var mScheduler = RxApp.MainThreadScheduler;
            var vLocator = Locator.Current.GetService<IViewLocator>();

            var navigationView = new Sextant.XamForms.NavigationView(mScheduler, bgScheduler, vLocator);
            var viewStackService = new ParameterViewStackService(navigationView);
            var model = createViewModelFunc(viewStackService);

            navigationView.Title = model.TabTitle;
            //navigationView.Icon = model.TabIcon;

            navigationView.PushPage(model, null, true, false).Subscribe();
            return navigationView;
        }
    }
}
