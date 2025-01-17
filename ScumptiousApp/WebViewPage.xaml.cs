using System;

namespace ScrumptiousApp;

public partial class WebViewPage : ContentPage
{
    private string _url;
    public WebViewPage(string url)
    {
        InitializeComponent();
        _url = url; // Store the URL
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine($"WebView handler is {(webView.Handler != null ? "connected" : "null")}");

        // Ensure the WebView is initialized with the correct URL
        if (!string.IsNullOrEmpty(_url))
        {
            webView.Source = _url;
        }
        else
        {
            webView.Source = "about:blank"; // Default to a blank page
        }
    }


    private async void OnQRButtonClicked(object sender, EventArgs e)
    {
        // Navigate back to the root page (QR scanner)
        await Navigation.PopToRootAsync();
    }


}


