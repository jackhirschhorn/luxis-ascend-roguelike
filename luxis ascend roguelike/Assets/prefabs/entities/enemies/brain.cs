using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brain : ScriptableObject
{
	public entity targ;
	public Vector3 vtarg;
	public int state = 0;
	public sense[] sens = new sense[0];
	public attack[] atks = new attack[0];
	public pathfinder pf;
	public entity me;
	public attack hit = null;
	
	public virtual void doturn(int indx){
		switch(state){
			case 0: //search
				foreach(sense s in sens){
					entity t = s.sensecheck(me);
					if(t != null)targ = t;
				}
				if(targ != null){
					state = 1;
					//play alert animation
				} else {
					me.move(pf.wander(me),indx);
				}
			break;
			case 1: //target found
				foreach(attack a in atks){
					if(a.atkcheck(targ, me))hit = a;
				}
				
				if(hit != null){ //an attack can hit the target
					hit.demoatk(targ, me);
					vtarg = targ.transform.position;
					state = 2;
					break;
				} else { //an attack cannot hit the target
					me.move(pf.findpath(targ, me),indx);
					break;
				}
			break;
			case 2: //launch attack
				hit.doatk(vtarg, me);
				state = 1;
			break;
		}
	}
}
