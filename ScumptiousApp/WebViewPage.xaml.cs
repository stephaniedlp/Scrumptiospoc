using System;

namespace ScrumptiousApp;

public partial class WebViewPage : ContentPage
{
	public WebViewPage(string url)
	{
		InitializeComponent();
        webView.Source = url;

    }
}