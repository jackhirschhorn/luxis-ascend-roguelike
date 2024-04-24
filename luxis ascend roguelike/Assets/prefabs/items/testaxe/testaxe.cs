using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class testaxe : MonoBehaviour
{	
    public void swing(Transform t, item it){
		if(Vector3.Distance(t.position,player.pc.transform.position)<0.1f || Vector3.Distance(t.position,player.pc.transform.position)>1.5f){
			master.MR.state = 0;
			master.MR.clickevent = null;
			master.MR.itseld = null;			
		}
		else{
				Debug.Log("Dismantle!");
				
				Vector3 pos2, pos3 = new Vector3(0,0,0);
				
				if(t.transform.position.x == player.pc.transform.transform.position.x){
					pos2  = t.transform.position+new Vector3(1,0.1f,0);
					pos3 = t.transform.position+new Vector3(-1,0.1f,0);
				}
				else if(t.transform.position.z == player.pc.transform.transform.position.z){
					pos2  = t.transform.position+new Vector3(0,0.1f,1);
					pos3   = t.transform.position+new Vector3(0,0.1f,-1);
				}
				else{
					pos2 = new Vector3(player.pc.transform.transform.position.x,0.1f,t.transform.position.z);
					pos3 = new Vector3(t.transform.position.x,0.1f,player.pc.transform.transform.position.z);
				}
				
					Collider[] cols = Physics.OverlapSphere(t.position, 0.25f, master.MR.entitymask).Concat(Physics.OverlapSphere(pos2, 0.25f, master.MR.entitymask).Concat(Physics.OverlapSphere(pos3, 0.25f, master.MR.entitymask)).ToArray()).ToArray();
					
					foreach(Collider c in cols){
						if(c.transform.parent.parent.GetComponent<entity>()){
							c.transform.parent.parent.GetComponent<entity>().takedamage(it.damage,0);
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
