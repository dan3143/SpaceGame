using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Range {
    public float min;
    public float max;
}

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Range limitX;
    [SerializeField] Range limitY;
    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);    
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, limitX.min, limitX.max),
            Mathf.Clamp(transform.position.y, limitY.min, limitY.max),
            transform.position.z);
    }
}
