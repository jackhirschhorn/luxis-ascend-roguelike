using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapgen : MonoBehaviour
{
    public List<theme> themes = new List<theme>();
	public List<Transform> rooms = new List<Transform>();
	public Transform saferoom;
	public Transform map;
	public LayerMask lm;
	
	public int floorsize = 5;
	
	public void generatefloorcall(){
		StartCoroutine(generatefloor());
	}
	
	public IEnumerator generatefloor(){
		yield return new WaitForEndOfFrame();
		//generate start room
		Transform clone = Instantiate(saferoom);
		clone.parent = map;
		clone.position = new Vector3(0,0,0);
		int curfloorsize = 1;
		//generate rest of rooms
		while(curfloorsize < floorsize){
			yield return new WaitForEndOfFrame();
			clone = Instantiate(rooms[Random.Range(0,rooms.Count)]);
			clone.position = new Vector3(0,-10,0);
			int rando1 = Random.Range(2,5);
			Vector2 connectfind = clone.GetComponent<roomgrid>().findcell(rando1);
			while(connectfind == new Vector2(10,10)){
				rando1 = Random.Range(2,5);
				connectfind = clone.GetComponent<roomgrid>().findcell(rando1);
				Debug.Log(connectfind);
				yield return new WaitForEndOfFrame();
			}
			bool free = true;
			int rando2 = (rando1 == 2?3:(rando1 == 3?2:(rando1 == 4?5:4)));
			Transform par;
			Vector2 connectfind2;
			do{
				do{
					par = map.GetChild(Random.Range(0,map.childCount));
					connectfind2 = par.GetComponent<roomgrid>().findcell(rando2);
					yield return new WaitForEndOfFrame();
				}while(connectfind2 == new Vector2(10,10));
				
				int offsetx = (int)(-connectfind.x+connectfind2.x+((-connectfind.x+connectfind2.x >= 0.95f || -connectfind.x+connectfind2.x <= -0.95f) && ((int)connectfind.x != 0 && (int)connectfind2.x != 0)?(-connectfind.x+connectfind2.x > 0?-1:1):0));
				int offsety = (int)(connectfind.y-connectfind2.y+((connectfind.y-connectfind2.y >= 0.95f || connectfind.y-connectfind2.y <= -0.95f)&& ((int)connectfind.y != 0 && (int)connectfind2.y != 0)?(connectfind.y-connectfind2.y > 0?-1:1):0));
				
				List<Vector2> tempvecs = clone.GetComponent<roomgrid>().findcells(1);
				free = true;
				foreach(Vector2 v2 in tempvecs){
					yield return new WaitForEndOfFrame();
					Vector3 placecheck = par.position+new Vector3((offsetx*6),0,(offsety*6))+new Vector3(v2.x*6,0,-v2.y*6);
					//Debug.DrawLine(placecheck, placecheck+(Vector3.forward*curfloorsize),(curfloorsize%5==1?Color.red:(curfloorsize%5==2?Color.blue:(curfloorsize%5==3?Color.green:(curfloorsize%5==4?Color.black:Color.white)))), 2, false);
					if(Physics.OverlapSphere(placecheck, 1, lm).Length != 0){
						free = false;
						break;
					}
				}
				if(free){
					clone.parent = map;
					clone.position = par.position+new Vector3((offsetx*6),0,(offsety*6));
					clone.GetComponent<roomgrid>().emptycell(connectfind);
					par.GetComponent<roomgrid>().emptycell(connectfind2);
					curfloorsize++;
				}
				yield return new WaitForEndOfFrame();					
			} while (!free);
		}
	}
}
