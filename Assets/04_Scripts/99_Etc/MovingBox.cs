using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour      //�������� �����̴� �÷��� 
{
    public int moveSpeed;               //������ �ӵ�
    public float moveChangeTime;        //�� �������� �̵��� �ð�
    public float curMoveChangeTime;     //���� �̵��ð�
    public bool isMove;                 //������ ������ �ƴ��� ���ð���
    //private bool isCoroutine = true;  //������ �̵� �� �� ����ߴ� ����
    private Rigidbody _rigidbody;       //������ٵ� ĳ���صα�

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
}
