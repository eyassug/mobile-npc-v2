using System;
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
            ImageLogo.Source = ImageSource.FromResource("MobileNPC.Resources.icon.png");
        }
    }
}