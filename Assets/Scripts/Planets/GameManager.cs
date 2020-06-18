using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Light2D btLight;
    [SerializeField] TextMeshProUGUI scoreTxt;
    int score;
    BluetoothControl btService;

    void Awake()
    {
        score = SaveStatus.Load();
    }

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android) {
            BluetoothControl.Server.ServerObject = this.gameObject.name;
        }
        btLight.enabled = false;
        scoreTxt.text = "x" + score;
        if (!Bluetooth.IsEnabled) {
            BluetoothControl.Server.RequestEnableBluetooth();
        }
    }

    void Update()
    {
        scoreTxt.text = "x" + score;
        if (Application.platform == RuntimePlatform.Android){
            btLight.enabled = Bluetooth.IsEnabled;
        }
    }

    void OnMessage(string message)
    {  
        Debug.Log("Server: " + message); 
        if (message == "bluetooth.connected") {
            btLight.color = Color.blue;
        }
        if (message == "bluetooth.on") {
            BluetoothControl.Server.Start();
        } 
        if (message == "server.listening") {
            btLight.color = Color.white;
        }
    }

    public void GoToMemoryPlanet()
    {
        SceneManager.LoadScene("CardGame");
    }

    public void GoToSequencePlanet()
    {
        SceneManager.LoadScene("SequenceGame");
    }

    public void SaveQuit()
    {
        SaveStatus.Save(score);
        Application.Quit();
    }

}
