using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] private float nextStageDelay;
    [SerializeField] private int currentStage;
    private int maxStage = 5;

    private List<Spawn> spawnList;
    private int spawnIndex;
    private bool spawnEnd;

    private void Awake()
    {
        spawnList = new List<Spawn>();
        ReadSpawnFile(currentStage);
    }
    private void Start()
    {
        Invoke(nameof(SpawnEnemy), spawnList[spawnIndex].delay);
    }
    private void ReadSpawnFile(int stage)
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load($"Stage{stage}") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();

            if (line == null) break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }
        Debug.Log($"[Stage] 로드 완료: {stage}");
    }
    private void SpawnEnemy()
    {
        if(spawnEnd) return;

        Spawn spawnData = spawnList[spawnIndex];

        int enemyIndex = 0;

        switch(spawnData.type)
        {
            case "A":
                enemyIndex = 0;
                break;
            case "B":
                enemyIndex = 1;
                break;
            case "C":
                enemyIndex = 2;
                break;
            case "D":
                enemyIndex = 3;
                break;

        }

        Instantiate(
            enemyPrefabs[enemyIndex],
            spawnPoints[spawnData.point].position,
            Quaternion.identity);

        spawnIndex++;

        if(spawnIndex >= spawnList.Count)
        {
            spawnEnd = true;
            Invoke(nameof(LoadNextStage), nextStageDelay);
            return;
        }

        Invoke(nameof(SpawnEnemy), spawnList[spawnIndex].delay);
    }
    private void LoadNextStage()
    {
        currentStage++;

        if (currentStage > maxStage)
        {
            Debug.Log("모든 스테이지 종료");
            return;
        }

        ReadSpawnFile(currentStage);
        Invoke(nameof(SpawnEnemy), spawnList[spawnIndex].delay);
    }
}
