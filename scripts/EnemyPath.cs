using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{

    WaveConfig waveConfig;

    List<Transform> waypoints;
    
    int wayPointIndex = 0;



    void Start()
    {
        //waypoints にはPathのTransformがList形式で入る
        waypoints = waveConfig.GetWaypoints();
        //最初のWaypointにこのスクリプトがアタッチされたObjectを配置
        transform.position = waypoints[wayPointIndex].transform.position;
    }

    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    //このスクリプトがアタッチされたGameObjectをwaypoints(Transformを格納した配列)に従って移動させる
    private void Move()
    {　
        //最後のWaypointに到達するまで移動を続ける
        if (wayPointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[wayPointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            //現在地、行き先、1フレームに動ける最大距離 を指定
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                movementThisFrame);

            //ターゲットに到達したら次のターゲットにIndexを増やす
            if (transform.position == targetPosition)
            { 
                wayPointIndex++;
            }

        }
        //最後のWaypointに到達したのでObjectを破棄
        else
        {
            Destroy(gameObject);
        }
    }
}
