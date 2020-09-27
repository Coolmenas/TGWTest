using System.Collections.Generic;

namespace TGWTest.Models
{
    class Section
    {
        public Section(string key, List<SubSection> value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public List<SubSection> Value { get; set; }
    }
}
