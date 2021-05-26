using System;
using ReactiveUI;
using Sextant.XamForms;
using Xamarin.Forms;

namespace MobileNPC.Views
{
    public class MainNavigationView :  NavigationView, IViewFor
    {
        public MainNavigationView()
            : base(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler, ViewLocator.Current)
    {
        this.BarBackgroundColor = Color.Blue;
        this.BarTextColor = Color.White;
    }

    public object ViewModel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
}

