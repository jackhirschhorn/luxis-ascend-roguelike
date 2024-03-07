using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itementity : MonoBehaviour
{
    public Transform itm;
	public bool isgold = false;
	public int goldvalue = 0;
	
	public void Awake(){
		if(!isgold)itm.GetComponent<item>().itmref = this;
	}
	
	public virtual void pickup(){
		if(isgold){
			player.pc.gold += goldvalue;
			Destroy(transform.parent.parent.gameObject);
		} else {
			master.MR.showitem(itm, this.transform);
		}
	}
	
	public virtual void hide(){
		if(transform.parent.gameObject.activeSelf){
			master.MR.hideitem(itm, this.transform);
		}
	}
	
	public void OnTriggerEnter(Collider col){
		if(transform.parent.gameObject.activeSelf && col.tag == "Player"){
			pickup();
		}
	}
	
	public void OnTriggerExit(Collider col){
		if(transform.parent.gameObject.activeSelf && col.tag == "Player"){
			hide();
		}
	}
}
