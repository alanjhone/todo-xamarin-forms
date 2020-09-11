using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Helper;

namespace Todo.Model
{
    public class BaseEntity : BaseObservable
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

    }
}
