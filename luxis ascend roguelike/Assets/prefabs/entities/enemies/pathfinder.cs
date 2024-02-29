using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinder : ScriptableObject
{
    public virtual Vector3 findpath(entity e, entity me){
		return Vector3.zero;
	}
	
	public virtual Vector3 wander(entity me){
		return Vector3.zero;
	}
}
