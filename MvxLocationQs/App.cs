using MvvmCross;
using MvvmCross.ViewModels;
namespace MvxLocationQs
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.RegisterSingleton(Plugin.Permissions.CrossPermissions.Current);
            Mvx.RegisterSingleton(Acr.UserDialogs.UserDialogs.Instance);

            RegisterAppStart<MainViewModel>();
        }
    }
}
