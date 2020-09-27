using System;
using System.Collections.Generic;
using System.Text;

namespace TGWTest.Models
{
    class SubSection
    {
        public SubSection(string key, List<Parameter> value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public List<Parameter> Value { get; set; }
    }
}
