using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : entity
{
	public bool isboss = false;
	public door(){
		hp = 5;
		chp = 5;
		armor = 5;
	}
	
	public void updateroomin_door(Transform t){
		if(t == transform.parent.parent){
			gameObject.SetActive(true);
			//play doorclose animation
		} else {
			gameObject.SetActive(false);
		}
	}
}
