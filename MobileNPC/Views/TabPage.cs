namespace MobileNPC.Views
{
    using System;
    using ReactiveUI.XamForms;
    using MobileNPC.ViewModels;
    using ReactiveUI;

    public class TabPage : ReactiveContentPage<TabViewModel>
    {
        public TabPage()
        {
            this.WhenActivated(disposable =>
            {
                // Why do I need to add this here to make the VM WhenActivated Work?
            });
        }
    }
}
