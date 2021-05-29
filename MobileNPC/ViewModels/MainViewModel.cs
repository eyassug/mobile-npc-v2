namespace MobileNPC.ViewModels
{
    using System;
    using Splat;
    using ReactiveUI;
    using System.Collections.Generic;
    using Sextant;

    public class MainViewModel : BaseViewModel
    {
        public List<Func<IViewStackService, TabViewModel>> TabViewModels
        {
            get => _tabViewModels;
            set => this.RaiseAndSetIfChanged(ref _tabViewModels, value);
        }

        private List<Func<IViewStackService, TabViewModel>> _tabViewModels;

        public MainViewModel(IViewStackService viewStackService = null)
            : base(viewStackService)
        {
            TabViewModels = new List<Func<IViewStackService, TabViewModel>>()
            {
                (customViewStack) => new TabViewModel("Scan", "fas fa-barcode", customViewStack, () => new ScanViewModel(customViewStack)),
                (customViewStack) => new TabViewModel("Red", "fas fa-user", customViewStack, () => new HomeViewModel(customViewStack)),
            };
        }
    }
}
