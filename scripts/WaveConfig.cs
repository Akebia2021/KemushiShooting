using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//新しいGameObjectの雛形を作成
[CreateAssetMenu(menuName = "Enemy wave Config")] 

//Scriptable Objectを継承する
public class WaveConfig : ScriptableObject
{
    //enemyPrefab = 敵オブジェクト、pathPrefab＝waypoints
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    
    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    
    //Pathの小要素からTransformを抽出してそのリストを返す関数。
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        //Path プレハブの小要素であるObjectのTransform(位置情報)をListに格納
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GetTBSpawn()
    {
        return timeBetweenSpawns;
    }
    public float GetRandomFactor()
    {
        return spawnRandomFactor;

    }
    public int GetNumberOfEnemy()
    {
        return numberOfEnemies;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;

    }


}
