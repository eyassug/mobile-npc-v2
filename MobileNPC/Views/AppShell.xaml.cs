using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using ReactiveUI.XamForms;
using MobileNPC.ViewModels;

namespace MobileNPC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : ReactiveTabbedPage<AppShellViewModel>
    {
        public AppShell()
        {
            InitializeComponent();
        }
    }
}
