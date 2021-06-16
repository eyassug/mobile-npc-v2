namespace MobileNPC.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Attribute
    {
        public Attribute(string code, string label, Akeneo.Model.ProductValue productValue)
        {
            Code = code;
            Label = label;
            Value = productValue?.Data?.ToString() ?? "N/A";
        }

        public Attribute(string code, string label, string value) : this(code, label, new Akeneo.Model.ProductValue())
        {
            Value = value;
        }
        public string Code { get; }
        public string Label { get; }
        public string Value { get; }
    }
}
