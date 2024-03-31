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
	public int whattheme = 0;
	
	public void generatefloorcall(){
		StartCoroutine(generatefloor());
	}
	
	public IEnumerator generatefloor(){
		yield return new WaitForEndOfFrame();
		//generate start room
		Time.timeScale = 1f;
		Transform clone = Instantiate(saferoom);
		clone.parent = map;
		clone.position = new Vector3(0,0,0);
		int curfloorsize = 1;
		//generate rest of rooms
		while(curfloorsize < floorsize){
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			clone = Instantiate(rooms[Random.Range(0,rooms.Count)],new Vector3(0,-30,0),rooms[Random.Range(0,rooms.Count)].rotation);
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
				yield return new WaitForEndOfFrame();
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
		
		//populate rooms
		foreach(Transform c in map){
			//doors
			roomgrid grd = c.GetComponent<roomgrid>();
			List<Vector3> veccheck = new List<Vector3>();
			Vector3 vecpar = new Vector3();
			foreach(List<Vector2> vl in grd.finddoorchecks()){
				if(vl.Count != 0){
					vecpar = new Vector3(vl[0].x*6,0,-vl[0].y*6);
					for(int i = 1; i < vl.Count;i++){					
						veccheck.Add(new Vector3(vl[i].x*6,0,-vl[i].y*6));
					}
					for(int i = 0; i < veccheck.Count;i++){
						Collider[] col = Physics.OverlapSphere(c.position+veccheck[i], 1, lm);
						if(col.Length != 0 && col[0].transform.parent.parent != c){
							//Debug.Log(Physics.OverlapSphere(c.position+veccheck[i], 1, lm).Length + " placed");
							//place door
							Transform clone2 = Instantiate(themes[whattheme].doors[Random.Range(0,themes[whattheme].doors.Count)]);
							clone2.parent = c.GetChild(1);
							clone2.position = c.position+(vecpar+(veccheck[i]-vecpar)/2);
							//rotate door model
							if(vecpar.x < veccheck[i].x+0.05f && vecpar.x > veccheck[i].x-0.05f){
								clone2.rotation = Quaternion.Euler(0,90*(vecpar.z < veccheck[i].z?0:2),0);
							} else {
								clone2.rotation = Quaternion.Euler(0,90*(vecpar.x < veccheck[i].x?1:3),0);
							}
							c.GetComponent<roomcontrol>().doors.Add(clone2.GetChild(2).GetComponent<door>());
						}
					}
				}
				veccheck.Clear();
				yield return new WaitForEndOfFrame();
			}
			
		}
		yield return new WaitForEndOfFrame();		
		foreach(Transform c in map){
			//tiles
			foreach(Transform c2 in c.GetChild(0)){
				Transform clone2 = Instantiate(themes[whattheme].tiles[int.Parse(c2.gameObject.tag)]);
				clone2.parent = c2;
				clone2.position = c2.position;
			}
			
			//walls
			for(int i = 0; i < c.GetChild(2).childCount; i++){
				//place a corner at each piece, then a wall between them if it doesn't touch anything else
					Transform c3 = c.GetChild(2).GetChild(i);
					Transform c4 = (i == c.GetChild(2).childCount-1?c.GetChild(2).GetChild(0):c.GetChild(2).GetChild(i+1));
					Transform clone2;
					if(Physics.OverlapSphere(c3.position, 0.1f, master.MR.wallonlymask).Length == 0){
						clone2 = Instantiate(themes[whattheme].wallconnectors[Random.Range(0,themes[whattheme].wallconnectors.Count)]);
						clone2.parent = c3;
						clone2.position = c3.position+new Vector3(0,0.5f,0);
					}
					int tempdist = (int)Vector3.Distance(c3.position, c4.position);
					bool rotit = (c3.position.x == c4.position.x?true:false);
					for(int i2 = 1; i2 < tempdist; i2++){
						Vector3 tempplace = Vector3.Lerp(c3.position,c4.position, (i2+0f)/(tempdist+0f));
						Vector3 theplace = new Vector3(Mathf.FloorToInt(tempplace.x+0.5f),0,Mathf.FloorToInt(tempplace.z+0.5f));
						if(Physics.OverlapSphere(theplace, 0.25f, master.MR.wallonlymask).Length == 0){
							clone2 = Instantiate(themes[whattheme].walls[Random.Range(0,themes[whattheme].walls.Count)]);
							clone2.parent = c3;
							clone2.position = theplace+new Vector3(0,0.5f,0);
							if(rotit)clone2.rotation = Quaternion.Euler(0,90,0);
						}
					}
					yield return new WaitForEndOfFrame();		
			}
			//deco
			foreach(Transform c5 in c.GetChild(3)){
				Transform clone2 = Instantiate(themes[whattheme].deco[Random.Range(0,themes[whattheme].deco.Count)]);
				clone2.parent = c5;
				clone2.position = c5.position + new Vector3(0,0.5f,0);
			}
			//enemies
			foreach(Transform c6 in c.GetChild(4)){
				List<Transform> templst = new List<Transform>();
				switch(c6.gameObject.tag){
					case "1":
						templst = themes[whattheme].enemy_melee;
						break;
					case "2":
						templst = themes[whattheme].enemy_ranged;
						break;
					case "3":
						templst = themes[whattheme].enemy_heavy;
						break;
					case "4":
						templst = themes[whattheme].enemy_magic;
						break;
					case "5":
						templst = themes[whattheme].enemy_fast;
						break;
					default:
						Debug.Log("UNTAGGED ENEMY IN" + c6.parent);
						break;
				}
				if(templst.Count != 0){
					Transform clone2 = Instantiate(templst[Random.Range(0,templst.Count)]);
					clone2.parent = c6;
					clone2.position = c6.position;
					clone2.parent = master.MR.entrans;
					clone2.GetComponent<enemy>().rc = c.GetComponent<roomcontrol>();
					c.GetComponent<roomcontrol>().enmy.Add(clone2.GetComponent<enemy>());
				}
			}
			yield return new WaitForEndOfFrame();
		}
		foreach(Transform c in map){
			c.GetComponent<roomcontrol>().go = true;
		}
	}
}
