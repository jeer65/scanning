namespace MauiAppScanning.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private RosterDatabase _rosterDatabase;

    ObservableCollection<Roster> rosters;

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
        Debug.WriteLine (items.Count);
    }
}
