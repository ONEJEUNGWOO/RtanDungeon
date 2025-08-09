using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnController : MonoBehaviour
{
    [Header("Spawn NPC")]
    public GameObject prefab; //������
    public float spawnRate;
    public float lastSpawnTime = 0f;
    public int spawnCount;
    public Vector3 spawnPosition;

    public void SpawnNPC()
    {
        if (lastSpawnTime == 0f)        // TODO �� ó�� ���� �� �� if�� ���� �� ��� �����غ���
        {
            lastSpawnTime = Time.time;

            for (int i = 0; i < spawnCount; i++) // ����ī��Ʈ ��ŭ �ݺ� ����
            {
                Instantiate(prefab, spawnPosition, Quaternion.identity).GetComponent<NPC>();
            }
        }
            
        if (Time.time - lastSpawnTime < spawnRate)
            return;

        lastSpawnTime = Time.time;

        for (int i = 0; i < spawnCount; i++) // ����ī��Ʈ ��ŭ �ݺ� ����
        {
            Instantiate(prefab, spawnPosition, Quaternion.identity).GetComponent<NPC>();
        }
    }
}
