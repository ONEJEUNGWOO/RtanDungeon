using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnController : MonoBehaviour
{
    [Header("Spawn NPC")]
    public GameObject prefab; //프리팹 위치 수
    public float spawnRate;
    public float lastSpawnTime = 0f;
    public int spawnCount;
    public Vector3 spawnPosition;

    public void SpawnNPC()
    {
        if (lastSpawnTime == 0f)
        {
            lastSpawnTime = Time.time;

            for (int i = 0; i < spawnCount; i++) // 스폰카운트 만큼 반복 생성
            {
                Instantiate(prefab, spawnPosition, Quaternion.identity).GetComponent<NPC>();
            }
        }
            
        if (Time.time - lastSpawnTime < spawnRate)
            return;

        lastSpawnTime = Time.time;

        for (int i = 0; i < spawnCount; i++) // 스폰카운트 만큼 반복 생성
        {
            Instantiate(prefab, spawnPosition, Quaternion.identity).GetComponent<NPC>();
        }
    }
}
