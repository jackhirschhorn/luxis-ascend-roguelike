using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowpotion : MonoBehaviour
{
	public int howmuch = 5;
	public int whatstat = 0;
	
    public void drinkpotion(Transform t, item it){
		if(Vector3.Distance(t.position,player.pc.transform.position)>1.5f){
			master.MR.state = 0;
			master.MR.clickevent = null;	
			master.MR.itseld = null;
		} else {
			Debug.Log("Drink up!");
			Collider[] cols = Physics.OverlapSphere(t.position, 0.25f, master.MR.entitymask);
			foreach(Collider c in cols){
				if(c.transform.parent.parent.GetComponent<entity>()){
                    c.transform.parent.parent.GetComponent<entity>().heal(whatstat,howmuch);
                }
			}
			if(cols.Length != 0){
				it.removedurability(1);
			}
			master.MR.state = 0;
			master.MR.clickevent = null;
			master.MR.itseld = null;
			master.MR.doenemyturn(0);
		}
	}
}
