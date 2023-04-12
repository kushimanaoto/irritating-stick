using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_PingPong2 : MonoBehaviour
{
    private Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(pos.x, pos.y , pos.z + Mathf.PingPong(Time.time / 3, 1.0f));
    }
}