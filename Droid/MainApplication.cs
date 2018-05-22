using System;
using Acr.UserDialogs;
using Android.App;
using Android.Runtime;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android;
using MvxLocationQs.Droid.Services;
using MvxLocationQs.Services;
using Plugin.CurrentActivity;

namespace MvxLocationQs.Droid
{
#if DEBUG
    [Application(Debuggable = true)]
#else
    [Application(Debuggable = false)]
#endif
    public class MainApplication : MvxAppCompatApplication<MvxLocationQsSetup, App>
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
            UserDialogs.Init(() => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
        }
    }

    public class MvxLocationQsSetup : MvxAppCompatSetup<App>
    {
        protected override void InitializePlatformServices()
        {
            base.InitializePlatformServices();

            Mvx.RegisterType<ISystemSettings, SystemSettings>();
            Mvx.RegisterSingleton(CrossCurrentActivity.Current);
        }
    }
}