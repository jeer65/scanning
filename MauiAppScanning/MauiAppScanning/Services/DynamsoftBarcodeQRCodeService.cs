namespace MauiAppScanning.Services
{
	public partial class DynamsoftBarcodeQRCodeService
	{
		public partial void InitSDK(string license);
		public partial string DecodeFile(string filePath);
	}
}