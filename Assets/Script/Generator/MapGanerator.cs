using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGanerator : MonoBehaviour
{
    public int width;
	public int height;
    public Transform mapPosition;

    public int smoothingCycle;
    //public Transform generaorPoint;

	[Range(0,100)]
	public int randomFillPercent;

    [Range(0,80)]
	public int threshold;

	int[,] cavePoints;

    public GameObject stone;
    public GameObject wall;
    private void Awake() {
        GenerateMap();
    }
    // Start is called before the first frame update
    void Start()
    {
    Vector2 mapPos= new Vector2(mapPosition.transform.position.x,mapPosition.transform.position.y);
       PlaceGird(mapPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateMap() {
		cavePoints = new int[width,height];
		int seed = Random.Range(0,1000000);
        System.Random randomSeed = new System.Random(seed.GetHashCode());
		
        for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
                //wall 
                if(x<5||y<5|| x> width-10 || y >height-10){
                    cavePoints[x,y] = 0;
                }
                //stone
                else if(randomSeed.Next(0,100)<randomFillPercent){
                    cavePoints[x,y] = 1;
                }
                else{
                    cavePoints[x,y] = 0;
                }
				
			}
		}

        for (int i = 0; i < smoothingCycle; i++)
        {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    int newNeighborWalls = GetNeighbors(x,y);
                    if(newNeighborWalls >threshold){
                        cavePoints[x,y] = 1;
                    }else if(newNeighborWalls <threshold){
                        cavePoints[x,y] = 0;
                    }
                }
		    }
            
        }
	}

    private int GetNeighbors(int pointx,int pointy){
        int wallNeighbors = 0;
        for (int x = pointx-1; x <= pointx+1; x++)
        {
            for (int y = pointy-1; y <= pointy+1; y++)
            {
                if(x>=0 && x < width && y>=0 && y <height)
                {
                    if(x != pointx || y!= pointy){
                        if(cavePoints[x,y] == 1 || cavePoints[x,y] == 2){
                            wallNeighbors++;
                        }
                    }
                    
                }
                else
                {
                    wallNeighbors++;
   
                }
            }
            
        }
        return wallNeighbors;
        

    }

    private void PlaceGird(Vector2 mapPosition){

        for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
                if(cavePoints[x,y] == 1){
                    Instantiate(stone,new Vector2(mapPosition.x+ x,mapPosition.y+y),Quaternion.identity);
                }
                
				
			}
		}
    }
}
