using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemData itemToGive;
    public int quantityPerHit;
    public int capacy;

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            capacy -= 1;
            if (capacy <= 0)
            {
                Instantiate(itemToGive.dropPrefab, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up));
                Destroy(gameObject);
                break;
            }
            Instantiate(itemToGive.dropPrefab, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up));
        }
    }
}
