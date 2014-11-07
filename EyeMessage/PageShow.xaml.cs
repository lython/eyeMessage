using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using Microsoft.Xna.Framework.Media;

namespace EyeMessage
{
    public partial class PageShow : PhoneApplicationPage
    {
        public static string msg = "";
        public static SolidColorBrush mySolidColor;
        private bool style_text = true;

        public PageShow()
        {
            InitializeComponent();
            text_msg.Text = msg;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            if (mySolidColor != null) text_msg.Foreground = mySolidColor;
        }

        private void PhoneApplicationPage_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            this.NavigationService.GoBack();
        }

        private void text_msg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (style_text)
            {
                storyboard_1.Begin();
                style_text = false;
            }
            else
            {
                storyboard_1.Stop();
                storyboard_2.Stop();
                style_text = true;
            }
            //double actual_height = Application.Current.Host.Content.ActualHeight - text_msg.ActualHeight;
            //double actual_width = Application.Current.Host.Content.ActualWidth - text_msg.ActualWidth;
            
            //var gt = text_msg.TransformToVisual(null);
            //Point p = gt.Transform(new Point(0, 0));

        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
        }

        private void menuItemLock_Click(object sender, EventArgs e)
        {
            //LockHelper("Images/lock.jpg", true);
        }
        
        //更改锁屏背景
        private async void LockHelper(string filePathOfTheImage, bool isAppResource)
        {
            try
            {
                var isProvider = Windows.Phone.System.UserProfile.LockScreenManager.IsProvidedByCurrentApplication;
                if (!isProvider)
                {
                    // If you're not the provider, this call will prompt the user for permission.
                    // Calling RequestAccessAsync from a background agent is not allowed.
                    var op = await Windows.Phone.System.UserProfile.LockScreenManager.RequestAccessAsync();

                    // Only do further work if the access was granted.
                    isProvider = op == Windows.Phone.System.UserProfile.LockScreenRequestResult.Granted;
                }

                if (isProvider)
                {
                    // At this stage, the app is the active lock screen background provider.

                    // The following code example shows the new URI schema.
                    // ms-appdata points to the root of the local app data folder.
                    // ms-appx points to the Local app install folder, to reference resources bundled in the XAP package.
                    var schema = isAppResource ? "ms-appx:///" : "ms-appdata:///Local/";
                    var uri = new Uri(schema + filePathOfTheImage, UriKind.Absolute);

                    // Set the lock screen background image.
                    Windows.Phone.System.UserProfile.LockScreen.SetImageUri(uri);

                    // Get the URI of the lock screen background image.
                    var currentImage = Windows.Phone.System.UserProfile.LockScreen.GetImageUri();
                    System.Diagnostics.Debug.WriteLine("The new lock screen background image is set to {0}", currentImage.ToString());
                }
                else
                {
                    MessageBox.Show("You said no, so I can't update your background.");
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private void storyboard_1_Completed(object sender, EventArgs e)
        {
            storyboard_2.Begin();
        }

        private void storyboard_2_Completed(object sender, EventArgs e)
        {
            storyboard_1.Begin();
        }

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            // 如果是横向
            if (e.Orientation == PageOrientation.Landscape || e.Orientation == PageOrientation.LandscapeLeft || e.Orientation == PageOrientation.LandscapeRight)
            {
                easing_1.Value = 800;
                easing_2.Value = -800;
            }
            // 如果是纵向
            else if (e.Orientation == PageOrientation.Portrait || e.Orientation == PageOrientation.PortraitDown ||
                e.Orientation == PageOrientation.PortraitUp)
            {
                easing_1.Value = 480;
                easing_2.Value = -480;
            }
            else
            {
                easing_1.Value = 480;
                easing_2.Value = -480;
            }
        }
    }
}