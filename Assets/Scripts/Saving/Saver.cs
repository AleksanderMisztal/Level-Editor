using System.IO;
using UnityEngine;

namespace Saving
{
    public static class Saver
    {
        private const string FileExtension = ".txt";
        private static readonly string savePath = Application.dataPath + "/Saves/";

        static Saver()
        {
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);
        }

        public static void Save(string fileName, object obj)
        {
            string data = JsonUtility.ToJson(obj);
            string saveName = savePath + fileName + FileExtension;
            File.WriteAllText(saveName, data);
        }

        public static T Read<T>(string fileName)
        {
            string saveName = savePath + fileName + FileExtension;
            string data = File.ReadAllText(saveName);
            T read = JsonUtility.FromJson<T>(data);
            return read;
        }
    }
}