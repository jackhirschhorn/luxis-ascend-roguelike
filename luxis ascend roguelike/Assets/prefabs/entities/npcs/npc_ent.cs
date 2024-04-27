using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc_ent : enemy
{
    public virtual void onownedpickup(item i){
		Debug.Log("hey! that " + i.nme + " is mine!");
	}
	
	public virtual void onowneddrop(item i){
		Debug.Log("thanks for returning that " + i.nme);
	}
}
