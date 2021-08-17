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
            if (Configuration.AppConstants.EnvironmentName == Configuration.Countries.Malawi)
                ImageLogo.Source = ImageSource.FromResource("MobileNPC.Resources.coa_malawi.png");
            else
                ImageLogo.Source = ImageSource.FromResource("MobileNPC.Resources.npc_icon.png");
        }
    }
}