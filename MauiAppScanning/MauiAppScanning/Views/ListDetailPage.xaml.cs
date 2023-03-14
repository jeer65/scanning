namespace MauiAppScanning.Views;

public partial class ListDetailPage : ContentPage
{
	ListDetailViewModel ViewModel;

	public ListDetailPage(ListDetailViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = ViewModel = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}
