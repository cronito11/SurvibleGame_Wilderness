using System.IO;
using UnityEngine;

namespace Surviblewilderness
{
    public class SaveSystem : MonoBehaviour
    {
        const string SAVE_FILE_LOCATION = "F:/Web game programming/SurvibleGame_Wilderness/Assets/_Project/Save Files";
        public static void Save<T>(T data, string fileName)
        {
            string path = Application.persistentDataPath + "/" + fileName;
            string json = JsonUtility.ToJson(data, true);
            Debug.Log(json);
            File.WriteAllText(path, json);
            Debug.Log($"Saved {fileName} at: " + path);
        }

        public static T Load<T>(string fileName)
        {
            string path = Application.persistentDataPath + "/" + fileName;

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                T data = JsonUtility.FromJson<T>(json);
                Debug.Log($"Loaded {fileName} from: " + path);
                return data;
            }
            else
            {
                Debug.LogWarning($"File not found: {fileName}");
                return default(T);
            }
        }
    }
}
