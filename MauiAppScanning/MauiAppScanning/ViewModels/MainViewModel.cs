using IdParser;

namespace MauiAppScanning.ViewModels;

public partial class MainViewModel : BaseViewModel
{
	private DynamsoftBarcodeQRCodeService _barcodeQRCodeService;
	private RosterDatabase _rosterDatabase;
	private IMediaPicker _mediaPicker;

	[ObservableProperty]
	private ObservableCollection<Roster> rosters = new();

	[ObservableProperty]
	private string barcodeData;

	[ObservableProperty]
	IdentificationCard idCard;

	public MainViewModel(RosterDatabase rosterDatabase, IMediaPicker mediaPicker)
	{
		_rosterDatabase = rosterDatabase;

		_mediaPicker = mediaPicker;

		InitService();
	}

	[RelayCommand]
	public async void ParseBarcode()
	{
		try
		{
			if (_mediaPicker.IsCaptureSupported)
			{
				var photo = await _mediaPicker.CapturePhotoAsync();

				if (photo == null) return;

				// save the file into local storage
				var localFolder = FileSystem.AppDataDirectory;
				var localFilePath = Path.Combine(localFolder, photo.FileName);
#if WINDOWS
				// on Windows file.OpenReadAsync() throws an exception
				using Stream sourceStream = File.OpenRead(photo.FullPath);
#else
				using Stream sourceStream = await photo.OpenReadAsync();
#endif
				using FileStream localFileStream = File.OpenWrite(localFilePath);

				await sourceStream.CopyToAsync(localFileStream);

				var result = _barcodeQRCodeService.DecodeFile(localFilePath);

				if (!string.IsNullOrWhiteSpace(result) && !result.Equals("No barcode found.", StringComparison.OrdinalIgnoreCase))
				{
					IdCard = Barcode.Parse(result);
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	[RelayCommand]
	public async Task LoadFile()
	{
		var items = await _rosterDatabase.GetItemsAsync();
		Rosters = new ObservableCollection<Roster>(items);
	}

	private async void InitService()
	{
		await Task.Run(() =>
		{
			_barcodeQRCodeService = new DynamsoftBarcodeQRCodeService();

			try
			{
				_barcodeQRCodeService.InitSDK("DLS2eyJoYW5kc2hha2VDb2RlIjoiMTAxNzM4OTgwLVRYbE5iMkpwYkdWUWNtOXFYMlJpY2ciLCJtYWluU2VydmVyVVJMIjoiaHR0cHM6Ly9tbHRzLmR5bmFtc29mdC5jb20iLCJvcmdhbml6YXRpb25JRCI6IjEwMTczODk4MCIsImNoZWNrQ29kZSI6LTYyNTkwNDA2OX0=");
			}
			catch (Exception ex)
			{
				System.Console.WriteLine($"Error: {ex.Message}");
			}

			return Task.CompletedTask;
		});
	}
}