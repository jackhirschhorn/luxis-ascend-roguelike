using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class master : MonoBehaviour
{
    public static master MR;
	
	public void Awake(){
		MR = this;
	}
	
	public void tileclick(Transform t){
		//changes based on states
		if(!player.pc.moving){
			player.pc.move(t);
		}
	}
}
