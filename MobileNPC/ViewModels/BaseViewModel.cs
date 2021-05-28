namespace MobileNPC.ViewModels
{
    using Sextant;
    using ReactiveUI;
    using Splat;
    using System.ComponentModel;
    using System.Reactive.Linq;
    using System.Reactive;
    using System;

    public class BaseViewModel : ReactiveObject, INotifyPropertyChanged, IActivatableViewModel, IViewModel, INavigable
    {
        public virtual string Id => "BaseViewModel";

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        protected readonly IViewStackService ViewStackService;

        public BaseViewModel(IViewStackService viewStackService)
        {
            ViewStackService = viewStackService ?? Locator.Current.GetService<IParameterViewStackService>();
        }

        protected IParameterViewStackService NavigationService => (IParameterViewStackService)ViewStackService;

        public virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) => Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) => Observable.Return(Unit.Default);
    }
}
