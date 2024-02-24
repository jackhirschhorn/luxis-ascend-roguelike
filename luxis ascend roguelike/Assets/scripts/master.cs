using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class master : MonoBehaviour
{
    public static master MR;
	public int state = 0;//0 = move, 1 = item
	public UnityEvent clickevent;
	public LayerMask wallonlymask;
	
	public void Awake(){
		MR = this;
	}
	
	public void tileclick(Transform t){
		//changes based on states
		if(!player.pc.moving){
			if(state == 0){
				player.pc.move(t);
			} else if(state == 1){
				clickevent.Invoke();
			}
		}
	}
	
	public void button(){
		Debug.Log("Ye");
	}
	
	public void dropgold(int i, Vector3 pos){
		//todo
	}
	
	public void droploot(Transform[] loot, Vector3 pos){
		//todo
	}
	
	public bool additem(Transform itm){
		//todo
		return false;
	}
}
