using BarcodeScanner.Mobile;

namespace MauiAppScanning.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        //TODO: bool allowed = await
        BarcodeScanner.Mobile.Methods.AskForRequiredPermission();
        

    }
    private void Camera_OnDetected(object sender, OnDetectedEventArg e)
    {
        Debug.WriteLine("Stop");
    }
}
