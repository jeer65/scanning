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
        BarcodeData = "@\n\rANSI 636032030102DL00410218ZM02590027DLDCA\nDCB\nDCD\nDBA12122023\nDCSHARRIS\nDCTCHARLES STACY\nDBD11212019\nDBB12121963\nDBC1\nDAY\nDAU\nDAG2051 HOLLOW OAK DR\nDAIANN ARBOR\nDAJMI\nDAK481038402  \nDAQH 620 115 777 944\nDCF\nDCG\nDCH\nDAH\nDCKH620115777944196312122023\nDDAF\n\rZMZMARev 01-21-2011\nZMB01\n\r";
        
        if (!string.IsNullOrWhiteSpace( BarcodeData))
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
