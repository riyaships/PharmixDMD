using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Models
{
    public class DataTablesViewModel
    {
    }
    public class DataTablesParam
    {
        public int draw { get; set; }
        public List<DataTablesColumn> columns { get; set; }
        public List<DataTablesOrder> order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public DataTablesSearch search { get; set; }
    }

    public class DataTablesColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public DataTablesSearch search { get; set; }
    }

    public class DataTablesSearch
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    public class DataTablesOrder
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}
