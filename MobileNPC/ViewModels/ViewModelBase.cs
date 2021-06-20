using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace MobileNPC.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel, IActivatableViewModel, IDisposable
    {
        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();


        public ViewModelBase(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            Disposables = new CompositeDisposable();

            // Set IsBusy to true while BusyCounter > 0 (handling parallelism)
            this.WhenAnyValue(x => x.BusyCounter)
                .Select(busyCounter => busyCounter > 0)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.IsBusy)
                .DisposeWith(Disposables);
        }


        public string UrlPathSegment { get; protected set; }
        public IScreen HostScreen { get; protected set; }
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
    }
}
