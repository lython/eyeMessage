using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EyeMessage.Resources;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;

namespace EyeMessage
{
    public partial class MainPage : PhoneApplicationPage
    {

        Popup pop_list = new Popup();
        PagePopup page_list = new PagePopup();

        Popup pop_color = new Popup();
        PageColor page_color = new PageColor();

        private TransformGroup transformGroup;
        private TranslateTransform translation;

        private double start_pos = 0;

        // 构造函数
        public MainPage()
        {
            InitializeComponent();
            InitializeTransform();
            pop_list.Child = page_list;
            pop_color.Child = page_color;
            page_list.listbox_me.SelectionChanged += listbox_me_SelectionChanged;
            page_list.listbox_me.SelectedIndex = 0;
            textblock_msg.Text = textbox_msg.Text;
            page_color.color_picker.ColorChanged += color_picker_ColorChanged;
        }

        private void InitializeTransform()
        {
            this.transformGroup = new TransformGroup();
            this.translation = new TranslateTransform();

            //建立一個新的TransformGroup，並且設定該Group要呈現的影響項目：位置
            this.transformGroup.Children.Add(this.translation);
            //將TransformGroup指定給Rectangle物件
            this.LayoutRoot.RenderTransform = this.transformGroup;
            this.translation.X = -240;
        }

        private void btn_send_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PageShow.msg = textbox_msg.Text;
            NavigationService.Navigate(new Uri("/PageShow.xaml", UriKind.Relative));
            pop_list.IsOpen = false;
            pop_color.IsOpen = false;
            storyboard_2.Begin();
        }

        private void textbox_msg_TextChanged(object sender, TextChangedEventArgs e)
        {
            textblock_msg.Text = textbox_msg.Text;
        }

        private void btn_list_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (pop_list.IsOpen == false)
            {
                //pop_list.IsOpen = true;
                //page_list.storyboard_1.Begin();
                storyboard_1.Begin();
            }
            else
            {
                //page_list.storyboard_2.Begin();
                storyboard_2.Begin();
            }
        }

        private void btn_save_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            (page_list.listbox_me.SelectedItem as ListBoxItem).Content = "  " + textbox_msg.Text;
        }

        private void storyboard_2_Completed(object sender, EventArgs e)
        {
            pop_list.IsOpen = false;
            pop_color.IsOpen = false;
        }

        private void listbox_me_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.textbox_msg.Text = (page_list.listbox_me.SelectedItem as ListBoxItem).Content.ToString().Substring(2);
        }

        private void color_picker_ColorChanged(object sender, Color color)
        {
            textblock_msg.Foreground = new SolidColorBrush(page_color.color_picker.Color);
            PageShow.mySolidColor = new SolidColorBrush(page_color.color_picker.Color);
        }

        private void textbox_msg_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_msg.Background = new SolidColorBrush(Colors.White);
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (pop_list.IsOpen == true)
            {
                page_list.storyboard_2.Begin();
                storyboard_2.Begin();
                e.Cancel = true;
                return;
            }
            else if (pop_color.IsOpen == true)
            {
                page_color.storyboard_2.Begin();
                storyboard_2.Begin();
                e.Cancel = true;
                return;
            }
            string[] tmp = new string[10];
            for (int i = 0; i < 10; i++)
            {
                tmp[i] = (page_list.listbox_me.Items[i] as ListBoxItem).Content.ToString().Substring(2);
            }
            MyShare.MySaveMessage = tmp;
        }

        private void storyboard_1_Completed(object sender, EventArgs e)
        {
            (page_list.listbox_me.Items[0] as ListBoxItem).Foreground = new SolidColorBrush(Colors.White);
        }

        private void btn_color_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (pop_color.IsOpen == false)
            {
                pop_color.IsOpen = true;
                page_color.storyboard_1.Begin();
                storyboard_3.Begin();
            }
            else
            {
                page_color.storyboard_2.Begin();
                storyboard_2.Begin();
            }
        }

        private void grid_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            start_pos = e.ManipulationOrigin.X;            
        }

        private void grid_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            if (this.translation.X < -10 && e.DeltaManipulation.Translation.X > 0)
            {
                this.translation.X += e.DeltaManipulation.Translation.X;
                this.textbox_msg.Text = this.translation.X.ToString();
            }
            else if (this.translation.X > -240 && e.DeltaManipulation.Translation.X < 0)
            {
                this.translation.X += e.DeltaManipulation.Translation.X;
                this.textbox_msg.Text = this.translation.X.ToString();
            }
 
        }

        private void grid_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            if (this.translation.X > -120)
            {
                this.translation.X = 0;
            }
            else
            {
                this.translation.X = -240;
            }
        }
    }
}