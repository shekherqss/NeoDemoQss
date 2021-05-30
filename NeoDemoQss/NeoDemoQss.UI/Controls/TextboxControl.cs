using NeoDemoQss.UI.UIConstants;
using System;
using Xamarin.Forms;

namespace NeoDemoQss.UI.Controls
{
    public class TextboxControl : Entry
    {
        public string ControlResourceId { get; set; }

        public TextboxControl()
        {
            //design setup
            base.TextColor = Color.FromHex(Constants.Theme_Control_Text_Color);
            base.FontSize = 14;
            base.Margin = new Thickness(50, 0, 50, 0);
            this.ControlResourceId = Guid.NewGuid().ToString();
        }
    }
}
