using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveStatus
{
    public int points;
    public SaveStatus(int points) 
    {
        this.points = points;
    }

    public static void Save(int points)
    {
        string filename = Application.persistentDataPath + "/gamesave.save";
        SaveStatus save = new SaveStatus(points);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filename);
        bf.Serialize(file, save);
        file.Close();
    }

    public static int Load()
    {
        int points = 0;
        string filename = Application.persistentDataPath + "/gamesave.save";
        if (File.Exists(filename)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename, FileMode.Open);
            SaveStatus save = (SaveStatus) bf.Deserialize(file);
            file.Close();
            points = save.points;
        }
        return points;
    }
}
