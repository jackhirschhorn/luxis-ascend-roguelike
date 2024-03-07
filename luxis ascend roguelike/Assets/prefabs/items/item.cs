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
		if(master.MR.waspicked && transform.parent == master.MR.inv){
			int temp = this.transform.GetSiblingIndex();
			dropitem();
			master.MR.addinvitem(temp);
			master.MR.waspicked = false;
		}
	}
	
	public void pickup_itemcall(){
		if(transform.GetComponent<Animator>().GetBool("ye") && transform.parent == master.MR.inv)StartCoroutine(pickup2());
	}
	
	public IEnumerator pickup2(){
		yield return new WaitForEndOfFrame();
		if(master.MR.itemup != null){
			if(master.MR.itemup == this){
				master.MR.waspicked = false;			
			} else {
				master.MR.waspicked = false;
				Transform clone = master.MR.itemup.transform;
				if(clone.parent != transform.parent){
					clone.parent = transform.parent;
					clone.SetSiblingIndex(this.transform.GetSiblingIndex()+1);
					(clone as RectTransform).anchoredPosition = (this.transform as RectTransform).anchoredPosition;
					master.MR.itemup.itmref.transform.parent.gameObject.SetActive(false);
					yield return new WaitForEndOfFrame();
					master.MR.restackpickups();
					master.MR.doenemyturn(0);
					yield return new WaitForEndOfFrame();
					if(nme == ""){
						Destroy(this.gameObject);
					} else {
						dropitem();
						//drop item
					}
				} else {
					int tempindx = this.transform.GetSiblingIndex();
					Vector2 tempvec = (this.transform as RectTransform).anchoredPosition;
					(this.transform as RectTransform).anchoredPosition = (clone as RectTransform).anchoredPosition;
					//if(clone.GetSiblingIndex() < tempindx)tempindx--;
					transform.SetSiblingIndex(clone.GetSiblingIndex());
					clone.SetSiblingIndex(tempindx);
					(clone as RectTransform).anchoredPosition = tempvec;
					
				}
			}
			
		}
	}
	
	public virtual void dropitem(){
		itmref.transform.parent.gameObject.SetActive(true);
		itmref.transform.parent.GetChild(1).position = player.pc.transform.position;
		transform.parent = itmref.transform.parent;
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
