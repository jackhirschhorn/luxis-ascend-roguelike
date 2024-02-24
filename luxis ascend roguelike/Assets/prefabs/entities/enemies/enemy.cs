using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : entity
{
    public brain brn;
	public string name;
	public Transform[] loot = new Transform[0];
	public int gold;
	
	public virtual void takedamage(int dam, int peirce){
		hp -= Mathf.Max(dam-(Mathf.Max(armor-peirce,0)),0);
	}
	
	public virtual void die(){
		master.MR.dropgold(gold,transform.position);
		master.MR.droploot(loot,transform.position);
		//death animation
		Destroy(this.gameObject);
	}
}
