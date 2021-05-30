using MvvmCross.Commands;
using MvvmCross.ViewModels;
using NeoDemoQss.Core.Template;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeoDemoQss.Core.ViewModel
{
    public class FillDetailsTypeOneViewModelSharp : BaseViewModel
    {
        public const string defaultSerializedJson = "{\"title\":\"Music Form\",\"fields\":[{\"label\":\"Band Name\",\"type\":\"textbox\"},{\"label\":\"Genre\",\"type\":\"dropdown\",\"values\":[\"Rock\",\"Metal\",\"Jazz\"]},{\"label\":\"Member Count\",\"type\":\"dropdown\",\"values\":[\"One\",\"Two\",\"Three\",\"More Than Three\"]},{\"label\":\"Rating out of 5\",\"type\":\"numericTextBox\"}]}";

        private string _serializedJson = defaultSerializedJson;
        public string SerializedJson
        {
            get { 
                return _serializedJson; 
            }
            set { SetProperty(ref _serializedJson, value); }
        }



        public override Task Initialize()
        {
            Task.Run(async() =>
            {
                await Task.Delay(2000); // api call and response delay
                PageTemplateInput = JsonConvert.DeserializeObject<PageTemplate>(SerializedJson);
                await RaisePropertyChanged("PageTemplateInput");
            });
            return base.Initialize();
        }

        public IMvxCommand SubmitCommand => new MvxCommand(() =>
        {
            var submit = this.PageTemplateInput;
        });
    }
}
