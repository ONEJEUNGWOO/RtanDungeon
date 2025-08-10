using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour
{
    public int moveSpeed;
    public float moveChangeTime;
    public float curMoveChangeTime;
    public bool isMove;
    private bool isCoroutine = true;
    private Rigidbody _rigidbody;

    Vector3 moveLeft;
    Vector3 moveRight;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        curMoveChangeTime = moveChangeTime;
    }

    //private void FixedUpdate()
    //{
    //    if (isMove && isCoroutine)
    //    {
    //        Debug.Log("����");
    //        StartCoroutine(MovingHoraizontal());
    //    }
    //}

    private void Update()
    {
        moveLeft = -transform.right * moveSpeed * Time.deltaTime;
        moveRight = transform.right * moveSpeed * Time.deltaTime;
        MovinChange();

    }

    private void MovinChange()
    {
        curMoveChangeTime -= Time.deltaTime;

        if (curMoveChangeTime > moveChangeTime)
        {
            transform.position += moveRight;
        }
        else if (curMoveChangeTime > 0f)
        {
            transform.position += moveLeft;
        }
        else
        {
            curMoveChangeTime = moveChangeTime * 2;
        }
    }

    //private IEnumerator MovingHoraizontal()       //������ �̵� �� ��� ������ �ʹ� ���Ƽ� ���
    //{
    //    isCoroutine = false;
    //    _rigidbody.velocity = transform.right * moveSpeed;
    //    yield return new WaitForSeconds(moveChangeTime);
    //    _rigidbody.velocity = -transform.right * moveSpeed;
    //    yield return new WaitForSeconds(moveChangeTime);

    //    _rigidbody.velocity = -transform.right * moveSpeed; //�պ�
    //    yield return new WaitForSeconds(moveChangeTime);
    //    _rigidbody.velocity = transform.right * moveSpeed;
    //    yield return new WaitForSeconds(moveChangeTime);
    //    isCoroutine = true;
    //}

    private IEnumerator MovingHoraizontal()
    {
        isCoroutine = false;
        transform.position += moveLeft;
        yield return new WaitForSeconds(moveChangeTime);
        
        
        isCoroutine = true;
    }
}
