using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage        //�߻�ȭ �������� �÷��̾� �̿ܿ��� �Ա� ������
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamage //�÷��̾� ������� �����ش� Ŭ����
{
    public UICondition uICondition;

    Condition health { get { return uICondition.health; } }
    Condition hunger { get { return uICondition.hunger; } }
    Condition stamina { get { return uICondition.stamina; } }
    Condition speed { get { return uICondition.speed; } }

    public float hungryDamage;

    public event Action OnTakeDamage;


    void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue == 0f)
        {
            health.Subtract(hungryDamage * Time.deltaTime);
        }

        if (health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amout)
    {
        health.Add(amout);
    }

    public void Eat(float amout)
    {
        hunger.Add(amout);
    }

    public void Die()
    {
        Debug.Log("����"); //TODO �׾��� �� UI �����
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        OnTakeDamage?.Invoke();  
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0f)
            return false;

        stamina.Subtract(amount);
        return true;
    }

    public void Run(float amount)
    {
        StartCoroutine(ChangeSpeed(amount));
    }

    public IEnumerator ChangeSpeed(float amount)
    {
        float curSpeed = CharacterManager.Instance.Player.controller.moveSpeed;
        CharacterManager.Instance.Player.controller.moveSpeed += amount;
        yield return new WaitForSeconds(3f);

        CharacterManager.Instance.Player.controller.moveSpeed = curSpeed;


    }
}
