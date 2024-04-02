using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionrange : itemrangefinder
{
    
	public override IEnumerator showrange2(bool b){ //standard melee
		if(b){
			if(transform.parent == master.MR.inv && master.MR.itemup == null){			
				for(int i = 0;i < 9; i++){
					if(!Physics.Raycast(player.pc.transform.position+new Vector3(0,0.5f,0), new Vector3((i%3==0?-1:(i%3==1?0:1)),0,(i/3==0?-1:(i/3==1?0:1))),1f,master.MR.wallonlymask)){
						Transform clone = Instantiate(prefab);
						clone.position = player.pc.transform.position + new Vector3((i%3==0?-1:(i%3==1?0:1)),0.11f,(i/3==0?-1:(i/3==1?0:1)));
						fabs.Add(clone);
					}
				}
			}
		} else if(!active) {
			for(int i = fabs.Count-1; i >= 0; i--){
				Destroy(fabs[i].gameObject);
			}
			fabs.Clear();
		}
		yield return new WaitForEndOfFrame();
	}
}
