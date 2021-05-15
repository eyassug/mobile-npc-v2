using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using MobileNPC.Services;
using System;

namespace MobileNPC.ViewModels
{
    public class ScanViewModel : ViewModelBase
    {
        private readonly Interaction<string, Unit> notFoundInteration;
        private readonly IProductService productService;
        private readonly IGS1ParserService gS1Parser;
        public ScanViewModel(IProductService productService, IGS1ParserService gS1Parser, IScreen hostScreen = null) : base(hostScreen)
        {
            this.productService = productService;
            this.gS1Parser = gS1Parser;
            notFoundInteration = new Interaction<string, Unit>();
            ScanCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                IsAnalyzing = false;
                IsScanning = false;

                string barcodeContent = string.Empty; // Get from Result.Text
                var identifier = this.gS1Parser.GetGTIN(barcodeContent);
                var product = await this.productService.GetProductAsync(identifier);
                if(product != null)
                {
                    HostScreen.Router
                               .Navigate
                               .Execute(new ProductViewModel(product))
                               .Subscribe();
                }
                else
                    _ = ProductNotFoundInteraction.Handle(barcodeContent);

                IsAnalyzing = true;
                IsScanning = true;
            });

        }

        [Reactive]
        public string Result { get; set; }
        [Reactive]
        public bool IsScanning { get; set; }
        [Reactive]
        public bool IsAnalyzing { get; set; }
        public Interaction<string, Unit> ProductNotFoundInteraction => notFoundInteration;
        public ReactiveCommand<Unit, Unit> ScanCommand { get; private set; }
    }
}
