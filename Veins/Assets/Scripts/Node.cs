using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
    //Structures
    [System.Serializable]
    public class NodeData {
        public string nodeName;
        public int amount;
    }

    //Public
    public NodeData contains;
}