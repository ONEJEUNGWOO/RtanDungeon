using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour    //데미지를 입었을 때 이미지를 껐다 켰다 해주는 클래스
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    void Start()
    {
        CharacterManager.Instance.Player.condition.OnTakeDamage += Flash;   //이벤트 발생 할때 실행 할 함수등록
    }

    private void OnDisable()
    {
        CharacterManager.Instance.Player.condition.OnTakeDamage -= Flash;   //구독 취소
    }

    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color (255f, 0f, 0f);
        coroutine = StartCoroutine(FadeAway()); //따로 변수에 담아주지 않으면 끌 방법이 AllStop 뿐이기 에 변수에 추가해 준 것 같음
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
