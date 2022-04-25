using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : Node
{
    public Node Child{get;set;}
    public Decorator(BehaviourTree tree, Node child):base(tree){
        Child = child;
    }
    
    
}
