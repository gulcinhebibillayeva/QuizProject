using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace QuizProject.Helpers;

public static class JsonHelper
{
    public static List<T> ReadFromJson<T>(string path)
    {
        try
        {
            if (!File.Exists(path))
            {
                return new List<T>();
            }
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON oxunarkən xəta: {ex.Message}");
            return new List<T>();
        }
    }

    public static void WriteToJson<T>(string path, List<T> data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
    }

    public static void AppendToJson<T>(string path, T newItem)
    {
        var list = ReadFromJson<T>(path);
        list.Add(newItem);
        WriteToJson(path, list);
    }

}

