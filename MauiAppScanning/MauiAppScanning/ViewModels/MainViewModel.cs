using BarcodeScanner.Mobile;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Maui.Core.Views;
using Dapper;
using IdParser;
using Microsoft.Data.SqlClient;
using System.Runtime.Versioning;

namespace MauiAppScanning.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly LocalDataService localDataService;

    [ObservableProperty]
    private bool isScanning = true;

    [ObservableProperty]
    private bool torchOn = false;

    [ObservableProperty]
    private bool vibrationDetected = true;

    [ObservableProperty]
    private IdentificationCard idCard;

    public MainViewModel(LocalDataService localDataService)
    {
        this.localDataService = localDataService;
    }

    [RelayCommand]
    public void BarcodeDetected(OnDetectedEventArg barcodeData)
    {
        if (barcodeData?.BarcodeResults?.Count == 0)
            return;

        var data = barcodeData.BarcodeResults[0];
        if (data.BarcodeFormat != BarcodeFormats.Pdf417)
            return;

        try
        {
            IdCard = Barcode.Parse(barcodeData.BarcodeResults[0].RawValue);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            //TODO: Log the exception
        }
    }

    [RelayCommand]
    public async Task CheckIn()
    {
        try
        {
            //TODO: Replace this with a better matching strategy. (e.g., Levenshtein or other)
            var lastName = IdCard.Name.Last.ToLower();
            var matches = localDataService.Find<CrewMember>(c => c.LastName.ToLower() == lastName);

            if (!matches.Any())
            {
                await Shell.Current.DisplayAlert("No matches found", $"No crew members found with the last name {IdCard.Name.Last}", "OK");
                //TODO: Implement manual add process.
                return;
            }

            var crewMember = matches.First(); //TODO: Select from instead of grabbing the first one

            if (IdCard.ExpirationDate <= DateTime.Now)
            {
                bool isOk = await Shell.Current.DisplayAlert("License Expired", "Do you agree to working without driving?", "Yes", "No");
                if (!isOk)
                {
                    //TODO: Display cancellation message.
                    return;
                }
            }


            crewMember.Registrations ??= new List<Registration>();

            var registration = crewMember.Registrations.FirstOrDefault(r => r.CheckOutTime == default);
            if (registration is null)
            {
                registration = new Registration
                {
                    CrewMemberId= crewMember.Id,
                    CheckInTime = DateTimeOffset.Now,
                    DocumentNumber = IdCard.IdNumber,
                    DrivingAllowed = DateTime.Now < IdCard.ExpirationDate
                };

                crewMember.Registrations.Add(registration);
                await Shell.Current.DisplayAlert("Check-In", "Success!", "OK");
            }
            else
            {
                registration.CheckOutTime = DateTimeOffset.Now;
                localDataService.Update(registration);

                await Shell.Current.DisplayAlert("Check-Out", "Success!", "OK");
            }

            localDataService.Update(crewMember);
        }
        finally
        {
            IdCard = null;
            IsScanning = true;
        }
    }

    partial void OnIsScanningChanged(bool value)
    {
        Debug.WriteLine($"IsScanning changed to {value}");
    }

}
