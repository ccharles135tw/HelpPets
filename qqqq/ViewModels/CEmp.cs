using qqqq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.ViewModels
{
    public class CEmp
    {

        public Employee _Employee;
        public CEmp()
        {
            _Employee = new Employee();
        }
        public CEmp(Employee e)
        {
            _Employee = e;
        }

        public int EmpoyeeId { get { return _Employee.EmpoyeeId; }set { _Employee.EmpoyeeId = value; } }
        public string Name { get { return _Employee.Name; } set { _Employee.Name = value; } }
        public string Phone { get { return _Employee.Phone; } set { _Employee.Phone = value; } }
    
    }
}
