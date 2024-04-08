using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations;

public class item : MonoBehaviour
{
    public string nme;
	public string desc;
	public int damage = 0;
	public int durability = 0;
	public int goldcost = 0;
	public itementity itmref;
	public Sprite icon, blank;
	public UEvent thisevent;
	
	public void Awake(){
		if(icon == null)icon = transform.GetComponent<Image>().sprite;
		if(damage != -1){
			transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+damage;
			transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+durability;
		} else if (durability != -1) {
			transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+durability;
		}
	}
	
	public void Start(){
		ConstraintSource cs = new ConstraintSource();
		cs.sourceTransform = master.MR.canvasscaler;
		cs.weight = 1;
		transform.GetComponent<ScaleConstraint>().SetSource(0,cs);
	}
	
	public void LateUpdate(){
		if(damage != -1){
			transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+damage;
			transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+durability;
		} else if (durability != -1) {
			transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+durability;
		}
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
		int temp = transform.GetSiblingIndex();
		transform.parent = null;
		master.MR.addinvitem(temp);
		Destroy(itmref.gameObject);
		Destroy(this.gameObject);
	}
	
	public virtual void ondrag(){
		if(nme != ""){
			master.MR.showdrag(icon, damage, durability);
			transform.GetComponent<Image>().sprite = blank;
			master.MR.itemup = this;
		}
	}
	
	public virtual void enddrag(){
		if(nme != ""){
			transform.GetComponent<Image>().sprite = icon;
			master.MR.pickup();
			StartCoroutine(enddrag2());
			master.MR.hidedrag();
		}
	}
	
	public virtual IEnumerator enddrag2(){
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		master.MR.itemup = null;
		if(master.MR.waspicked && transform.parent == master.MR.inv){
			int temp = this.transform.GetSiblingIndex();
			dropitem();
			master.MR.addinvitem(temp);
			master.MR.waspicked = false;
			master.MR.doenemyturn(0);
		} else if (master.MR.waspicked && transform.parent.name == "inv"){
			int temp = this.transform.GetSiblingIndex();
			Transform tempt = transform.parent.parent.parent;
			dropitem();
			tempt.GetComponent<backpack>().replaceslot(temp);
			master.MR.doenemyturn(0);
		}
	}
	
	public virtual void pickup_itemcall(){
		if(transform.GetComponent<Animator>().GetBool("ye") && (transform.parent == master.MR.inv || transform.parent.name == "inv"))StartCoroutine(pickup2());
	}
	
	public virtual IEnumerator pickup2(){
		yield return new WaitForEndOfFrame();
		if(master.MR.itemup != null){
			if(master.MR.itemup == this){
				master.MR.waspicked = false;			
			} else {
				master.MR.waspicked = false;
				Transform clone = master.MR.itemup.transform;
				if(clone.parent != transform.parent){
					int temp2 = this.transform.GetSiblingIndex();
					Vector2 tempv2 = (this.transform as RectTransform).anchoredPosition;
					if(clone.parent.name == "inv"){ //from backpack
						if(nme == ""){
							int temp = clone.GetSiblingIndex();
							Transform tempt = clone.parent.parent.parent;
							clone.parent = transform.parent;						
							tempt.GetComponent<backpack>().replaceslot(temp);
						} else {
							Transform tempt = clone.parent;
							int temp = clone.GetSiblingIndex();
							Vector2 v22 = (clone as RectTransform).anchoredPosition;
							clone.parent = transform.parent;
							transform.parent = tempt;
							transform.SetSiblingIndex(temp);
							(this.transform as RectTransform).anchoredPosition = v22;
						}
					} else {
						if(transform.parent.name == "inv"){ //empty backpack slot
							if(nme == ""){
								int temp = clone.GetSiblingIndex();
								Transform tempt = clone.parent;								
								clone.parent = transform.parent;
								if(tempt == master.MR.inv)master.MR.addinvitem(temp);
							} else {
								Transform tempt = clone.parent;
								int temp = clone.GetSiblingIndex();
								Vector2 v22 = (clone as RectTransform).anchoredPosition;
								clone.parent = transform.parent;
								transform.parent = tempt;
								transform.SetSiblingIndex(temp);
								(this.transform as RectTransform).anchoredPosition = v22;
							}
						} else {
							clone.parent = transform.parent;
						}
					}
					clone.SetSiblingIndex(temp2+1);
					(clone as RectTransform).anchoredPosition = tempv2;
					master.MR.itemup.itmref.transform.parent.gameObject.SetActive(false);
					yield return new WaitForEndOfFrame();
					master.MR.restackpickups();
					master.MR.doenemyturn(0);
					yield return new WaitForEndOfFrame();
					if(nme == ""){
						Destroy(this.gameObject);
					} else {
						if(clone.parent.name == "inv" || transform.parent.name == "inv"){
							
						} else {
							dropitem();
							//drop item
						}
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
		master.MR.state = 0;
		master.MR.clickevent = null;
		master.MR.itseld = null;
	}
	
	public virtual void dothething(){
		if(transform.parent == master.MR.inv){
			if(master.MR.clickevent == thisevent){
				master.MR.state = 0;
				master.MR.clickevent = null;
				master.MR.itseld = null;
			} else {
				master.MR.state = 1;
				master.MR.clickevent = thisevent;
				master.MR.itseld = this;
			}
		}
	}
	
	public virtual void onmove(){
		
	}
	
	public void punch(Transform t, item it){
		if(Vector3.Distance(t.position,player.pc.transform.position)<0.1f || Vector3.Distance(t.position,player.pc.transform.position)>1.5f){
			master.MR.state = 0;
			master.MR.clickevent = null;	
			master.MR.itseld = null;
		}
		else{
			Debug.Log("punch!");
			Collider[] cols = Physics.OverlapSphere(t.position, 0.25f, master.MR.entitymask);
			foreach(Collider c in cols){
				if(c.transform.parent.parent.GetComponent<entity>()){
					c.transform.parent.parent.GetComponent<entity>().takedamage(damage,0);
				}
			}
			master.MR.state = 0;
			master.MR.clickevent = null;
			master.MR.itseld = null;
			master.MR.doenemyturn(0);
		}
	}
}
