namespace MobileNPC.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GS1ParserService : IGS1ParserService
    {
        const int MinLength = 13;
        const int MaxLength = 14;
        const string GtinIdentifier = "01";
        public GS1ParserService()
        {
        }

        public Dictionary<string, string> GetApplicationIdentifiers(string barcodeContent)
        {
            if (string.IsNullOrEmpty(barcodeContent)) throw new ArgumentException(nameof(barcodeContent));
            throw new NotImplementedException();
        }

        public string GetGTIN(string barcodeContent)
        {
            if (string.IsNullOrEmpty(barcodeContent)) throw new ArgumentException(nameof(barcodeContent));
            if (barcodeContent.Length == MinLength || barcodeContent.Length == MaxLength)
                return barcodeContent;
            var identifiers = GS1Net.Parse(barcodeContent);
            if(identifiers.Any(i => i.Key.AI == GtinIdentifier))
                return identifiers.Single(i => i.Key.AI == GtinIdentifier).Value;
            throw new ArgumentException(nameof(barcodeContent));
        }
    }
}
