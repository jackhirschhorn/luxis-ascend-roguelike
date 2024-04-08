using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backpackdesc : itemrangefinder
{
   public override void showrange(bool b){ //standard melee
		if(showtooltip && transform.parent == master.MR.inv)master.MR.showtooltip(transform.GetComponent<item>(),b);
	}
}
