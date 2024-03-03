using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : ScriptableObject
{
    public virtual bool atkcheck(entity targ, entity me){
		return false;
	}
	
	public virtual void demoatk(entity targ, entity me){
		
	}
	
	public virtual void doatk(Vector3 targ, entity me, int indx){
		me.StartCoroutine(doatk2(targ,me,indx));
	}
	
	public virtual IEnumerator doatk2(Vector3 targ, entity me, int indx){
		yield return new WaitForEndOfFrame();
	}
}
