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
        // Diccionario con los nombres de productos y códigos de barras
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
                // Extraer GUID desde la URL almacenada en preferencias
                string storedUrl = Preferences.Get("LastOpenedUrl", string.Empty);
                string extractedGuid = ExtractLastGuidFromUrl(storedUrl);

                // Verificar si el código de barras coincide con un producto
                if (barcodeNames.TryGetValue(first.Value, out var productName))
                {
                    // Mostrar el nombre del producto en el label
                    ResultLabel.Text = $"Item: {productName}";
                    await Task.Delay(1000);

                    // Construir la URL con los parámetros
                    string baseUrl = "https://scrumptious-app-a6effacngchyfna6.westus-01.azurewebsites.net/Inventory";
                    string fullUrl = $"{baseUrl}/{Uri.EscapeDataString(extractedGuid)}/{Uri.EscapeDataString(first.Value)}";


                    await Navigation.PushAsync(new WebViewPage(fullUrl));

                  
                }
                else if (Uri.TryCreate(first.Value, UriKind.Absolute, out var uri))
                {
                    Preferences.Set("LastOpenedUrl", uri.AbsoluteUri);
                    // Navegar a la página WebView con la URL
                    await Navigation.PushAsync(new WebViewPage(uri.AbsoluteUri));

                    // Detener la detección para evitar escaneos duplicados
                    barcodeView.IsDetecting = false;
                }
                else if (long.TryParse(first.Value, out long barcodeNumber))
                {
                    // Si el código de barras es numérico, mostrarlo
                    ResultLabel.Text = $"Barcode (Numeric): {barcodeNumber}";
                }
                else
                {
                    // Actualizar la vista del generador de códigos de barras
                    barcodeGenerator.ClearValue(BarcodeGeneratorView.ValueProperty);
                    barcodeGenerator.Format = first.Format;
                    barcodeGenerator.Value = first.Value;

                    // Actualizar el texto en el label
                    ResultLabel.Text = $"Barcodes: {first.Format} -> {first.Value}";

                    // Generar y guardar la imagen del código de barras
                    await GenerateAndSaveBarcodeImageAsync(barcodeGenerator, "barcode.png");
                }
            });
        }
    }

    private string ExtractLastGuidFromUrl(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            // Analizar la URL y extraer el último segmento
            var uri = new Uri(url);
            var lastSegment = uri.Segments.LastOrDefault()?.Trim('/');

            // Validar si el último segmento es un GUID
            if (Guid.TryParse(lastSegment, out Guid guid))
            {
                return guid.ToString();
            }
        }

        return null; // Retorna null si no es un GUID válido
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
