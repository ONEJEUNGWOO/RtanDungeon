using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;
    public LayerMask spwanLayerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public GameObject curSpawnGameObject;

    public TextMeshProUGUI promptText;  //TODO �Ŀ� �巡�� �� ��� ���� �������� �� �����丵 �غ���
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;

    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));    //��ũ�� �߾ӿ��� ���̰� ���۵ǰ� �ϱ� ���� /2�� ��
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curSpawnGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }

            if (Physics.Raycast(ray, out hit, maxCheckDistance, spwanLayerMask)) //���̾��ũ�� ���� ������ ���� ����
            {
                if (hit.collider.gameObject != curSpawnGameObject)
                {
                    curSpawnGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetSpawnPromptText();
                }
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();      
    }

    private void SetSpawnPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();      
    }

    public void OnInteracterInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
