using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private bool followX;
    [SerializeField] private bool followY;
    [SerializeField] private bool applyFloor;
    
    void Update()
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 position = transform.position;

        if (followX) position.x = applyFloor ? Mathf.Floor(playerPos.x) : playerPos.x;
        if (followY) position.y = applyFloor ? Mathf.Floor(playerPos.y) : playerPos.y;
        
        transform.position = position;
    }
}
