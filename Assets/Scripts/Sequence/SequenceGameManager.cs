using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SequenceGameManager : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject game;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int[] buttonIds;
    private List<int> sequence = new List<int>();
    private int sequenceLength = 1;
    private int currentButton = 0;
    private int score = 0;
    private int bonus = 0;
    private int globalScore;
    private BluetoothService bt;
    private int x, y;
    private int columns = 2;
    private int rows = 2;
    
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android) {
            ToggleButton(buttons[0]);
            bt = BluetoothService.Instance;
            x = y = 0;
        }
        
        globalScore = SaveStatus.Load();
        buttonIds = new int[buttons.Length];
        for (int i = 0; i < buttons.Length; i++) {
            int aux = i;
            buttons[i].onClick.AddListener(() => ButtonPressed(aux));
            buttonIds[i] = aux;
        }
        GenerateSequence();
        PlaySequence();
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) {
            if (bt.IsButtonClicked("Left")) Move("left");
            if (bt.IsButtonClicked("Right")) Move("right");
            if (bt.IsButtonClicked("Down")) Move("down");
            if (bt.IsButtonClicked("Up")) Move("up");
            
            if (bt.IsButtonClicked("A")) {
                if (menu.activeSelf) {
                    Again();
                } else {
                    ButtonPressed(GetIndex(x, y));
                }
            }

            if (bt.IsButtonClicked("B")) {
                if (menu.activeSelf) {
                    ReturnSpace();
                } else {
                    ButtonPressed(GetIndex(x, y));
                }
            }
        }
    }

    void Move(string message)
    {
        ToggleButton(GetButton(x, y));
        if (message == "up" && y > 0) {
            y -= 1;
        } else if (message == "down" && y < rows-1) {
            y += 1;
        }
        if (message == "left" && x > 0) {
            x -= 1;
        } else if (message == "right" && x < columns-1) {
            x += 1;
        }
        ToggleButton(GetButton(x, y));
    }

    Button GetButton(int i, int j) {
        return buttons[j*columns + i];
    }

    int GetIndex(int i, int j) {
        return j*columns+i;
    }

    void ButtonPressed(int id)
    {
        if (sequence[currentButton] == id) {
            currentButton++;
            score += 1 + bonus;
            scoreText.text = "SCORE: " + score;
            if (currentButton == sequence.Count) {
                if (Random.Range(0,1) == 1) {
                    bonus++;
                }
                sequenceLength++;
                PlayAgain();
            }
        } else { // LOSE
            menu.SetActive(true);
            game.SetActive(false);
            sequence.Clear();
            sequenceLength = 0;
        }
    }

    void PlayAgain()
    {
        currentButton = 0;
        GenerateSequence();
        PlaySequence();
    }

    void GenerateSequence() 
    {
        currentButton = 0;
        int rand = Random.Range(0, buttonIds.Length);
        sequence.Add(rand);
    }

    IEnumerator _PlaySequence()
    {
        ToggleButtonsClickable();
        GameObject selector;
        foreach(int button in sequence) {
            selector = buttons[button].transform.GetChild(0).gameObject;
            selector.SetActive(true);
            yield return new WaitForSeconds(delay);
            selector.SetActive(false);
            yield return new WaitForSeconds(delay);
        }
        ToggleButtonsClickable();
    }

    void PlaySequence()
    {
        StartCoroutine(_PlaySequence());
    }

    void ToggleButtonsClickable() {
        foreach(Button btn in buttons) {
            btn.interactable = !btn.interactable;
        }
    }

    void ToggleButton(Button btn) {
        GameObject selector = btn.transform.GetChild(1).gameObject;
        selector.SetActive(!selector.activeSelf);
    }

    public void ReturnSpace()
    {
        globalScore += score;
        SaveStatus.Save(globalScore);
        SceneManager.LoadScene("Space");
    }

    public void Again()
    {
        menu.SetActive(false);
        game.SetActive(true);
        sequenceLength = 1;
        bonus = 0;
        PlayAgain();
    }
}
