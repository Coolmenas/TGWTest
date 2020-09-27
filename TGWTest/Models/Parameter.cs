using System.ComponentModel.DataAnnotations;

namespace TGWTest.Models
{ 
    class Parameter
    {
        public Parameter(string key, string source, dynamic value)
        {
            Key = key;
            Source = source;
            Value = value;
        }
        public string Key { get; }
        public dynamic Value { get; set; }
        public dynamic Source { get; set; }

    }
}
