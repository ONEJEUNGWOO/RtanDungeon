using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour    //����� �Ŵ��� ���� Ŭ���� �ܺο��� �� Ŭ������ ���� 
{
    public Condition health;
    public Condition hunger;
    public Condition stamina;
    public Condition speed;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.uICondition = this;
    }

}
