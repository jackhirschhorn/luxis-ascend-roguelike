using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vision : ScriptableObject
{
	public float visrange = 3.5f;
	
	
    public virtual entity sensecheck(entity me){
		if(Vector3.Distance(me.transform.position,player.pc.transform.position) < visrange){ //are we close enough?
			if(!Physics.Raycast(me.transform.position, player.pc.transform.position-me.transform.position, visrange, master.MR.wallonlymask)){//is there not a wall between us?
				return player.pc;
			}
		}
		return null;
	}
}
