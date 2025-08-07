using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour //�÷��̾�� ������Ʈ �޾��ִ� Ŭ����
{
    public PlayerController controller;
    public PlayerCondition condition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }
}