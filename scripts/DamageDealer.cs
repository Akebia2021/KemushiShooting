using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//このスクリプトは弾などにアタッチする
public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 100;



    public int GetDamage()
    {
        return damage;
    }
    
    public void Hit()
    {
        Destroy(gameObject);
    }

}

