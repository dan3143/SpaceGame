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
    bool isConnected = false;

    void Awake()
    {
        score = SaveStatus.Load();
    }

    void Start()
    {        
        scoreTxt.text = "x" + score;
        Debug.Log("Score:" + score);
    }

    void Update()
    {
        if (isConnected) {
            btLight.color = Color.blue;
        } else {
            btLight.color = Color.white;
        }
        scoreTxt.text = "x" + score;
    }

    void Message(string message)
    {
        if (message == "bluetooth.connected") {
            isConnected = true;
        } else if (message == "bluetooth.disconnected") {
            isConnected = false;
        }
    }

    public void GoToMemoryPlanet()
    {
        SceneManager.LoadScene("CardGame");
    }

    public void ActivateBluetooth()
    {
        if (Application.platform == RuntimePlatform.Android) {
            Debug.Log("Activating Bluetooth");
        } else {
            Debug.Log("Wait this is not Android");
        }
    }
}
