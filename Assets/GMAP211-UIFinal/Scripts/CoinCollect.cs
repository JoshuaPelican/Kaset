using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collects coin and prevents holding it
        Destroy(collision.gameObject.GetComponent<ClickDrag>());
        //Play Coin Sound Effect
        Camera.main.GetComponent<Animator>().SetTrigger("InsertCoin");
    }
}
