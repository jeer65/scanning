using Com.Dynamsoft.Dbr;
using MauiAppScanning.Platforms.Android;

namespace MauiAppScanning.Services
{
	public partial class DynamsoftBarcodeQRCodeService
	{
		BarcodeReader reader;

		public partial void InitSDK(string license)
		{
			BarcodeReader.InitLicense(license, new DBRLicenseVerificationListener());
			reader = new BarcodeReader();
		}

		public partial string DecodeFile(string filePath)
		{
			string decodingResult = "";

			try
			{

				TextResult[] results = reader.DecodeFile(filePath);
				if (results != null)
				{
					foreach (TextResult result in results)
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