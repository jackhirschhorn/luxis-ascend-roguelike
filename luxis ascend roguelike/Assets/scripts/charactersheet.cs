using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactersheet : MonoBehaviour
{
    public List<resourceopt> recs = new List<resourceopt>();
	public int str, con, dex, agi, inte, wis, lck;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public bool docost(resourceopt ro, int level, bool check){
		for(int i = 0; i < recs.Count; i++){
			if(recs[i].id == ro.id){
				if(recs[i].amnt > (ro.amnt + (ro.max*level-1))){
					if(!check)recs[i].amnt -= ro.amnt + (ro.max*level-1);
					return true;
				} 
				return false;
			}
		}
		return false;
	}
}
