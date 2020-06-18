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
    bool selected = false;
    BluetoothControl btControl;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
            btControl = BluetoothControl.Instance;
        light2d.enabled = false;        
    }

    public void Update()
    {
        if (selected)
        {
            bool click = false;
            if (Application.platform == RuntimePlatform.Android) {
                click = btControl.IsButtonClicked("A");
            } else {
                click = Input.GetButton("A");
            }
            
            if (click) {
                Exec();
            }
        }
    }

    void Exec()
    {
        if (selected)
        {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            eventData.selectedObject = this.gameObject;
            planetClick.Invoke(eventData);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        selected = true;
        light2d.enabled = selected;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        SetSelected(false);
    }

    void OnMouseEnter()
    {
        SetSelected(true);
    }

    void OnMouseExit()
    {
        SetSelected(false);
    }

    
    void OnMouseDown()
    {
        Exec();
    }

    void SetSelected(bool selected) {
        this.selected = selected;
        this.light2d.enabled = selected;
    }

}
