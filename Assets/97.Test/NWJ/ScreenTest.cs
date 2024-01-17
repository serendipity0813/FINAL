using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTest : MonoBehaviour
{
    RectTransform rectTransform;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (TouchManager.instance.IsBegan())
        {
            Debug.LogFormat("{0}, {1}",Screen.width, Screen.height);
        }
    }
}
