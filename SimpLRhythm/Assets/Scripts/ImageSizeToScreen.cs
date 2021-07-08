using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSizeToScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTran = gameObject.GetComponent<RectTransform>();
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
    }
}
