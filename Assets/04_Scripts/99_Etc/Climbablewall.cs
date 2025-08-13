using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbablewall : MonoBehaviour
{
    public bool isClim = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;

        CharacterManager.Instance.Player.controller.CatLeap(isClim);
        isClim = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Player") return;

        CharacterManager.Instance.Player.controller.CatLeap(isClim);
        isClim = true;
    }


}
