using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritescroll : MonoBehaviour
{
    public Material mat;
	public float x,y;
	
	// Update is called once per frame
    void LateUpdate()
    {
		Vector2 temp = mat.GetTextureOffset("_MainTex");
		temp.x += x*Time.deltaTime;
		temp.y += y*Time.deltaTime;
        mat.SetTextureOffset("_MainTex",temp);
    }
}
