namespace MobileNPC.Services
{
    using System.Collections.Generic;
    public interface IGS1ParserService
    {
        string GetGTIN(string barcodeContent);
        Dictionary<string, string> GetApplicationIdentifiers(string barcodeContent);
    }
}
