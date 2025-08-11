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
        var t = rg.velocity;        //두번 째 튕길 경우 y값이 -값으로 나와 매번 점프 높이가 달라져서 0으로 초기화 해주는 작업
        t.y = 0;
        rg.velocity = t;
        rg.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }
}
