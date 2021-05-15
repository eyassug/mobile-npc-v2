using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace MobileNPC.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel, IActivatableViewModel
    {
        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();


        public ViewModelBase(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }


        public string UrlPathSegment { get; protected set; }
        public IScreen HostScreen { get; protected set; }
        public ViewModelActivator Activator { get => viewModelActivator; }

        [Reactive]
        public string Title { get; set; }
        [ObservableAsProperty]
        public bool IsOnline { get; }
        [ObservableAsProperty]
        public bool IsBusy { get; }
        [Reactive]
        protected int BusyCounter { get; set; }
        
    }
}
