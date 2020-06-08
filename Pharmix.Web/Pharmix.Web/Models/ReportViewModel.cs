using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmix.Web.Models
{
    public class ReportViewModel
    {

        [JsonIgnore]
        private string[] _colors = new string[] { "red", "blue", "green" };
        [JsonIgnore]
        private int _currIndex = 0;
        public List<string> labels { get; set; }
        [JsonIgnore]
        private List<ReportDataSetViewModel> _dataSets;
        public List<ReportDataSetViewModel> datasets
        {
            get
            {
                return _dataSets;
            }
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    _currIndex++;
                    _currIndex = _currIndex >= value.Count() ? 0 : _currIndex;
                    value[i].backgroundColor = _colors[_currIndex];
                }
                _dataSets = value;
            }
        }
    }

    public class ReportDataSetViewModel
    {
      
        [JsonIgnore]
        public int Id { get; set; }
        public string label { get; set; }
        public string backgroundColor
        {
            get;set;
        }
        public List<int> data { get; set; }
    }
}
