using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability : ScriptableObject
{
    public int abilitytype; //0 = ability, 1 = skill, 2 = trait
	
	//serializable?
	public int xp; //current
	public int difficulty;
	public resourceopt[] costs = new resourceopt[0];
	
	public virtual bool callability(charactersheet cs, int level, int vari, int dice){
		foreach(resourceopt ro in costs){
			if(!cs.docost(ro,level,true))return false;
		}
		foreach(resourceopt ro in costs){			
			cs.docost(ro,level,false);
		}
		//stuff here
		doability(cs, level, vari, dice);
		return true;
	}
	
	public virtual void doability(charactersheet cs, int level, int vari, int dice){
		
	}
}
