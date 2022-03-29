using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Student
    {
        public string studentId { get; set; }

        public string studentName { get; set; }

        public override string ToString()
        {
            return studentId + " " + studentName;
        }
    }
}
