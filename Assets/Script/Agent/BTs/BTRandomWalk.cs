using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRandomWalk : Node
{  
    protected Vector3 NextDestination {get;set;}
    public float speed = 10;
    public Transform Player;

    public BTRandomWalk(BehaviourTree tree):base(tree){
        NextDestination = Vector3.zero;
        FindNextDestination();
    }
    public bool FindNextDestination(){
        object o;
        bool found = false;
        found = Tree.BlackBoard.TryGetValue("world",out o);
        if(found){
            Rect bounds = (Rect)o;
            float x = UnityEngine.Random.value*bounds.width;
            float y = UnityEngine.Random.value*bounds.height;
            NextDestination = new Vector3(x,y,NextDestination.z);
        }
        return found;
    }

    public override BehaviourState Execute(){
        if(Tree.gameObject.transform.position == NextDestination){
            if(!FindNextDestination()){
                return BehaviourState.Failure;
            }else{
                return BehaviourState.Success;
            }
        }else{
            Player = GameObject.FindWithTag("Player").transform;
            Vector3 displacement = Player.position -Tree.gameObject.transform.position;
            displacement = displacement.normalized;
            if (Vector2.Distance (Player.position, Tree.gameObject.transform.position) < 30.0f) {
                Tree.gameObject.transform.position += (displacement * speed * Time.deltaTime);
                            
            }else{
                Tree.gameObject.transform.position = Vector3.MoveTowards(Tree.gameObject.transform.position,NextDestination,Time.deltaTime*speed);

            }
            return BehaviourState.Running;
        }
        
    }

    
}
