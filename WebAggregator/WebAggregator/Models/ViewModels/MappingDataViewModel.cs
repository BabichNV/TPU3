using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAggregator.Models.ViewModels
{
    public class MappingDataViewModel
    {
        public List<string> CurrentHeaders { get; set; }
        public List<string> NewHeaders { get; set; }
        public MappingDataViewModel()
        {
            CurrentHeaders = new List<string>();
            NewHeaders = new List<string>();
        }
    }
}