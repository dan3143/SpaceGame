using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class Planet : MonoBehaviour
{
    [SerializeField] 
    Light2D light2d;

    [SerializeField]
    EventTrigger.TriggerEvent planetClick;

    void Start()
    {
        light2d.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Input.GetButtonDown("Submit"))
        {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            eventData.selectedObject = this.gameObject;
            planetClick.Invoke(eventData);
        }   
        light2d.enabled = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        light2d.enabled = false;        
    }

}
