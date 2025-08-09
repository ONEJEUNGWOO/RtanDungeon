using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnController : MonoBehaviour , IInteractable

{
    [Header("Spawn NPC")]
    public GameObject prefab; //프리팹
    public float spawnRate;
    public float lastSpawnTime = 0f;
    public int spawnCount;
    public Vector3 spawnPosition;

    private bool isSpawn = true;
    private float timeRemaining;

    private void Update()
    {
        isSpawn = timeRemaining <= 0;
        timeRemaining = Mathf.Max(0, spawnRate + lastSpawnTime - Time.time );
    }

    public void SpawnNPC()
    {
        //if (lastSpawnTime == 0f)        // TODO 맨 처음 실행 할 때 if문 없이 쓸 방법 생각해보기 그냥 처음에 소환이 안되면 될 것 같음
        //{
        //    lastSpawnTime = Time.time;

        //    for (int i = 0; i < spawnCount; i++) // 스폰카운트 만큼 반복 생성
        //    {
        //        Instantiate(prefab, spawnPosition, Quaternion.identity).GetComponent<NPC>();
        //    }
        //}
            
        if (Time.time - lastSpawnTime < spawnRate)  //if문 순회를 줄이기 위해 리턴을 시키는 조건을 달아줌
            return;

        lastSpawnTime = Time.time;

        for (int i = 0; i < spawnCount; i++) // 스폰카운트 만큼 반복 생성
        {
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }

    public string GetInteractPrompt()   //인터페이스 받아와서 텍스트 설정 해 주기
    {
        string str = $"스폰 오브젝트 : {prefab.name}\n 스폰 가능 여부 : {isSpawn}\n 스폰까지 남은 시간 : {timeRemaining:n0}초\n 스폰 하시려면 오른쪽 마우스 버튼을 누르세요.";
        return str;
    }

    public void OnInteract()
    {
        return;
    }
}
