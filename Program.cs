using System;
using System.IO;
using System.Text;
using Unidecode.NET;

namespace UnicodeToAscii;

internal static class Program
{
	private static void Main(string[] args)
	{
		try
		{
			string? fileName, cleanFileName;

			if (args.Length == 0)
			{
				Console.Write("File name: ");
				fileName = Console.ReadLine();
			}
			else fileName = args[0];

			if (string.IsNullOrWhiteSpace(fileName))
			{
				Console.WriteLine("No file name provided.");

				if (args.Length == 0)
					Console.ReadKey(true);
				return;
			}

			if (!File.Exists(fileName))
			{
				Console.WriteLine("File does not exist.");

				if (args.Length == 0)
					Console.ReadKey(true);
				return;
			}

			var index = fileName.LastIndexOf('.');
			if (index != -1)
				cleanFileName = $"{fileName[..index]}-clean.{fileName[(index + 1)..]}";
			else cleanFileName = fileName + "-clean";

			if (File.Exists(cleanFileName))
			{
				Console.WriteLine($"File {cleanFileName} already exists!");

				if (args.Length == 0)
					Console.ReadKey(true);
				return;
			}

			Console.WriteLine("Reading file...");
			var content = File.ReadAllText(fileName);

			Console.WriteLine("Writing file...");
			File.WriteAllText(cleanFileName, content.Unidecode(), Encoding.UTF8);

			Console.WriteLine("--- DONE ---");

			if (args.Length == 0)
				Console.ReadKey(true);
		}
		catch (Exception e)
		{
			Console.WriteLine("Error! Exception has occured!");
			Console.WriteLine(e.ToString());
			
			if (args.Length == 0)
				Console.ReadKey(true);
		}
	}
}