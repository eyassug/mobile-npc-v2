namespace MobileNPC.ViewModels
{
    using System;
    using Sextant;
    using ReactiveUI;
    using Xamarin.Forms;
    using System.Reactive.Disposables;
    using AP.MobileToolkit.Markup;

    public class TabViewModel : BaseViewModel, ITabViewModel
    {
        public string TabTitle { get; }
        public ImageSource TabIcon { get; }

        public TabViewModel(string tabTitle, string tabIcon, IViewStackService viewStackService, Func<IViewModel> pageCreate) : base(viewStackService)
        {
            TabIcon = new FontImageSource().SetIcon(tabIcon);
            TabTitle = tabTitle;

            this.WhenActivated(disposable =>
            {
                viewStackService.PushPage(pageCreate(), resetStack: true).Subscribe().DisposeWith(disposable);
            });
        }
    }
}
