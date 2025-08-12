using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour      //도전과제 움직이는 플렛폼 
{
    public int moveSpeed;               //움직일 속도
    public float moveChangeTime;        //한 방향으로 이동할 시간
    public float curMoveChangeTime;     //남은 이동시간
    public bool isMove;                 //움직일 것인지 아닌지 선택가능
    //private bool isCoroutine = true;  //물리로 이동 할 때 사용했던 변수
    private Rigidbody _rigidbody;       //리지드바디 캐싱해두기

    Vector3 moveLeft;                   
    Vector3 moveRight;                  

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        curMoveChangeTime = moveChangeTime;
    }

    private void Update()
    {
        moveLeft = -transform.right * moveSpeed * Time.deltaTime;
        moveRight = transform.right * moveSpeed * Time.deltaTime;
        MovinChange();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        collision.transform.SetParent(this.transform);
    }
  
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        collision.transform.SetParent(null);
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

    //private IEnumerator MovingHoraizontal()       //물리로 이동 할 경우 문제가 너무 많아서 폐기
    //{
    //    isCoroutine = false;
    //    _rigidbody.velocity = transform.right * moveSpeed;
    //    yield return new WaitForSeconds(moveChangeTime);
    //    _rigidbody.velocity = -transform.right * moveSpeed;
    //    yield return new WaitForSeconds(moveChangeTime);

    //    _rigidbody.velocity = -transform.right * moveSpeed; //왕복
    //    yield return new WaitForSeconds(moveChangeTime);
    //    _rigidbody.velocity = transform.right * moveSpeed;
    //    yield return new WaitForSeconds(moveChangeTime);
    //    isCoroutine = true;
    //}
}
