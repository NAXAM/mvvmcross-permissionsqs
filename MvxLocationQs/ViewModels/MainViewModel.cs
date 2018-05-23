using System;
using System.Windows.Input;
using MvvmCross.ViewModels;
using Plugin.Permissions.Abstractions;
using MvvmCross.Commands;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvxLocationQs.Services;
using MvvmCross.Base;

namespace MvxLocationQs
{
    public class MainViewModel : MvxViewModel
    {
        bool _LocationEnabledAndGranted;
        public bool LocationEnabledAndGranted
        {
            get => _LocationEnabledAndGranted;
            set => SetProperty(ref _LocationEnabledAndGranted, value);
        }

        protected IPermissions Permissions { get; }
        protected IUserDialogs UserDialogs { get; }
        protected ISystemSettings SystemSettings { get; }
        protected IMvxMainThreadAsyncDispatcher MainThreadAsyncDispatcher { get; }

        public MainViewModel(
            IPermissions permissions,
            IUserDialogs userDialogs,
            ISystemSettings systemSettings,
            IMvxMainThreadAsyncDispatcher mainThreadAsyncDispatcher
        )
        {
            Permissions = permissions;
            UserDialogs = userDialogs;
            SystemSettings = systemSettings;
            MainThreadAsyncDispatcher = mainThreadAsyncDispatcher;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            LocationEnabledAndGranted = false;
        }

        ICommand _GetLocationCommand;
        public ICommand GetLocationCommand
        {
            get { return (_GetLocationCommand = _GetLocationCommand ?? new MvxCommand(ExecuteGetLocationCommand)); }
        }
        async void ExecuteGetLocationCommand()
        {
            LocationEnabledAndGranted = false;

            if (false == SystemSettings.EnabledGps())
            {
                return;
            }

            var locationPermissionStatus = await Permissions.CheckPermissionStatusAsync(Permission.LocationWhenInUse);

            switch (locationPermissionStatus)
            {
                case PermissionStatus.Unknown:
                    {
                        await MainThreadAsyncDispatcher.ExecuteOnMainThreadAsync(async delegate
                        {
                            var requested = await Permissions.RequestPermissionsAsync(Permission.LocationWhenInUse);

                            LocationEnabledAndGranted = requested.ContainsKey(Permission.LocationWhenInUse) &&
                                                                 requested[Permission.LocationWhenInUse] == PermissionStatus.Granted;
                        });
                    }
                    break;

                case PermissionStatus.Denied:
                case PermissionStatus.Restricted:
                    {
                        var confirmed = await UserDialogs.ConfirmAsync("We want to access your location to show your good stuff around. Plz grant us access.", "Permission Request", "Open Settings", "Oh no.");

                        if (confirmed)
                        {
                            await MainThreadAsyncDispatcher.ExecuteOnMainThreadAsync(async delegate
                            {
                                var requested = await Permissions.RequestPermissionsAsync(Permission.LocationWhenInUse);

                                LocationEnabledAndGranted = requested.ContainsKey(Permission.LocationWhenInUse) &&
                                                                requested[Permission.LocationWhenInUse] == PermissionStatus.Granted;

                                //Permissions.OpenAppSettings();
                            });
                        }
                    }
                    break;

                case PermissionStatus.Granted:
                    LocationEnabledAndGranted = true;
                    break;
            }
        }
    }
}
