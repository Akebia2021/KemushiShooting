﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paths : MonoBehaviour
{
    public Transform[] waypoints;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, 1f) * Time.deltaTime);
    }
}
