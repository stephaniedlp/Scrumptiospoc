using ZXing.Net.Maui.Controls;
using ZXing.Net.Maui;

namespace ScrumptiousApp;

public partial class BarcodePage : ContentPage
{
    public event Action<byte[]> ImageStreamGenerated;

    public BarcodePage()
    {
        InitializeComponent();

        barcodeView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
            Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");

        var first = e.Results?.FirstOrDefault();
        if (first is not null)
        {
            Dispatcher.Dispatch(async () =>
            {
                // Verificar si el valor es un link válido
                if (Uri.TryCreate(first.Value, UriKind.Absolute, out var uri))
                {
                    // Navegar a la página del WebView con la URL
                    await Navigation.PushAsync(new WebViewPage(uri.AbsoluteUri));

                    // Detener la detección para evitar escaneos duplicados
                    barcodeView.IsDetecting = false;
                }
                else
                {
                    // Actualiza el generador y la etiqueta como antes
                    barcodeGenerator.ClearValue(BarcodeGeneratorView.ValueProperty);
                    barcodeGenerator.Format = first.Format;
                    barcodeGenerator.Value = first.Value;

                    // Actualiza el texto en el label
                    ResultLabel.Text = $"Barcodes: {first.Format} -> {first.Value}";

                    // Genera y guarda la imagen del código
                    await GenerateAndSaveBarcodeImageAsync(barcodeGenerator, "barcode.png");
                }
            });
        }
    }

    void SwitchCameraButton_Clicked(object sender, EventArgs e)
    {
        barcodeView.CameraLocation = barcodeView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }

    void TorchButton_Clicked(object sender, EventArgs e)
    {
        barcodeView.IsTorchOn = !barcodeView.IsTorchOn;
    }

    public async Task GenerateAndSaveBarcodeImageAsync(BarcodeGeneratorView barcodeGeneratorView, string fileName)
    {
        var imageBytes = await RenderBarcodeToImageAsync(barcodeGeneratorView);

        // Simula el envío de la imagen generada
        ImageStreamGenerated?.Invoke(imageBytes);

        // Cierra la página después de 3 segundos
        await Task.Delay(3000);
        await Navigation.PopAsync();
    }

    public async Task<byte[]> RenderBarcodeToImageAsync(BarcodeGeneratorView barcodeGeneratorView)
    {
        // Renderiza el contenido visual del BarcodeGeneratorView como imagen
        var screenshotResult = await barcodeGeneratorView.CaptureAsync();
        using var stream = await screenshotResult.OpenReadAsync();
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}
