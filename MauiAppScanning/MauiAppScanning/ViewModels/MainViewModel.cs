namespace MauiAppScanning.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private RosterDatabase _rosterDatabase;

    [ObservableProperty]
    public ObservableCollection<Roster> rosters = new();

    [ObservableProperty]
    Roster selectedRoster;

    public MainViewModel(RosterDatabase rosterDatabase)
    {
        _rosterDatabase = rosterDatabase;
    }


    [RelayCommand]
    public async Task LoadFile()
    {
        var items = await _rosterDatabase.GetItemsAsync();
        Rosters = new ObservableCollection<Roster>(items);        
    }
}
