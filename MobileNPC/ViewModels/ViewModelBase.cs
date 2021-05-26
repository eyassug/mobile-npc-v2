using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Sextant;
using Splat;

namespace MobileNPC.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IViewModel, IRoutableViewModel, IActivatableViewModel, IDisposable, INavigable
    {
        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();
        protected readonly IViewStackService ViewStackService;

        public ViewModelBase(IScreen hostScreen = null, IViewStackService viewStackService = null)
        {
            UrlPathSegment = nameof(ViewModelBase);
            //HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            ViewStackService = viewStackService ?? Locator.Current.GetService<IViewStackService>();
            NavigationService = ViewStackService;
            ParameterNavigationService = Locator.Current.GetService<IParameterViewStackService>();
            Disposables = new CompositeDisposable();
            // Set IsBusy to true while BusyCounter > 0 (handling parallelism)
            this.WhenAnyValue(x => x.BusyCounter)
                .Select(busyCounter => busyCounter > 0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.IsBusy)
                .DisposeWith(Disposables);
        }

        public ViewModelBase(IViewStackService viewStackService) : this(null, viewStackService: viewStackService)
        {

        }


        public string UrlPathSegment { get; protected set; }
        public IScreen HostScreen { get; protected set; }
        public IParameterViewStackService ParameterNavigationService { get; protected set; }
        public IViewStackService NavigationService { get; }
        public ViewModelActivator Activator { get => viewModelActivator; }
        protected CompositeDisposable Disposables { get; private set; }

        [Reactive]
        public string Title { get; set; }
        [ObservableAsProperty]
        public bool IsOnline { get; }
        [ObservableAsProperty]
        public bool IsBusy { get; }
        [Reactive]
        protected int BusyCounter { get; set; }

        public string Id => UrlPathSegment;


        protected virtual void HandleIsBusy(bool isBusy)
        {
            if (isBusy)
                BusyCounter++;
            else if (BusyCounter > 0)
                BusyCounter--;
        }

        public virtual void Destroy()
        {
            Disposables.Dispose();
            Disposables = null;
        }

        public void Dispose()
        {
            Disposables.Dispose();
            Disposables = null;
        }

        public virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) => Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) => Observable.Return(Unit.Default);
    }
}
