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
    [Header("UI")]                          //�ʿ��� UI�� �������� �ν�����â ���� �������� ����� ���� �𸣰ڽ��ϴ�..
    public UnityEngine.UI.Slider slider;
    public TextMeshProUGUI text;
    public Canvas canvas;
    public Image image;
    public Color color;

    public float launchingPower;
    public float readyTime;         //��ٷ��� �� �ð� (���ð�)
    public float loadTime = 0f;     //��ٸ� �ð� 
    public Direction type;

    private bool isReady;
    private Vector3 direction;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        slider.maxValue = readyTime;    //�����̴� ���� �̸� �����ֱ�
        canvas.gameObject.SetActive(false);

        switch (type)                   //���� ���⺤�� �־��ֱ�
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
            text.text = ($"��ó ���� ���� ���� �ð� : {readyTime - loadTime:n0}");
        else
            text.text = ("�غ� �Ϸ�.  ��� �Ͻ÷��� FŰ�� ���� �ּ���");

        //other.GetComponent<Rigidbody>().AddForce(direction * launchingPower, ForceMode.VelocityChange);   //impulse���� �ʹ� ���� �̵��ϰ� �̰� �ʹ� ���� �ڷ�ƾ�� ����߽��ϴ�.
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
            yield return null;  // �� ������ ���
        }
    }
}
