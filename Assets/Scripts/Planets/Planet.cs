﻿using System.Collections;
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
    bool selected = false;
    BluetoothService bluetoothService;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            bluetoothService = GameObject.FindGameObjectWithTag("Bluetooth").GetComponent<BluetoothService>();
        light2d.enabled = false;
    }

    public void Update()
    {
        if (selected)
        {
            bool click = false;
            if (Application.platform == RuntimePlatform.Android) {
                click = bluetoothService.IsButtonClicked("A");
            } else {
                click = Input.GetButton("A");
            }
            
            if (click) {
                Debug.Log("A click happened");
                BaseEventData eventData = new BaseEventData(EventSystem.current);
                eventData.selectedObject = this.gameObject;
                planetClick.Invoke(eventData);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        selected = true;
        light2d.enabled = selected;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        selected = false;
        light2d.enabled = selected;        
    }

}
