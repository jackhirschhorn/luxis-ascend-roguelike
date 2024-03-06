using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testitem : item
{	
    public void swing(Transform t){
		Debug.Log("slash!");
		foreach(Transform t2 in master.MR.entrans){
			if(Vector3.Distance(t.position, t2.position) <= 0.1f){
				t2.GetComponent<entity>().takedamage(1,0);
				removedurability(1);
			}
		}
		master.MR.state = 0;
		master.MR.clickevent = null;
		master.MR.doenemyturn(0);
	}
}
