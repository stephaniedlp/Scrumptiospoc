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
        // Dictionary mapping barcode numbers to names
        var barcodeNames = new Dictionary<string, string>
    {
        { "00850070270021", "Classic Breakfast Burrito" },
        { "00850070270014", "Veggie Breakfast Burrito" },
        { "00850070270106", "The Summer Bod Quesadilla" },
        { "00850070270052", "Say Cheese Quesadilla" },
        { "00850070270038", "Chicken And Cheese Quesadilla Chicken Bacon Pesto Quesadilla" },
        { "00850070270069", "Chicken and Cheese Quesadilla Chicken Bacon Quesadilla" },
        { "00850070270090", "Chicken and Cheese Quesadilla Hunky Texan Quesadilla" },
        { "00850070270076", "Chicken and Cheese Quesadilla Pollo Loco Quesadilla" },
        { "00850070270083", "Beef Quesadilla The Reezo Quesadilla" },
        { "00860006511980", "Chorizo Breakfast Burrito With Egg and Cheese" },
        { "00860006511883", "Pork Sausage Breakfast Burrito With Egg and Cheese" },
        { "00860006511876", "Bacon Breakfast Burrito With Egg and Cheese" }

    };

        foreach (var barcode in e.Results)
            Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");

        var first = e.Results?.FirstOrDefault();
        if (first is not null)
        {
            Dispatcher.Dispatch(async () =>
            {
                // Check if the barcode matches a predefined name
                if (barcodeNames.TryGetValue(first.Value, out var name))
                {
                    // Display the item name in the label
                    ResultLabel.Text = $"Item: {name}";
                }
                else if (Uri.TryCreate(first.Value, UriKind.Absolute, out var uri))
                {
                    // Navigate to the WebViewPage with the URL
                    await Navigation.PushAsync(new WebViewPage(uri.AbsoluteUri));

                    // Stop detection to prevent duplicate scans
                    barcodeView.IsDetecting = false;
                }
                else if (long.TryParse(first.Value, out long barcodeNumber))
                {
                    // If the barcode value is numeric, display it
                    ResultLabel.Text = $"Barcode (Numeric): {barcodeNumber}";
                }
                else
                {
                    // Update the BarcodeGeneratorView and Label as usual for non-numeric barcodes
                    barcodeGenerator.ClearValue(BarcodeGeneratorView.ValueProperty);
                    barcodeGenerator.Format = first.Format;
                    barcodeGenerator.Value = first.Value;

                    // Update the label text
                    ResultLabel.Text = $"Barcodes: {first.Format} -> {first.Value}";

                    // Generate and save the barcode image
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
