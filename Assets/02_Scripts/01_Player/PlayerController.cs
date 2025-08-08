using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour       //�÷��̾� �̵��� ����� Ŭ����
{
    [Header("Movement")]  //��� ������ �ϸ� �ν����Ϳ��� ������ �Ǿ� ���� ��������.
    public float moveSpeed;
    public float JumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minLook;           //�ּ� �þ� ����
    public float maxLook;           //�ִ� �þ� ����
    public float lookSpeed;         //�þ� �̵��ӵ�
    private float camCurXRot;       //����ī�޶� �þ� ��ġ
    private Vector2 mouseDelta;     //���콺 �̵�����
    public bool canLook = true;     //���콺 �̵��Ұ� �� ���� �Ұ����� ǥ��

    public Action inventory;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Move();
        Debug.Log(transform.forward);
        Debug.Log(transform.right);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Ŀ�� ���ֱ�
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        Look();
    }


    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; //���� �� ���� ������ transform�� ���� ���ϰ� Ű �Է½� ���� ���ִ� �ڵ�, ��ġ����?
        dir *= moveSpeed; //���⸸ �־��µ� �ӵ��� ������
        dir.y = _rigidbody.velocity.y; //y�� rigid�� �⺻ �߷� ������ �״�� �ޱ� ���� rigid �� ���� ����

        _rigidbody.velocity = dir; //���� ������ٵ� �ӵ��� ������ �־���
    }

    private void Look()
    {
        camCurXRot += mouseDelta.y * lookSpeed;
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSpeed, 0);
    }

    public void OnMove(InputAction.CallbackContext context)     //Ű �Է��� �Ͼ �� ���� ���� �Ѱ� �޴� �޼���
    {
        if (context.phase == InputActionPhase.Performed)        //Ű�� ������ ���� �� ������ �޾ƿͶ�
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)        //Ű�� ������ ���� �ʴٸ� ������ ���ֶ�.
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGround())
        {
            _rigidbody.AddForce(Vector2.up * JumpPower, ForceMode.Impulse);
        }
    }

    bool isGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)

        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
                return true;
        }
        return false;
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()     //���� �� �� ���� �Ұ��� �ݴ�� �������ִ� �Լ� ���콺 Ŀ�� ���̰ų� ȭ�� �������� �Ұ����� �����ϴ� �Լ�
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
