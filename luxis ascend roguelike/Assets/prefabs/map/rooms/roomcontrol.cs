using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomcontrol : MonoBehaviour
{
    public List<door> doors = new List<door>();
	public List<enemy> enmy = new List<enemy>();
	public GameObject canv;
	public bool active = false;
	public bool cleared = false;
	
	public IEnumerator Start(){
		canv.SetActive(false);		
		yield return new WaitForSeconds(0.5f);
		checkroom();
	}
	
	public void checkroom(){
		if(active){
			canv.SetActive(true);
			foreach(door d in doors){
				d.gameObject.SetActive((cleared?false:true));
				d.transform.parent.GetChild(3).gameObject.SetActive(true);
			}
			foreach(enemy e in enmy){
				e.gameObject.SetActive(true);
			}
		} else {
			foreach(door d in doors){
				d.gameObject.SetActive(false);
				d.transform.parent.GetChild(3).gameObject.SetActive(false);
			}
			foreach(enemy e in enmy){
				e.gameObject.SetActive(false);
			}
		}
	}
	
	public void clearroom(enemy e){
		enmy.Remove(e);
		if(enmy.Count == 0){
			cleared = true;
			checkroom();
		}
	}
	
	public void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			active = true;
			//Camera.main.transform.parent.position = transform.position;
			checkroom();
		}
	}
	
	/*public void OnTriggerExit(Collider col){
		if(col.tag == "Player"){
			active = false;
			checkroom();
		}
	}*/
}
