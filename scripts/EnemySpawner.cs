using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());

        }
        while (looping);
        
    }

    //すべてのWaveをSpawn（内部で別のCoroutineを呼ぶ）
    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    //1Wave中ですべての敵をSpawn
    //この関数は単に指定した回数、指定したObjectのInstanceを作成するだけである。
    //Insatanceのその後の挙動には全くタッチしないのでInstanceの挙動自体はEnemyPathスクリプトが行う。
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) 
    {
        //waveconfigで設定した敵の数の回数文だけForループを回す
        for(int enemyCount = 0; enemyCount<waveConfig.GetNumberOfEnemy(); enemyCount++)
        {

            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTBSpawn());
        }
    }
}
