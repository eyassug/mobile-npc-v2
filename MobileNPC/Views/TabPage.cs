using System;
using MobileNPC.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace MobileNPC.Views
{
    public class TabPage : ReactiveContentPage<TabViewModel>
    {
        public TabPage()
        {
            this.WhenActivated(disposable =>
            {
                //To make the VM WhenActivated Work
            });
        }
    }
}

