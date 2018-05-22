using Foundation;
using UIKit;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross;
using MvxLocationQs.Services;
using MvxLocationQs.iOS.Services;

namespace MvxLocationQs.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<MvxLocationQsSetup, App>
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            return base.FinishedLaunching(application, launchOptions);
        }
    }

    public class MvxLocationQsSetup : MvxIosSetup<App>
    {
        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices();

            Mvx.RegisterType<ISystemSettings, SystemSettings>();
        }
    }
}

