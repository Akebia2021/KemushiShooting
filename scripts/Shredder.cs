﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shredder : MonoBehaviour
{
    //OntriggerとOncollisionは違うので注意
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
