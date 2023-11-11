using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : ScriptableObject
{
    string nme;
	int itemtype; //0 = inventory item, 1 = weapon, 2+ = armor slot
	
	public virtual void onequip(){
		
	}
	
	public virtual void onunequip(){
		
	}
	
	public virtual void use(){
		
	}
}
