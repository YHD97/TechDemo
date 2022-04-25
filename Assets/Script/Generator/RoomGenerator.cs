using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction{ up,down,left,right};
    public Direction direction;
    [Header("Room Information")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor,endColor;
    public LayerMask roomLayer;
    private GameObject endRoom;

    public GameObject player;
    public Transform generaorPlayerPoint;
    

    public int maxSetp;

    [Header("Position")]
    public Transform generaorPoint;
    public float xOffsie,yOffsize;
    

    public WallType walltype;

    public List<Room> rooms = new List<Room>();

    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player,generaorPlayerPoint.position,Quaternion.identity);

        for(int i =0; i<roomNumber;i++){
            rooms.Add(Instantiate(roomPrefab,generaorPoint.position,Quaternion.identity).GetComponent<Room>());
            //change room position
            ChangeRoomPosition();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        // find the final room
        endRoom = rooms[0].gameObject;
        foreach (var room in rooms)
        {
            // if(room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude){
            //     endRoom = room.gameObject;
            // }
            SetUpRoom(room,room.transform.position);
            
        }
        FindFinalRoom();
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangeRoomPosition(){
        direction = (Direction)Random.Range(0,4);
        while(Physics2D.OverlapCircle(generaorPoint.position,0.2f,roomLayer))
        {
            switch (direction)
            {
                case Direction.up:
                    generaorPoint.position += new Vector3(0,yOffsize,0);
                    break;
                case Direction.down:
                    generaorPoint.position += new Vector3(0,-yOffsize,0);
                    break;
                case Direction.left:
                    generaorPoint.position += new Vector3(-xOffsie,0,0);
                    break;
                case Direction.right:
                    generaorPoint.position += new Vector3(xOffsie,0,0);
                    break; 
            }

        }  

    }
    public void SetUpRoom(Room newRoom, Vector3 roomPosition){
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition+new Vector3(0,yOffsize,0),0.2f,roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition+new Vector3(0,-yOffsize,0),0.2f,roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition+new Vector3(-xOffsie,0,0),0.2f,roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition+new Vector3(xOffsie,0,0),0.2f,roomLayer);
        newRoom.UpdateRoom(xOffsie,yOffsize);

        switch (newRoom.doorNumber)
        {
            case 1:
                if(newRoom.roomUp){
                    Instantiate(walltype.singleUp,roomPosition,Quaternion.identity);
                }
                if(newRoom.roomDown){
                    Instantiate(walltype.singleDown,roomPosition,Quaternion.identity);
                }
                if(newRoom.roomLeft){
                    Instantiate(walltype.singleLeft,roomPosition,Quaternion.identity);
                }
                if(newRoom.roomRight){
                    Instantiate(walltype.singleRight,roomPosition,Quaternion.identity);
                }
                break;
              
            case 2:
                if(newRoom.roomLeft && newRoom.roomUp){
                    Instantiate(walltype.doubleLU,roomPosition,Quaternion.identity);
                }
                if(newRoom.roomLeft && newRoom.roomRight){
                    Instantiate(walltype.doubleLR,roomPosition,Quaternion.identity);
                }
                if(newRoom.roomLeft && newRoom.roomDown ){
                    Instantiate(walltype.doubleLD,roomPosition,Quaternion.identity);
                }
                if(newRoom.roomUp && newRoom.roomRight){
                    Instantiate(walltype.doubleUR,roomPosition,Quaternion.identity);
                }
                if(newRoom.roomUp && newRoom.roomDown){
                    Instantiate(walltype.doubleUD,roomPosition,Quaternion.identity);
                }
                if(newRoom.roomRight && newRoom.roomDown){
                    Instantiate(walltype.doubleRD,roomPosition,Quaternion.identity);
                }
                break;
            case 3: 
                if(!newRoom.roomDown){
                    Instantiate(walltype.tripleLUR,roomPosition,Quaternion.identity);
                }
                if(!newRoom.roomRight){
                    Instantiate(walltype.tripleLUD,roomPosition,Quaternion.identity);
                }
                if(!newRoom.roomLeft){
                    Instantiate(walltype.tripleURD,roomPosition,Quaternion.identity);
                }
                if(!newRoom.roomUp){
                    Instantiate(walltype.tripleLRD,roomPosition,Quaternion.identity);
                }
                break;
            case 4:
                if(newRoom.roomLeft && newRoom.roomUp && newRoom.roomRight && newRoom.roomDown){
                    Instantiate(walltype.fourDoors,roomPosition,Quaternion.identity);
                }
                break;

           
        }
    }

    public void FindFinalRoom(){

        //get max value
        for (int i = 0; i < rooms.Count; i++)
        {
            if(rooms[i].stepToStart > maxSetp){
                maxSetp = rooms[i].stepToStart;
            }
            
        }
        //get the max value room and less value room 
        foreach (var room in rooms)
        {
            if(room.stepToStart == maxSetp){
                farRooms.Add(room.gameObject);
            }
            if(room.stepToStart == maxSetp-1){
                lessRooms.Add(room.gameObject);
            }
        }

        for (int i = 0; i < farRooms.Count; i++)
        {
            if(farRooms[i].GetComponent<Room>().doorNumber ==1){
                oneWayRooms.Add(farRooms[i]);
            }
        }

        for (int i = 0; i < lessRooms.Count; i++)
        {
            if(lessRooms[i].GetComponent<Room>().doorNumber ==1){
                oneWayRooms.Add(lessRooms[i]);
            }
        }

        if(oneWayRooms.Count != 0){
            endRoom = oneWayRooms[Random.Range(0,oneWayRooms.Count)];
        }
        else{
            endRoom = farRooms[Random.Range(0,oneWayRooms.Count)];
        }

    }

}

[System.Serializable]
public class WallType {
    public GameObject singleUp,singleDown,singleLeft,singleRight,

                    doubleLU,doubleLR,doubleLD,doubleUR,doubleUD,doubleRD,
                    tripleLUR,tripleLUD,tripleURD,tripleLRD,
                    fourDoors;
    
}
