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
	
	public Transform vis; //the asset we use
	public Transform visuse; //the reference to the asset after spawned
	public override void demoatk(entity targ, entity me){
		visuse = Instantiate(vis); //spawn the asset
		visuse.position = targ.transform.position+new Vector3(0,0.1f,0); //place the asset at the indicated location (and slightly up so it doesn't Z-clip)
	}
	
	public override IEnumerator doatk2(Vector3 targ, entity me, int indx){
		Collider[] cols = Physics.OverlapSphere(visuse.position, 0.25f, master.MR.entitymask);
		foreach(Collider c in cols){
			if(c.transform.parent.parent.GetComponent<entity>()){
				c.transform.parent.parent.GetComponent<entity>().takedamage(1,0);
			}
		}
		Destroy(visuse.gameObject);
		me.anim.SetBool("attack",true);
		yield return new WaitUntil(() => !me.anim.GetBool("attack"));
		master.MR.doenemyturnatk(indx+1);
	}
	
	public override void clearattack(){
		if(visuse != null)Destroy(visuse.gameObject);
	}
}
