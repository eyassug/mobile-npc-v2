namespace MobileNPC.ViewModels
{
    using Sextant;
    using ReactiveUI;
    using Splat;
    using System.ComponentModel;

    public class BaseViewModel : ReactiveObject, INotifyPropertyChanged, IActivatableViewModel, IViewModel
    {
        public virtual string Id => "BaseViewModel";

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        protected readonly IViewStackService ViewStackService;

        public BaseViewModel(IViewStackService viewStackService)
        {
            ViewStackService = viewStackService ?? Locator.Current.GetService<IViewStackService>();
        }
    }
}
