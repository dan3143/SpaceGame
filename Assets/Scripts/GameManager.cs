using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Light2D btLight;
    bool isConnected = false;
    public int points = 0;
    
    public void GoToMemoryPlanet()
    {
        Debug.Log("Loading memory planet...");
    }

    public void ActivateBluetooth()
    {
        if (Application.platform == RuntimePlatform.Android) {
            Debug.Log("Activating Bluetooth");
        } else {
            Debug.Log("Wait this is not Android");
        }
    }

    public void Save()
    {
        string filename = Application.persistentDataPath + "/gamesave.save";
        SaveStatus save = new SaveStatus(points);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filename);
        bf.Serialize(file, save);
        file.Close();
    }

    public void Load()
    {
        string filename = Application.persistentDataPath + "/gamesave.save";
        if (File.Exists(filename)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filename, FileMode.Open);
            SaveStatus save = (SaveStatus) bf.Deserialize(file);
            file.Close();
            points = save.points;
        }
    }

    void Update()
    {
        if (isConnected) {
            btLight.color = Color.cyan;
            btLight.enabled = true;
        } else {
            btLight.color = Color.white;
            btLight.enabled = false;
        }
    }

    void Message(string message)
    {
        if (message == "bluetooth.connected") {
            isConnected = true;
        } else if (message == "bluetooth.disconnected") {
            isConnected = false;
        }
    }
}
