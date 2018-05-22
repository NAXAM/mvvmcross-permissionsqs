using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Runtime;
using Plugin.Permissions;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;
using System.Linq;

namespace MvxLocationQs.Droid
{
    [Activity(
        Label = "MvxLocationQs",
        MainLauncher = true,
        Icon = "@mipmap/icon",
        Theme = "@style/AppTheme"
    )]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            System.Diagnostics.Debug.WriteLine(string.Join(", ",  grantResults.Select(x => x.ToString())));

            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

