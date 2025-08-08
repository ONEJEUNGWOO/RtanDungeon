using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour   //ķ�����̾� ��ó�� ���� �������� �ְ� �ϴ� Ŭ����
{
    public int damage;
    public float damageRate;

    List<IDamage> things = new List<IDamage>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage() //�߰��� ���۳�Ʈ�� ���� �Լ��� ����
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)  //�浹�� �־��� �� IDamage���۳�Ʈ�� �ִٸ� ����Ʈ�� �߰� ���ִ� �Լ�
    {
        if (other.TryGetComponent(out IDamage damage))
        {
            things.Add(damage);
        }
    }

    private void OnTriggerExit(Collider other)  //�浹�� ���� �� Idamage���۳�Ʈ�� �ִٸ� ����Ʈ���� ���� ���ִ� �Լ�
    {
        if (other.TryGetComponent(out IDamage damage))
        {
            things.Remove(damage);
        }
    }
}
