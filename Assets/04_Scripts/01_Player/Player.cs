using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //플레이어에게 컴포넌트 달아주는 클래스
{
    public PlayerController controller;
    public PlayerCondition condition;
    public Equipment equip;
    public ItemData ItemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        equip = GetComponent<Equipment>();
    }
}