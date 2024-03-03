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
	
	public void Awake(){
		for(int i = 0; i < atks.Length; i++){
			atks[i] = Instantiate(atks[i]);
		}
	}
	
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
					me.anim.SetBool("suprise",true);					
					master.MR.doenemyturn(indx+1);
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
					master.MR.doenemyturn(indx+1);
					break;
				} else { //an attack cannot hit the target
					me.move(pf.findpath(targ, me),indx);
					break;
				}
				break;
			default:
				master.MR.doenemyturn(indx+1);
			break;
		}
	}
	
	public virtual void doatkturn(int indx){
		switch(state){
			case 2:
				state = 3;				
				master.MR.doenemyturnatk(indx+1);
				break;
			case 3: //launch attack
				hit.doatk(vtarg, me, indx);
				state = 1;
				hit = null;
				break;			
			default:
			master.MR.doenemyturnatk(indx+1);
			break;
		}
	}
}
