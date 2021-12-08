namespace MobileNPC.ViewModels
{
    using System;
    using Sextant;
    using ReactiveUI;
    using Xamarin.Forms;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;

    public class TabViewModel : BaseViewModel, ITabViewModel
    {
        public string TabTitle { get; }
        public ImageSource TabIcon { get; }

        private TabViewModel(string tabTitle, IViewStackService viewStackService, Func<IViewModel> pageCreate) : base(viewStackService)
        {
            TabTitle = tabTitle;
            this.WhenActivated(disposables =>
            {
                viewStackService.PushPage(pageCreate(), resetStack: true).Subscribe().DisposeWith(disposables);
            });
        }

        public TabViewModel(string tabTitle, string tabIconFont, IViewStackService viewStackService, Func<IViewModel> pageCreate)
            : this(tabTitle, viewStackService, pageCreate)
        {
            //TabIcon = new FontImageSource().SetIcon(tabIconFont);
        }

        public TabViewModel(string tabTitle, ImageSource tabIconImage, IViewStackService viewStackService, Func<IViewModel> pageCreate)
            : this(tabTitle, viewStackService, pageCreate)
        {
            TabIcon = tabIconImage;
        }
    }
}
