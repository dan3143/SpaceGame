using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Light2D btLight;
    [SerializeField]
    TextMeshProUGUI scoreTxt;
    int score;
    BluetoothService btService;

    void Awake()
    {
        score = SaveStatus.Load();
    }

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android) {
            BluetoothService.Bluetooth.ServerObject = "GameManager";
        }
        btLight.enabled = false;
        scoreTxt.text = "x" + score;
    }

    void Update()
    {
        scoreTxt.text = "x" + score;
        if (Application.platform == RuntimePlatform.Android)
            btLight.enabled = BluetoothService.Bluetooth.IsEnabled;
        
    }

    void Message(string message)
    {  
        Debug.Log("Server: " + message); 
        if (message == "bluetooth.connected") {
            btLight.color = Color.blue;
        }
        if (message == "bluetooth.on") {
            BluetoothService.Bluetooth.Start();
        } 
        if (message == "server.listening") {
            btLight.color = Color.white;
        } 
    }

    public void GoToMemoryPlanet()
    {
        SceneManager.LoadScene("CardGame");
    }

    public void SaveQuit()
    {
        SaveStatus.Save(score);
        Application.Quit();
    }

}
