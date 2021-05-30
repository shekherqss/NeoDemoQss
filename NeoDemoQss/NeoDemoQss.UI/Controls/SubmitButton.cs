using NeoDemoQss.UI.UIConstants;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeoDemoQss.UI.Controls
{
    public class SubmitButton : Button
    {
        public SubmitButton()
        {
            base.HorizontalOptions = LayoutOptions.FillAndExpand;
            base.VerticalOptions = LayoutOptions.EndAndExpand;
            base.CornerRadius = 5;
            base.Margin = new Thickness(20);
            base.TextColor = Color.FromHex(Constants.Theme_Control_Foreground_Color);
            base.BackgroundColor = Color.FromHex(Constants.Theme_Control_Background_Color);
            base.Text = "Submit";
        }
    }
}
