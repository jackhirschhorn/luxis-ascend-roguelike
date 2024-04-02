using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemrangefinder : MonoBehaviour
{
	public Transform prefab;
	public List<Transform> fabs = new List<Transform>();
	public bool active = false;
	
	public virtual void showrange(bool b){ //standard melee
		StartCoroutine(showrange2(b));
	}
	
	public virtual void showrange3(){
		if(transform.parent == master.MR.inv){
			active = !active;
			StartCoroutine(showrange2(true));
		}
	}
	
	public virtual void showrange4(){
		if(transform.parent == master.MR.inv){
			active = false;
			StartCoroutine(showrange2(false));
		}
	}
	
	public virtual IEnumerator showrange2(bool b){
		if(b){
			if(transform.parent == master.MR.inv && master.MR.itemup == null){
				for(int i = 0;i < 9; i++){
					if(i != 4){
						if(!Physics.Raycast(player.pc.transform.position, new Vector3((i%3==0?-1:(i%3==1?0:1)),0,(i/3==0?-1:(i/3==1?0:1))),1f,master.MR.wallonlymask)){
							Transform clone = Instantiate(prefab);
							clone.position = player.pc.transform.position + new Vector3((i%3==0?-1:(i%3==1?0:1)),0.11f,(i/3==0?-1:(i/3==1?0:1)));
							fabs.Add(clone);
						}
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
