namespace MauiAppScanning.ViewModels;

public partial class ListDetailViewModel : BaseViewModel
{
    private readonly LocalDataService localDataService;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    ObservableCollection<CrewMember> crewMembers;

    public ListDetailViewModel(LocalDataService localDataService)
    {
        this.localDataService = localDataService;
    }

    [RelayCommand]
    public void LoadFromLocal()
    {
        CrewMembers = new ObservableCollection<CrewMember>(localDataService.FindAll<CrewMember>());
    }

    [RelayCommand]
    public async Task LoadFromCloud()
    {
        await localDataService.SyncFromCloud();
        LoadFromLocal();
    }

    [RelayCommand]
    public async Task SendToCloud()
    {
        await localDataService.SyncToCloud();
    }

    [RelayCommand]
    public void DeleteRegistrations()
    {
        foreach (var member in CrewMembers)
        {
            member.Registrations?.Clear();
            localDataService.Update(member);
        }
        LoadFromLocal();
    }
}
