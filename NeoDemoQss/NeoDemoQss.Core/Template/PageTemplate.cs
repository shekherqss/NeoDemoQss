using System;
using System.Collections.Generic;
using System.Text;

namespace NeoDemoQss.Core.Template
{
    public class PageTemplate
    {
        public string Title { get; set; }
        public List<Field> Fields { get; set; }
    }

    public class Field
    {
        public string Label { get; set; }
        public string Type { get; set; }
        public string[] Values { get; set; }
        public string FieldResourceId { get; set; }
        public string SelectedValue { get; set; }
    }
}
