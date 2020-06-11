using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    void Start()
    {
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
        
    }

    void PlayAgain()
    {
        currentButton = 0;
        GenerateSequence();
        PlaySequence();
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

    IEnumerator Wait(int n)
    {
        yield return new WaitForSeconds(n);
    }

    void GenerateSequence() 
    {
        currentButton = 0;
        int rand = Random.Range(0, buttonIds.Length);
        sequence.Add(rand);
    }

    IEnumerator _PlaySequence()
    {
        ToggleButtons();
        GameObject selector;
        foreach(int button in sequence) {
            selector = buttons[button].transform.GetChild(0).gameObject;
            selector.SetActive(true);
            yield return new WaitForSeconds(delay);
            selector.SetActive(false);
            yield return new WaitForSeconds(delay);
        }
        ToggleButtons();
    }

    void PlaySequence()
    {
        StartCoroutine(_PlaySequence());
    }

    void ToggleButtons() {
        foreach(Button btn in buttons) {
            btn.interactable = !btn.interactable;
        }
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
