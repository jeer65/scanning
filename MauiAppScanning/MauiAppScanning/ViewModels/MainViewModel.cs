using IdParser;


namespace MauiAppScanning.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private RosterDatabase _rosterDatabase;

    [ObservableProperty]
    private ObservableCollection<Roster> rosters = new();

    [ObservableProperty]
    private string barcodeData;

    [ObservableProperty]
    IdentificationCard idCard;

    public MainViewModel(RosterDatabase rosterDatabase)
    {
        _rosterDatabase = rosterDatabase;
    }

    [RelayCommand]
    public void ParseBarcode()
    {        
        
        if (!string.IsNullOrWhiteSpace(BarcodeData))
        {
            IdCard = Barcode.Parse(BarcodeData);
        }
    }

    [RelayCommand]
    public async Task LoadFile()
    {
        var items = await _rosterDatabase.GetItemsAsync();
        Rosters = new ObservableCollection<Roster>(items);        
    }
}
