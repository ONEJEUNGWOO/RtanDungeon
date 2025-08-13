using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour       //플레이어 이동을 담당할 클래스
{
    private Camera _camera;


    [Header("Movement")]  //헤더 선언을 하면 인스펙터에서 구분이 되어 보기 편해진다.
    public float moveSpeed;
    public float JumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minLook;           //최소 시야 범위
    public float maxLook;           //최대 시야 범위
    public float lookSpeed;         //시야 이동속도
    private float camCurXRot;       //현재카메라 시야 위치
    private Vector2 mouseDelta;     //마우스 이동범위
    public bool canLook = true;     //마우스 이동불가 및 가능 불값으로 표시
    public LayerMask layerMask;

    public Action inventory;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        Move();
        Debug.Log(transform.forward);
        Debug.Log(transform.right);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //커서 없애기
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
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x; //현재 내 기준 방향을 transform을 통해 구하고 키 입력시 변경 해주는 코드, 위치벡터?
        dir *= moveSpeed; //방향만 있었는데 속도를 곱해줌
        dir.y = _rigidbody.velocity.y; //y는 rigid의 기본 중력 영향을 그대로 받기 위해 rigid 의 값을 받음

        _rigidbody.velocity = dir; //현재 리지드바디에 속도와 방향을 넣어줌
    }

    private void Look()
    {
        camCurXRot += mouseDelta.y * lookSpeed;
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSpeed, 0);
    }

    public void OnMove(InputAction.CallbackContext context)     //키 입력이 일어날 때 마다 값을 넘겨 받는 메서드
    {
        if (context.phase == InputActionPhase.Performed)        //키를 누르고 있을 땐 방향을 받아와라
        {
            curMovementInput = context.ReadValue<Vector2>();        //가져오기
        }
        else if (context.phase == InputActionPhase.Canceled)        //키를 누르고 있지 않다면 방향을 없애라.
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

    void ToggleCursor()     //실행 될 때 마다 불값을 반대로 변경해주는 함수 마우스 커서 보이거나 화면 움직임을 불값으로 조정하는 함수
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void OnPushButton(InputAction.CallbackContext context)       //우클릭 했을 때 발생 할 이벤트
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));    //스크린 중앙에서 레이가 시작되게 하기 위해 /2를 함
        RaycastHit hit;
        Children target;

        if (!Physics.Raycast(ray, out hit, maxLook, layerMask) || context.phase != InputActionPhase.Started)
            return;

        target = hit.transform.GetComponentInChildren<Children>();

        Debug.Log($"들어왔음 {hit.transform.gameObject.name}");

        if (hit.transform.TryGetComponent<SpawnController>(out SpawnController spawner))
        {
            spawner.SpawnNPC();
        }

        StartCoroutine(ButtonClick(target)); //TODO 일단 버튼은 움직임..
    }

    public IEnumerator ButtonClick(Children target)
    {
        target.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.3f);

        target.gameObject.SetActive(true);
    }

    public void CatLeap(bool isWall)
    {
        if (!isWall)
            _rigidbody.useGravity = true;
        else
            _rigidbody.useGravity = false;
    }
}
