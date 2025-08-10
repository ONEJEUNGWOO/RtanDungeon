using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour   //캠프파이어 근처에 가면 데미지를 주게 하는 클래스
{
    public int damage;
    public float damageRate;

    List<IDamage> things = new List<IDamage>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage() //추가한 컴퍼넌트를 통해 함수를 실행
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)  //충돌이 있었을 때 IDamage컴퍼넌트가 있다면 리스트에 추가 해주는 함수
    {
        if (other.TryGetComponent(out IDamage damage))
        {
            things.Add(damage);
        }
    }

    private void OnTriggerExit(Collider other)  //충돌이 끝날 때 Idamage컴퍼넌트가 있다면 리스트에서 삭제 해주는 함수
    {
        if (other.TryGetComponent(out IDamage damage))
        {
            things.Remove(damage);
        }
    }
}
