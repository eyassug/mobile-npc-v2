namespace MobileNPC.Views
{
    using System.Reactive.Disposables;
    using ReactiveUI;
    public partial class HomePage : ReactiveUI.XamForms.ReactiveContentPage<ViewModels.HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel, x => x.OpenModal, x => x.FirstModalButton)
                    .DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.PushPage, x => x.PushPage)
                    .DisposeWith(disposables);
            });
        }
    }
}
