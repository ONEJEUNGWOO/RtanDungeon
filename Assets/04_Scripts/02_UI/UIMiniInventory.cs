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
    private ItemData item;
    private PlayerCondition condition;

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

    private void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
    }
    public void UseConsumableIcon()        // 처음 아이콘 넣어주기
    {
        //if (icon != null) return;
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].item == null || !inventory.slots[i].item.canStack) continue;

            icon.sprite = inventory.slots[i].icon.sprite;
            item = inventory.slots[i].item;
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
            item = inventory.slots[i].item;
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
            item = inventory.slots[i].item;
            curIconIndex = i;
            break;
        }
    }

    public void OnUseConsumableItem(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started) return;
        Debug.Log("실행?");

        if (item.type == ItemType.Consumable)
        {
            for (int i = 0; i < item.consumables.Length; i++)
            {
                switch (item.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(item.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(item.consumables[i].value);
                        break;
                    case ConsumableType.Speed:
                        condition.Run(item.consumables[i].value);
                        break;
                }
            }
            inventory.RemoveMiniInventoryItem(curIconIndex);

            if (inventory.slots[curIconIndex].quantity <= 0)
            {
                ClearMiniInventory();
            }
        }
    }

    void ClearMiniInventory()
    {
        icon.sprite = null;
        item = null;
        curIconIndex = -1;
    }
}
