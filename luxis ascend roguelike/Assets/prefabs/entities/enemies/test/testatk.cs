using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="testatk")]
public class testatk : attack
{
    public override bool atkcheck(entity targ, entity me){
		if(Vector3.Distance(me.transform.position,player.pc.transform.position) < 1.5f){ //are we close enough?
			if(!Physics.Raycast(me.transform.position+new Vector3(0,0.1f,0), player.pc.transform.position-me.transform.position, 1.5f, master.MR.wallonlymask)){//is there not a wall between us?
				return true;
			}
		}
		return false;
	}
	
	public Transform vis;
	public Transform visuse;
	public override void demoatk(entity targ, entity me){
		visuse = Instantiate(vis);
		visuse.position = targ.transform.position+new Vector3(0,0.1f,0);
	}
	
	public override void doatk(Vector3 targ, entity me){
		foreach(Transform t2 in master.MR.entrans){
			if(Vector3.Distance(targ, t2.position) <= 0.1f)t2.GetComponent<entity>().takedamage(1,0);
		}
		if(Vector3.Distance(targ, player.pc.transform.position) <= 0.1f)player.pc.takedamage(1,0);
		Destroy(visuse.gameObject);
	}
}
