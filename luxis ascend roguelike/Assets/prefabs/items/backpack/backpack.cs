using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backpack : item
{
	public bool on = false;
	public Transform inv;
	public Transform backpackslot;
	
	public override void onmove(){
		on = true;
		togglepack();
	}
	
	public override void dothething(){
		//handled by clicking
	}
	
	public override void dropitem(){
		on = true;
		togglepack();
		itmref.transform.parent.gameObject.SetActive(true);
		itmref.transform.parent.GetChild(1).position = player.pc.transform.position;
		transform.parent = itmref.transform.parent;
	}
	
    public void togglepack(){		
		on = !on;
		if(on && transform.parent == master.MR.inv){ // open
			transform.GetChild(0).gameObject.SetActive(true);
		} else {
			on = false;
			transform.GetChild(0).gameObject.SetActive(false);
		}
	}
	
	public override IEnumerator pickup2(){
		yield return new WaitForEndOfFrame();
		if(master.MR.itemup != null){
			if(on){
				
			} else {
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
	}
	
	public virtual void replaceslot(int i2){
		RectTransform clone = Instantiate(backpackslot as RectTransform);
		clone.parent = inv;
		clone.SetSiblingIndex(i2);
		int temp = inv.childCount+1;
		for(int i = inv.childCount-1; i > -1; i--){
			//StartCoroutine(animateinv((inv.GetChild(i) as RectTransform),new Vector2(Screen.width*((i+1f)/temp),58),inv.childCount-i+5));
			(inv.GetChild(i) as RectTransform).anchoredPosition = new Vector2(35*((i*2)+1),51);
		}
	}
}
