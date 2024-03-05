using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class item : MonoBehaviour
{
    public string nme;
	public int damage = 0;
	public int durability = 0;
	public int goldcost = 0;
	public int visindx = 0;
	public UEvent thisevent;
	
	public virtual void removedurability(int i){
		if(durability != -1){
			durability -= i;
			if(durability <= 0){
				breakthis();
			}
		}
	}
	
	public virtual void breakthis(){
		Debug.Log(nme + " broke!");
	}
	
	public void ondrag(){
		Debug.Log("ye");
	}
	
	public void enddrag(){
		Debug.Log("ne");
	}
	
	public virtual void dothething(){
		master.MR.state = 1;
		master.MR.clickevent = thisevent;
	}
	
	public void punch(Transform t){
		Debug.Log("punch!");
		foreach(Transform t2 in master.MR.entrans){
			if(Vector3.Distance(t.position, t2.position) <= 0.1f)t2.GetComponent<entity>().takedamage(1,0);
		}
		master.MR.state = 0;
		master.MR.clickevent = null;
		master.MR.doenemyturn(0);
	}
}
