using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public enum Direction
{
    left,
    right,
    front,
    back,
}

public class launchingBox : MonoBehaviour
{
    [Header("UI")]                          //필요한 UI들 가져오기 인스펙터창 말고도 가져오는 방법을 아직 모르겠습니다..
    public UnityEngine.UI.Slider slider;
    public TextMeshProUGUI text;
    public Canvas canvas;
    public Image image;
    public Color color;

    public float launchingPower;
    public float readyTime;         //기다려야 할 시간 (대기시간)
    public float loadTime = 0f;     //기다린 시간 
    public Direction type;

    private bool isReady;
    private Vector3 direction;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        slider.maxValue = readyTime;    //슬라이더 길이 미리 맞춰주기
        canvas.gameObject.SetActive(false);

        switch (type)                   //지정 방향벡터 넣어주기
        {
            case Direction.left:
                direction = Vector3.left;
                break;
            case Direction.right:
                direction = Vector3.right;
                break;
            case Direction.front:
                direction = Vector3.forward;
                break;
            case Direction.back:
                direction = Vector3.back;
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() == null) return;

        _rigidbody = other.GetComponent<Rigidbody>();
        
        loadTime += Time.deltaTime;
        isReady = loadTime > readyTime ? true : false;

        canvas.gameObject.SetActive(true);
        slider.value = loadTime;
        color = image.color;
        color.a = readyTime - loadTime;
        image.color = color;

        if (loadTime < readyTime)
            text.text = ($"런처 충전 까지 남은 시간 : {readyTime - loadTime:n0}");
        else
            text.text = ("준비 완료.  사용 하시려면 F키를 눌러 주세요");

        //other.GetComponent<Rigidbody>().AddForce(direction * launchingPower, ForceMode.VelocityChange);   //impulse모드는 너무 조금 이동하고 이건 너무 빨라서 코루틴을 사용했습니다.
        //Debug.DrawRay(other.transform.position, direction, Color.red, 2f);
    }

    private void OnTriggerExit(Collider other)
    {
        loadTime = 0;
        canvas.gameObject.SetActive(false);
    }

    public void OnLauncherStart(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started || loadTime < 3) return;

        StartCoroutine(ApplyForceOverTime(_rigidbody, direction, launchingPower, 1f));
    }

    IEnumerator ApplyForceOverTime(Rigidbody rb, Vector3 dir, float power, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            rb.AddForce(dir * power / duration, ForceMode.VelocityChange);
            timer += Time.deltaTime;
            yield return null;  // 한 프레임 대기
        }
    }
}
