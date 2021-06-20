namespace MobileNPC.ViewModels
{
    using Sextant;
    using ReactiveUI;
    using Splat;
    using System.ComponentModel;
    using System.Reactive.Linq;
    using System.Reactive;
    using System;
    using System.Reactive.Disposables;
    using ReactiveUI.Fody.Helpers;
    using Xamarin.Essentials;
    public class BaseViewModel : ReactiveObject, INotifyPropertyChanged, IActivatableViewModel, IViewModel, INavigable
    {
        public virtual string Id => string.Empty;

        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        protected readonly IViewStackService ViewStackService;
        protected CompositeDisposable Disposables { get; }

        [Reactive]
        public virtual string Title { get; set; }
        [Reactive]
        public bool IsOnline { get; private set; }
        [ObservableAsProperty]
        public bool IsBusy { get; }
        [Reactive]
        protected int BusyCounter { get; set; }

        protected NetworkAccess NetworkAccess => Connectivity.NetworkAccess;

        protected virtual void HandleIsBusy(bool isBusy)
        {
            if (isBusy)
                BusyCounter++;
            else if (BusyCounter > 0)
                BusyCounter--;
        }

        public BaseViewModel(IViewStackService viewStackService)
        {
            ViewStackService = viewStackService ?? Locator.Current.GetService<IParameterViewStackService>();
            Disposables = new CompositeDisposable();
            Connectivity.ConnectivityChanged += ConnectivityChangedHandler;
            
            this.WhenAnyValue(x => x.BusyCounter)
                .Select(busyCounter => busyCounter > 0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.IsBusy)
                .DisposeWith(Disposables);

        }

        private void ConnectivityChangedHandler(object sender, ConnectivityChangedEventArgs e)
        {
            IsOnline = NetworkAccess == NetworkAccess.Internet;
        }

        protected IParameterViewStackService NavigationService => (IParameterViewStackService)ViewStackService;

        public virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) => Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) => Observable.Return(Unit.Default);
    }
}
