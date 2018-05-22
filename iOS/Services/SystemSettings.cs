using MvxLocationQs.Services;
using Plugin.Permissions.Abstractions;
using CoreLocation;
namespace MvxLocationQs.iOS.Services
{
    public class SystemSettings : ISystemSettings
    {
        protected IPermissions Permissions { get; }

        public SystemSettings(
            IPermissions permissions
        )
        {
            Permissions = permissions;
        }

        public bool EnabledGps()
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                return true;
            }

            Permissions.OpenAppSettings();

            return false;
        }
    }
}
