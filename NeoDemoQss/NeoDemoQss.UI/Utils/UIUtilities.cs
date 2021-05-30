using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using NeoDemoQss.Core.Template;
using NeoDemoQss.Core.ViewModel;
using NeoDemoQss.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NeoDemoQss.UI.Utils
{
    public class UIUtilities<TViewModel> where TViewModel : BaseViewModel
    {
        private readonly TViewModel _viewModel;
        public UIUtilities(TViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        /// <summary>
        /// It takes the textbox field details received from api which needs to be mapped to the current Textbox/Entry control to be rendered,
        /// based on the details received it creates the textbox control.
        /// </summary>
        /// <param name="field">This parameter has the details about the textbox control to be created</param>
        /// <param name="isNumeric">This parameters tells whether the current textbox will take only numeric input or not</param>
        /// <returns></returns>
        public StackLayout AddTextboxField(Field field, bool isNumeric = false)
        {
            var input = new TextboxControl();
            input.Unfocused += Textbox_UnFocused;
            input.Placeholder = string.Format("{0} {1}", "Enter", field.Label);
            field.FieldResourceId = input.ControlResourceId;

            if (isNumeric)
            {
                input.Keyboard = Keyboard.Numeric;
            }

            return new StackLayout()
            {
                Children =
                    {
                        new LabelControl(){ Text = field.Label },
                        input
                    }
            };
        }


        /// <summary>
        /// It takes the dropdown field details received from api which needs to be mapped to the current Dropdown/Picker control to be rendered,
        /// based on the details received it creates the Dropdown/Picker control.
        /// </summary>
        /// <param name="field">This parameter has the details about the Dropdown/Picker control to be created</param>
        /// <returns></returns>
        public StackLayout AddDropdownField(Field field)
        {
            DropdownControls picker = new DropdownControls
            {
                Title = field.Label,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            field.FieldResourceId = picker.ControlResourceId;
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;

            foreach (var item in field.Values)
            {
                picker.Items.Add(item);
            }


            return new StackLayout()
            {
                Children = { new LabelControl() { Text = field.Label }, picker }
            };
        }


        /// <summary>
        /// This is a generic Picker Selection event handler which gets called every time a used selects a value on any of the picker/dropdown control.
        /// It uses a "ControlResourceId" id which is a unique GUID mapped to every control, gets assigned to the control at the time of control's object creation.
        /// Since this is a generic event handler getting called for every Picker selection, we use this "ControlResourceId" propety to find the specific picker
        /// whose value was selected.
        /// It also fetches the selected value of specific control which triggered it .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = (DropdownControls)sender;
            var fieldResourceId = (control).ControlResourceId;
            var selectedValue = (control).SelectedItem.ToString();
            var record = _viewModel.PageTemplateInput.Fields.ToList().Where(x => x.FieldResourceId == fieldResourceId).FirstOrDefault();
            record.SelectedValue = selectedValue;
            var check = record.SelectedValue;
            var viewModelField = _viewModel.PageTemplateInput.Fields.Where(x => x.FieldResourceId == fieldResourceId).FirstOrDefault();
            viewModelField.SelectedValue = selectedValue;
        }


        /// <summary>
        /// This is a generic Textbox/Entry Unfocused event handler which gets called every time a user Unfocus  any of the Textbox/Entry control.
        /// It uses a "ControlResourceId" id which is a unique GUID mapped to every control, gets assigned to the control at the time of control's object creation.
        /// Since this is a generic event handler getting called for every Textbox/Entry Unfocus event, we use this "ControlResourceId" propety to find the specific picker
        /// whose value was selected.
        /// It also fetches the selected value of specific control which triggered it .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Textbox_UnFocused(object sender, EventArgs e)
        {
            var control = (TextboxControl)sender;
            var fieldResourceId = (control).ControlResourceId;
            var selectedValue = Convert.ToString((control).Text);
            var record = _viewModel.PageTemplateInput.Fields.ToList().Where(x => x.FieldResourceId == fieldResourceId).FirstOrDefault();
            record.SelectedValue = selectedValue;
            var check = record.SelectedValue;
            var viewModelField = _viewModel.PageTemplateInput.Fields.Where(x => x.FieldResourceId == fieldResourceId).FirstOrDefault();
            viewModelField.SelectedValue = selectedValue;
        }
    }
}
