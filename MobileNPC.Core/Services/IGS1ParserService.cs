namespace MobileNPC.Core.Services
{
    using System.Collections.Generic;
    public interface IGS1ParserService
    {
        string GetGTIN(string barcodeContent);
        Dictionary<string, string> GetApplicationIdentifiers(string barcodeContent);
        GS1Properties GetCommonIdentifiers(string barcodeContent);
    }

    public class GS1Properties
    {
        public string GTIN { get; set; }
        public string BatchOrLotNumber { get; set; }
        public string ProductionDate { get; set; }
        public string ExpirationDate { get; set; }
    }
}
