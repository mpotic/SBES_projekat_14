using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertificateMaker
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("CN: ");
				string cn = Console.ReadLine().Trim();
				Console.WriteLine("OU: ");
				string ou = Console.ReadLine().Trim();
				Console.WriteLine("filename: ");
				string filename = Console.ReadLine().Trim();
				Console.WriteLine("Input \"sign\" for signature sertificate, press enter for exchange certificate.");
				bool sign = Console.ReadLine().Trim().Equals("sign");

				Certificates.GenerateCertificate(cn, ou, filename, sign);

				Console.WriteLine("The password you used when creating the new certificate.");
				string password = Console.ReadLine();
				
				Certificates.GeneratePFX(filename, password);

				Console.WriteLine("Press 0 to exit the program or any other key to continue...");
				if (Console.ReadKey().KeyChar.ToString() == "0")
					break;
			}
		}
	}

	/// <summary>
	/// For methods in this class to work SmartCardsServiceCA.cer and SmartCardsService.pvk and makecert.exe and pvk2pfx.exe need to be in the Debug location
	/// of the project (.exe and those should be at the same location) and the project has to be started with admin priviledges.
	/// Also SmartCardsServiceCA.cer has to be installed in Trusted Root Certification Authorities.
	/// </summary>
	internal static class Certificates
	{
		/// <summary>
		/// Method used to generate .cer and .pvk files. CN is the subject name of the smart card user. OU is the organizational unit of the smart card user. 
		/// This information represents the CN and OU parameters of the certificate and those can later be found in CertName.cer -> Details -> Subject.
		/// You will be prompted for password of the subject for whom the certificate is being created and for the issuers password. Issuers password is 1234.
		/// </summary>
		/// <param name="CN"></param>
		/// <param name="OU"></param>
		/// <param name="filename"></param>
		/// <param name="sign"></param>
		/// <returns></returns>
		internal static bool GenerateCertificate(string CN, string OU, string filename = "", bool sign = false)
		{
			if (filename.Trim() == string.Empty)
				filename = CN;

			string command = $"-sv {filename}.pvk -iv SmartCardsServiceCA.pvk -n \"CN={CN},OU={OU}\" -pe -ic" +
				$" SmartCardsServiceCA.cer {filename}.cer -sr localmachine -ss My " +
				$"{(sign ? "-sky signature" : "-sky exchange")}";
			ProcessStartInfo info = new ProcessStartInfo("makecert.exe",
				$"-sv {filename}.pvk -iv SmartCardsServiceCA.pvk -n \"CN={CN},OU={OU}\" -pe -ic" +
				$" SmartCardsServiceCA.cer {filename}.cer -sr localmachine -ss My " +
				$"{(sign ? "-sky signature" : "-sky exchange")}")
			{
				RedirectStandardInput = true,
				UseShellExecute = false,
				CreateNoWindow = true,
				Verb = "runas"
			};

			Console.WriteLine($"-sv {filename}.pvk -iv SmartCardsServiceCA.pvk -n \"CN={CN},OU={OU}\" -pe -ic" +
				$" SmartCardsServiceCA.cer {filename}.cer -sr localmachine -ss My " +
				$"{(sign ? "-sky signature" : "-sky exchange")}\n");

			Process proc = Process.Start(info);

			proc.WaitForExit();

			return true;
		}

		/// <summary>
		/// Generates the pfx file and requires the password input which should be the same as the one used for .cer and .pvk creation.
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		internal static bool GeneratePFX(string filename, string password = "123")
		{
			ProcessStartInfo info = new ProcessStartInfo("pvk2pfx.exe",
				$"/pvk {filename}.pvk /pi {password} /spc {filename}.cer /pfx {filename}.pfx")
			{
				RedirectStandardInput = true,
				UseShellExecute = false,
				CreateNoWindow = true,
				Verb = "runas"
			};
			Console.WriteLine($"/pvk {filename}.pvk /pi {password} /spc {filename}.cer /pfx {filename}.pfx");

			Process proc = Process.Start(info);

			proc.WaitForExit();

			return true;
		}
	}
}
