using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itementity : MonoBehaviour
{
    public Transform itm;
	public bool isgold = false;
	public int goldvalue = 0;
	
	public virtual void pickup(){
		if(isgold){
			player.pc.gold += goldvalue;
			Destroy(transform.parent.parent.gameObject);
		} else {
			if(master.MR.additem(itm)){ //only if we actually pick it up
				Destroy(this.gameObject);
			}
		}
	}
	
	public void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			pickup();
		}
	}
}
