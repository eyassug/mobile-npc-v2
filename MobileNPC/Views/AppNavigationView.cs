﻿namespace MobileNPC.Views
{
    using System;
    using ReactiveUI;
    using Sextant.XamForms;
    using Xamarin.Forms;

    public class AppNavigationView : NavigationView, IViewFor
    {
        public AppNavigationView()
            : base(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler, ViewLocator.Current)
        {
            BarBackgroundColor = Color.FromHex("#96d1ff");
            BarTextColor = Color.White;
        }

        public object ViewModel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
