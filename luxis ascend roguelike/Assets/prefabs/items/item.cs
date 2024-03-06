using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class item : MonoBehaviour
{
    public string nme;
	public int damage = 0;
	public int durability = 0;
	public int goldcost = 0;
	public itementity itmref;
	public Sprite icon, blank;
	public UEvent thisevent;
	
	public void Awake(){
		if(icon == null)icon = transform.GetComponent<Image>().sprite;
	}
	
	public virtual void removedurability(int i){
		if(durability != -1){
			durability -= i;
			if(durability <= 0){
				breakthis();
			}
		}
	}
	
	public virtual void breakthis(){
		Debug.Log(nme + " broke!");
	}
	
	public void ondrag(){
		if(nme != ""){
			transform.GetComponent<Image>().sprite = blank;
			master.MR.itemup = this;
		}
	}
	
	public void enddrag(){
		if(nme != ""){
			transform.GetComponent<Image>().sprite = icon;
			master.MR.pickup();
			StartCoroutine(enddrag2());
		}
	}
	
	public IEnumerator enddrag2(){
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		master.MR.itemup = null;
	}
	
	public void pickup_itemcall(){
		if(transform.GetComponent<Animator>().GetBool("ye") && transform.parent == master.MR.inv)StartCoroutine(pickup2());
	}
	
	public IEnumerator pickup2(){
		yield return new WaitForEndOfFrame();
		if(master.MR.itemup != null){
			Transform clone = Instantiate(master.MR.itemup.transform);
			clone.parent = transform.parent;
			clone.SetSiblingIndex(this.transform.GetSiblingIndex()+1);
			(clone as RectTransform).anchoredPosition = (this.transform as RectTransform).anchoredPosition;
			Destroy(master.MR.itemup.itmref.transform.parent.gameObject);
			Destroy(master.MR.itemup.gameObject);
			yield return new WaitForEndOfFrame();
			master.MR.restackpickups();
			master.MR.doenemyturn(0);
			yield return new WaitForEndOfFrame();
			Destroy(this.gameObject);
			
		}
	}
	
	public virtual void dothething(){
		if(transform.parent == master.MR.inv){
			master.MR.state = 1;
			master.MR.clickevent = thisevent;
		}
	}
	
	public void punch(Transform t){
		Debug.Log("punch!");
		foreach(Transform t2 in master.MR.entrans){
			if(Vector3.Distance(t.position, t2.position) <= 0.1f)t2.GetComponent<entity>().takedamage(1,0);
		}
		master.MR.state = 0;
		master.MR.clickevent = null;
		master.MR.doenemyturn(0);
	}
}
