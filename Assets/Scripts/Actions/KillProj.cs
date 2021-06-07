using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillProj : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
