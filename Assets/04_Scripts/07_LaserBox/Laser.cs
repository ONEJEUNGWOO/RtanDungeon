using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    [Header("UI")]
    public UnityEngine.UI.Slider slider;
    public TextMeshProUGUI text;
    public Canvas canvas;

    public float searchRange = 10f;
    public LayerMask layerMask;

    private LineRenderer lineRenderer;
    private Vector3 startPosition;
    private Vector3 direction;

    private bool isOpen;

    private void Awake()
    {
        // LineRenderer ĳ��
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;     //��� ���� ������ �����ֱ�

        startPosition = transform.position;
        direction = transform.forward;
    }

    private void Start()
    {
        canvas.gameObject.SetActive(false);
        isOpen = false;
    }

    private void Update()
    {
        Ray ray = new Ray(startPosition, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, searchRange, layerMask))
        {
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, hit.point);  //���̿� ������ �� �ڸ����� ���̻� ���̸� �׸��� ����
            slider.value -= 1 * Time.deltaTime;

            float second = slider.value;

            text.text = ($"{second:n0}�� �̳��� ������ �������\r\n{second:n0}�� �� �÷��̾� ���");

            if (slider.value <= 0)
            CharacterManager.Instance.Player.condition.Die();


            if (isOpen) return; //����Ŭ����� ����, �̷��� ��� �ϴ°� �´��� �� �𸣰����� if���� �� ���� ������ ���ٰ� �� ����߽��ϴ�.
            isOpen = true;
            canvas.gameObject.SetActive(true);
        }
        else
        {
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, startPosition + direction * searchRange);

            if (!isOpen) return;
            isOpen = false;
            canvas.gameObject.SetActive(false);
            slider.value = 3;
        }
    }
}
