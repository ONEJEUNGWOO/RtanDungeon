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
        // LineRenderer 캐싱
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;     //몇개의 선을 이을지 정해주기

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
            lineRenderer.SetPosition(1, hit.point);  //레이에 맞으면 그 자리에서 더이상 레이를 그리지 않음
            slider.value -= 1 * Time.deltaTime;

            float second = slider.value;

            text.text = ($"{second:n0}초 이내로 지역을 벗어나세요\r\n{second:n0}초 후 플레이어 사망");

            if (slider.value <= 0)
            CharacterManager.Instance.Player.condition.Die();


            if (isOpen) return; //가드클라우즈 패턴, 이렇게 사용 하는게 맞는지 잘 모르겠지만 if문을 덜 돌기 때문에 좋다고 들어서 사용했습니다.
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
