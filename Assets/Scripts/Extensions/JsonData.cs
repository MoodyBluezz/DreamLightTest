using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class JsonData
{
	public static void SaveData(List<TileData> data, string fileName)
	{
		try
		{
			string json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
			File.WriteAllText(fileName, json);
			Console.WriteLine($"JSON data saved to: {fileName}");
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error saving JSON data: {e.Message}");
		}
	}

	public static List<TileData> LoadData(string fileName)
	{
		List<TileData> loadedData = new List<TileData>();

		try
		{
			if (File.Exists(fileName))
			{
				string json = File.ReadAllText(fileName);
				loadedData = JsonConvert.DeserializeObject<List<TileData>>(json);
				Console.WriteLine($"JSON data loaded from: {fileName}");
			}
			else
			{
				Console.WriteLine($"File not found: {fileName}");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error loading JSON data: {e.Message}");
		}

		return loadedData;
	}
}