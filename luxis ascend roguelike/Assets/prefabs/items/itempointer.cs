using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itempointer : MonoBehaviour
{
    public Transform pcol, icol;
	
	public void FixedUpdate(){ 
		pcol.position = new Vector3(Mathf.FloorToInt(icol.position.x+0.5f),0.5f,Mathf.FloorToInt(icol.position.z+0.5f));
	}
}
