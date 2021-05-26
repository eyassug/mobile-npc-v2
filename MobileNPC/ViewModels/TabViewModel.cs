namespace MobileNPC.ViewModels
{
    using System;
    using System.Reactive.Disposables;
    using ReactiveUI;
    using Sextant;
    using Xamarin.Forms;
    using AP.MobileToolkit;
    using AP.MobileToolkit.Fonts;
    using AP.MobileToolkit.Markup;
    public class TabViewModel : ViewModelBase
    {
        public string TabTitle { get; }
        public ImageSource TabIcon { get; } 
        public TabViewModel(string tabTitle, string tabIcon, IViewStackService viewStackService, Func<IViewModel> pageCreate) : base(viewStackService)
        {
            var iconSource = new FontImageSource().SetIcon(tabIcon);
            TabIcon = iconSource;
            TabTitle = tabTitle;

            this.WhenActivated(disposable =>
            {
                viewStackService.PushPage(pageCreate(), resetStack: true)
                    .Subscribe()
                    .DisposeWith(disposable);
            });
        }
    }
}
