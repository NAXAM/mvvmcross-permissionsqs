using System;
using Foundation;
using UIKit;

namespace MvxLocationQs.iOS
{
    [Preserve(AllMembers = true)]
    public class LinkerPleaseInclude
    {
        public void Include(UIButton button)
        {
            button.TouchUpInside += (s, e) =>
                button.SetTitle(button.Title(UIControlState.Normal), UIControlState.Normal);
        }
    }
}
