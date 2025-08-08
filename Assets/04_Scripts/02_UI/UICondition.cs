using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour    //컨디션 매니저 같은 클래스 외부에서 이 클래스에 의존 
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
