using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Planet : MonoBehaviour
{
    [SerializeField] 
    TextMeshProUGUI text;

    [SerializeField]
    EventTrigger.TriggerEvent planetClick;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject + " entered");
        text.enabled = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log(col.gameObject + " exited");
        text.enabled = false;
    }

}
