using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WDB
{
    class Exam 
    {
        private Int16 id;
        private string name;
        private DateTime date;


        public Int16 Id { get => id; set => id = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Name { get => name; set => name = value; }
    }
}

