using Foundation;
using DBRiOS;

namespace MauiAppScanning.Services
{
	public class Listener : DBRLicenseVerificationListener
	{
		public void DBRLicenseVerificationCallback(bool isSuccess, NSError error)
		{
			if (error != null)
			{
				System.Console.WriteLine(error.UserInfo);
			}
		}
	}

	public partial class DynamsoftBarcodeQRCodeService
	{
		DynamsoftBarcodeReader reader;

		public partial void InitSDK(string license)
		{
			DynamsoftBarcodeReader.InitLicense(license, new Listener());

			reader = new DynamsoftBarcodeReader();
		}

		public partial string DecodeFile(string filePath)
		{
			NSError error = null;
			string decodingResult = "";

			try
			{

				var results = reader.DecodeFileWithName(filePath, out error);
				if (results != null)
				{
					foreach (var result in results)
					{
						decodingResult += result.BarcodeText + "\n";
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