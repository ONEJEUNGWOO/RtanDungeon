using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    public int JumpPower;
    private Rigidbody rg;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        rg = other.GetComponent<Rigidbody>();
        var t = rg.velocity;        //�ι� ° ƨ�� ��� y���� -������ ���� �Ź� ���� ���̰� �޶����� 0���� �ʱ�ȭ ���ִ� �۾�
        t.y = 0;
        rg.velocity = t;
        rg.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }
}
