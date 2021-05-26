namespace MobileNPC.ViewModels
{
    using System;
    using ReactiveUI;
    using Sextant;

    public class AppShellViewModel : ViewModelBase
    {
        public AppShellViewModel(IScreen hostScreen = null): base(hostScreen)
        {
        }

        public Func<IViewStackService, TabViewModel> ScanTab => (viewStackService) => new TabViewModel("Scan", "fas fa-barcode", viewStackService, () => new ScanViewModel(viewStackService));
        public Func<IViewStackService, TabViewModel> AboutTab => (viewStackService) => new TabViewModel("Scan", "fas fa-barcode", viewStackService, () => new AboutViewModel(viewStackService));
    }
}
