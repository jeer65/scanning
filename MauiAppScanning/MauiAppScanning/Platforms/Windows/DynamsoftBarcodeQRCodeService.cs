using Dynamsoft;

namespace MauiAppScanning.Services
{
	public partial class DynamsoftBarcodeQRCodeService
	{
		BarcodeQRCodeReader reader = null;

		public partial void InitSDK(string license)
		{
			BarcodeQRCodeReader.InitLicense(license);

			try
			{
				reader = BarcodeQRCodeReader.Create();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public partial string DecodeFile(string filePath)
		{
			if (reader == null)
				return "";

			string decodingResult = "";
			try
			{
				var results = reader.DecodeFile(filePath);
				if (results != null)
				{
					foreach (var result in results)
					{
						decodingResult += result + "\n";
					}
				}
				else
				{
					decodingResult = "No barcode found.";
				}
			}
			catch (Exception e)
			{
				decodingResult = e.Message;
			}

			return decodingResult;
		}
	}
}