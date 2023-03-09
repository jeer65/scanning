namespace MauiAppScanning.Services
{
	// CustomMediaPicker is a custom MediaPicker that inherits from Microsoft.MAUI.Media.MediaPicker
	// on the Android and iOS platforms, but provides a custom implementation on Windows to overcome
	// a bug with the MediaPicker in MAUI/WinUI... see https://github.com/dotnet/maui/issues/7660

	public partial class CustomMediaPicker : IMediaPicker
	{
		public bool IsCaptureSupported => MediaPicker.Default.IsCaptureSupported;

		public partial Task<FileResult> CapturePhotoAsync(MediaPickerOptions options = null);

		public partial Task<FileResult> CaptureVideoAsync(MediaPickerOptions options = null);

		public Task<FileResult> PickPhotoAsync(MediaPickerOptions options = null) => MediaPicker.Default.PickPhotoAsync(options);

		public Task<FileResult> PickVideoAsync(MediaPickerOptions options = null) => MediaPicker.Default.PickVideoAsync(options);
	}
}