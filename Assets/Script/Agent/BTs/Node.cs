using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum BehaviourState{Running,Failure,Success};
    public  BehaviourTree Tree{get;set;}
    public Node(BehaviourTree tree){
        Tree = tree;
    }

    public virtual BehaviourState Execute(){
        return BehaviourState.Failure;
    }

}
