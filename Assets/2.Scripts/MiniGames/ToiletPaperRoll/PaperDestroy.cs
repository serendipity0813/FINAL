using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperDestroy : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -7)
            Destroy(gameObject);
    }
}
