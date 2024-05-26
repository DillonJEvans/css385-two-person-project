using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float movement_speed = 0.015f;
    public float life_limit;

    void Update()
    {
        transform.Translate(0, movement_speed, 0);
        if (Time.time > life_limit)
        {
            Destroy(gameObject);
        }
    }
}
