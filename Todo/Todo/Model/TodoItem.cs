using System;
using System.Collections.Generic;
using System.Text;
using Todo.Helper;

namespace Todo.Model
{
    public class TodoItem : BaseEntity
    {
        public string _Title;

        public string Title 
        {
            get { return _Title; }

            set { _Title = value; OnPropertyChanged(); }
        }

        private bool _IsCompleted;

        public bool IsCompleted
        {
            get { return _IsCompleted; }

            set { _IsCompleted = value; OnPropertyChanged(); }
        }

    }
}
