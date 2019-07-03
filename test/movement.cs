using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public GameObject testObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(routine1());
        }              
    }
    


    IEnumerator routine1()
    {
        for (int i=0; i < 5; i++){
            Instantiate(testObject, new Vector2(0, 0), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
        
    }


  


    //private void Move()
    //{
    //    //最後のWaypointに到達するまで移動を続ける
    //    if (pointIndex < points.Length)
    //    {
    //        var targetPosition = points[pointIndex].transform.position;
    //        var movementThisFrame = 2f * Time.deltaTime;
    //        //現在地、行き先、1フレームに動ける最大距離 を指定
    //        transform.position = Vector2.MoveTowards(
    //            transform.position,
    //            targetPosition,
    //            movementThisFrame);

    //        //ターゲットに到達したら次のターゲットにIndexを増やす
    //        if (transform.position == targetPosition)
    //        {
    //            pointIndex++;
    //        }

    //    }
    //    //最後のWaypointに到達したのでObjectを破棄
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
