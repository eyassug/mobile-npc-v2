namespace MobileNPC.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GS1ParserService : IGS1ParserService
    {
        const int MinLength = 13;
        const int MaxLength = 14;
        const string GtinIdentifier = GS1Properties.Identifiers.GTIN;
        const string BatchNumberIdentifier = GS1Properties.Identifiers.BatchNumber;
        const string ProductionDateIdentifier = GS1Properties.Identifiers.ProductionDate;
        const string ExpirationDateIdentifier = GS1Properties.Identifiers.ExpirationDate;

        public GS1ParserService()
        {
        }

        public Dictionary<string, string> GetApplicationIdentifiers(string barcodeContent)
        {
            if (string.IsNullOrEmpty(barcodeContent)) throw new ArgumentException(nameof(barcodeContent));
            return GS1Net.Parse(barcodeContent).ToDictionary(ai => ai.Key.AI, ai =>  ai.Value);
        }

        public GS1Properties GetCommonIdentifiers(string barcodeContent)
        {
            var identifiers = GetApplicationIdentifiers(barcodeContent);
            if (barcodeContent.Length == MinLength || barcodeContent.Length == MaxLength)
                return new GS1Properties { GTIN = barcodeContent };
            return new GS1Properties
            {
                GTIN = identifiers.FirstOrDefault(ai => ai.Key == GtinIdentifier).Value,
                BatchOrLotNumber = identifiers.FirstOrDefault(ai => ai.Key == BatchNumberIdentifier).Value,
                ProductionDate = identifiers.FirstOrDefault(ai => ai.Key == ProductionDateIdentifier).Value,
                ExpirationDate = identifiers.FirstOrDefault(ai => ai.Key == ExpirationDateIdentifier).Value,
            };
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
