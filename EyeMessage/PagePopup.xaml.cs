using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace EyeMessage
{
    public partial class PagePopup : PhoneApplicationPage
    {
        public PagePopup()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                (listbox_me.Items[i] as ListBoxItem).Content = "  " + MyShare.MySaveMessage[i];
            }
        }

        private void listbox_me_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                (listbox_me.Items[i] as ListBoxItem).Background = new SolidColorBrush(Colors.Transparent);
            }
            (listbox_me.SelectedItem as ListBoxItem).Background = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x7a, 0xcc));
            (listbox_me.SelectedItem as ListBoxItem).Foreground = new SolidColorBrush(Colors.White);
        }
    }
}