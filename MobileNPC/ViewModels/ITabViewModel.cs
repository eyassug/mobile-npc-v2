namespace MobileNPC.ViewModels
{
    using Xamarin.Forms;

    public interface ITabViewModel
    {
        string TabTitle { get; }
        ImageSource TabIcon { get; }
    }
}
