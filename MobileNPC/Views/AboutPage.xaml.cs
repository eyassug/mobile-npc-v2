using System;
using System.ComponentModel;
using System.Reactive.Disposables;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileNPC.Views
{
    public partial class AboutPage : ReactiveUI.XamForms.ReactiveContentPage<ViewModels.AboutViewModel>
    {
        public AboutPage()
        {
            InitializeComponent();
            ImageLogo.Source = ImageSource.FromResource("MobileNPC.Resources.coa_malawi.png");
            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel, x => x.OpenPrivacyPolicy, x => x.btnPrivacy)
                .DisposeWith(disposables);
                this.BindCommand(ViewModel, x => x.OpenTermsAndConditions, x => x.btnTerms)
                .DisposeWith(disposables);
            });
        }
    }
}