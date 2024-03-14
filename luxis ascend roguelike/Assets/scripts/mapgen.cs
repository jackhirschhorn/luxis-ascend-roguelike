using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapgen : MonoBehaviour
{
    public List<theme> themes = new List<theme>();
	public List<Transform> rooms = new List<Transform>();
	public Transform saferoom;
	
	public int floorsize = 5;
	
	public void generatefloorcall(){
		StartCoroutine(generatefloor());
	}
	
	public IEnumerator generatefloor(){
		yield return new WaitForEndOfFrame();
		//generate start room
		int curfloorsize = 1;
		while(curfloorsize < floorsize){
			
		}
	}
}
