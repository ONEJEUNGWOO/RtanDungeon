using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIMiniInventory : MonoBehaviour
{
    public static UIMiniInventory Instance;

    public Image icon;
    public UIInventory inventory;

    private int curIconIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != null)
                Destroy(gameObject);
        }
    }

    public void UseConsumableIcon()        // 처음 아이콘 넣어주기
    {
        //if (icon != null) return;
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].item == null || !inventory.slots[i].item.canStack) continue;

            icon.sprite = inventory.slots[i].icon.sprite;
            curIconIndex = i;
            break;
        }
    }

    public void OnNextConsumableIcon(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;

        for (int i = curIconIndex + 1; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].item == null || !inventory.slots[i].item.canStack) continue;
            Debug.Log("이후");
            icon.sprite = inventory.slots[i].icon.sprite;
            curIconIndex = i;
            break;
        }
    }

    public void OnBeforeConsumableIcon(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;

        for (int i = curIconIndex - 1; i > -1; i--)
        {
            if (inventory.slots[i].item == null || !inventory.slots[i].item.canStack) continue;
            Debug.Log("이전");
            icon.sprite = inventory.slots[i].icon.sprite;
            curIconIndex = i;
            break;
        }
    }
}
