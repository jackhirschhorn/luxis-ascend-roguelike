using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imgfollowcam : MonoBehaviour
{
	public Canvas myCanvas;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }
}
