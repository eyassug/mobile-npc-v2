﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileNPC.Views
{
    public partial class AboutPage : ReactiveUI.XamForms.ReactiveContentPage<ViewModels.AboutViewModel>
    {
        public AboutPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = new ViewModels.AboutViewModel();
        }
    }
}