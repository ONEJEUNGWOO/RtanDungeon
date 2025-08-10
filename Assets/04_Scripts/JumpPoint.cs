using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    public int JumpPower;
    public int moveSpeed;
    public float moveChangeTime;
    public bool isMove;
    private bool isCoroutine = true;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isMove && isCoroutine)
        {
            Debug.Log("Ω√¿€");
            StartCoroutine(MovingHoraizontal());
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.CompareTag("Player"))
    //        return;

    //    other.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    //}

    private IEnumerator MovingHoraizontal()
    {
        isCoroutine = false;
        _rigidbody.velocity = transform.right * moveSpeed;  
        yield return new WaitForSeconds(moveChangeTime);
        _rigidbody.velocity = -transform.right * moveSpeed;
        yield return new WaitForSeconds(moveChangeTime);

        _rigidbody.velocity = -transform.right * moveSpeed; //ø’∫π
        yield return new WaitForSeconds(moveChangeTime);
        _rigidbody.velocity = transform.right * moveSpeed;
        yield return new WaitForSeconds(moveChangeTime);
        isCoroutine = true;

    }
}
