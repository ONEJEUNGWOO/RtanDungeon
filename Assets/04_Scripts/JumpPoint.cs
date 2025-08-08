using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    public int JumpPower;
    private Rigidbody _rigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }
}
