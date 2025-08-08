using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour    //�������� �Ծ��� �� �̹����� ���� �״� ���ִ� Ŭ����
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    void Start()
    {
        CharacterManager.Instance.Player.condition.OnTakeDamage += Flash;   //�̺�Ʈ �߻� �Ҷ� ���� �� �Լ����
    }

    private void OnDisable()
    {
        CharacterManager.Instance.Player.condition.OnTakeDamage -= Flash;   //���� ���
    }

    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color (255f, 0f, 0f);
        coroutine = StartCoroutine(FadeAway()); //���� ������ ������� ������ �� ����� AllStop ���̱� �� ������ �߰��� �� �� ����
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(255f, 0f, 0f, a);
            yield return null;
        }

        image.enabled = false;
    }
}
