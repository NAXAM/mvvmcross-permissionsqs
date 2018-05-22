using System;

using UIKit;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Binding.BindingContext;

namespace MvxLocationQs.iOS
{
    [MvxFromStoryboard("Main")]
    [MvxRootPresentation(WrapInNavigationController = true)]
    public partial class MainView : MvxViewController<MainViewModel>
    {
        public MainView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var bindingSet = this.CreateBindingSet<MainView, MainViewModel>();
            bindingSet.Bind(btnGetLocation).To(vm => vm.GetLocationCommand);
            bindingSet.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }
    }
}
