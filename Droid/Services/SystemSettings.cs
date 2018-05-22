using MvxLocationQs.Services;
using Plugin.CurrentActivity;
using Android.Content;
using Android.Locations;
using Plugin.Permissions.Abstractions;

namespace MvxLocationQs.Droid.Services
{
    public class SystemSettings : ISystemSettings
    {
        protected ICurrentActivity CurrentActivity { get; }
        protected IPermissions Permissions { get; }

        public SystemSettings(
            ICurrentActivity currentActivity,
            IPermissions permissions
        )
        {
            CurrentActivity = currentActivity;
            Permissions = permissions;
        }

        public bool EnabledGps()
        {
            var currentActivity = CurrentActivity.Activity;

            var locationManager = (LocationManager)currentActivity.GetSystemService(Context.LocationService);

            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider))
            {
                return true;
            }

            Intent intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
            currentActivity.StartActivity(intent);

            return false;
        }
    }
}
