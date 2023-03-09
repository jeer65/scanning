using Com.Dynamsoft.Dbr;

namespace MauiAppScanning.Platforms.Android
{
	public class DBRLicenseVerificationListener : Java.Lang.Object, IDBRLicenseVerificationListener
	{
		public void DBRLicenseVerificationCallback(bool isSuccess, Java.Lang.Exception error)
		{
			if (!isSuccess)
			{
				System.Console.WriteLine(error.Message);
			}
		}
	}
}