using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class item : MonoBehaviour
{
    public string nme;
	public int damage = 0;
	public int durability = 0;
	public UnityEvent thisevent;
	
	public void removedurability(int i){
		if(durability != -1){
			durability -= i;
			if(durability <= 0){
				breakthis();
			}
		}
	}
	
	public void breakthis(){
		Debug.Log(nme + " broke!");
	}
	
	public void dothething(){
		master.MR.state = 1;
		master.MR.clickevent = thisevent;
	}
	
	public void punch(){
		Debug.Log("punch!");
		master.MR.state = 0;
		master.MR.clickevent = null;
	}
}
