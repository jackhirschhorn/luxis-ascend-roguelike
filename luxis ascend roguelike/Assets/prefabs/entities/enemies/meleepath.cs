using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="meleepath")]
public class meleepath : pathfinder
{
    public override Vector3 findpath(entity e, entity me){
		return Vector3.zero;
	}
	
	public override Vector3 wander(entity me){		
		int max = 0;
		bool[] moveable = new bool[9];
		for(int i = 1; i < 10; i++){
			if(i == 4){
				i++;
				moveable[3] = false;
			}
			Vector3 dir = new Vector3(i<4?1:(i>6?-1:0),0,(i%3==0?-1:(i%2==0?0:1)));
			dir = dir.normalized;
			RaycastHit hit;
			if(Physics.SphereCast(me.transform.position, 0.1f, dir,out hit, 1f)){
				moveable[i-1] = false;
			} else {
				moveable[i-1] = true;
				max++;
			}
		}
		if(max == 0)return Vector3.zero;
		int dirgo2 = Random.Range(0,max);
		int dirgo = -1;
		while(dirgo2 >= 0){
			dirgo++;
			while(!moveable[dirgo]){
				dirgo++;
			}
			dirgo2--;
		}
		dirgo++;
		return me.transform.position + new Vector3(dirgo<4?1:(dirgo>6?-1:0),0,(dirgo%3==0?-1:(dirgo%2==0?0:1)));
	}
}
