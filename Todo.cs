using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_App
{
    internal class Todo
    {
        private string title;
        private string description;
        private string dateValue;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string DateValue
        {
            get { return dateValue; }
            set { dateValue = value; }
        }


    }
}
