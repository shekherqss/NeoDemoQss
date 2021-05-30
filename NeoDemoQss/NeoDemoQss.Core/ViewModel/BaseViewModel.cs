using MvvmCross.Commands;
using MvvmCross.ViewModels;
using NeoDemoQss.Core.Template;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeoDemoQss.Core.ViewModel
{
    public class BaseViewModel : MvxViewModel
    {
        private PageTemplate _pageTemplateInput = default;
        public PageTemplate PageTemplateInput
        {
            get
            {
                return _pageTemplateInput;
            }
            set { SetProperty(ref _pageTemplateInput, value); }
        }
    }
}
