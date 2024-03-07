using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : entity
{
    public brain brn;
	public string name;
	public Transform[] loot = new Transform[0];
	public int gold;
	
	public virtual void Awake(){
		brn = Instantiate(brn);
		brn.me = this;
		brn.init();
	}	
	
	public override void die(){
		master.MR.dropgold(gold,transform.position);
		master.MR.droploot(loot,transform.position);
		foreach(attack atk in brn.atks){
			atk.clearattack();
		}
		//death animation
		Destroy(this.gameObject);
	}
	
}
