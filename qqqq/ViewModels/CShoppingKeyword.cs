using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qqqq.ViewModels
{
    public class CShoppingKeyword
    {
        public string Keyword { get; set; }
        public int ParentCategory { get; set; }
        public int Category { get; set; }
        public int[]SubCategory { get; set; }
        public bool Search { get; set; }
    }
}
