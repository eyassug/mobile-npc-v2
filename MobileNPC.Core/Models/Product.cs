namespace MobileNPC.Core.Models
{
    using System.Collections.Generic;
    public class Product
    {
        public string Identifier { get; set; }
        public Dictionary<string, Dictionary<string, string>> Attributes { get; set; }
    }
}
