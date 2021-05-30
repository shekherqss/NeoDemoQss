using NeoDemoQss.UI.UIConstants;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NeoDemoQss.UI.Controls
{
    public class PageTitleControl : Label
    {
        public PageTitleControl()
        {
            base.TextColor = Color.FromHex(Constants.Theme_Control_Foreground_Color);
            base.BackgroundColor = Color.FromHex(Constants.Theme_Control_Background_Color);
            base.FontSize = 22;
            base.FontAttributes = FontAttributes.Bold;
            base.HorizontalTextAlignment = TextAlignment.Center;
            base.VerticalTextAlignment = TextAlignment.Center;
            base.HorizontalOptions = LayoutOptions.FillAndExpand;
            base.VerticalOptions = LayoutOptions.FillAndExpand;
            Margin = new Thickness(0, 20, 0, 50);
            base.Margin = new Thickness(0, 0, 0, 0);
        }
    }
}
