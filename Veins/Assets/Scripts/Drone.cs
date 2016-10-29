using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour {
    //Structures
    public enum State { Idle, Moving, Working };

    //Public
    public Transform node, core, pathTarget;
    public State state;
    public Node.NodeData cargo;

    //Private
    NavMeshAgent navAgent;
    Node nodeScript;

    void Start() {
        //Get components
        navAgent = GetComponent<NavMeshAgent>();
        core = GameObject.Find("Core").transform;

        //Initialize
        cargo = new Node.NodeData();
        SetNode();
    }

    void Update() {
        //Check state
        if (state == State.Idle) {
            if (node != null) {
                //Move to target
                SetTarget(node.position);
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        if (state == State.Moving) {
            if (cargo.amount == 0) {
                if (other.gameObject == node.gameObject) {
                    //Start collecting node
                    navAgent.Stop();
                    Invoke("Work", 2);
                    state = State.Working;
                }
            }
            else {
                if (other.gameObject == core.gameObject) {
                    //Deliver node
                    navAgent.Stop();
                    cargo.amount = 0;
                    state = State.Idle;
                }
            }
        }

    }

    void Work() {
        //Recieve cargo
        int pickupAmount = Random.Range(1, Mathf.Min(nodeScript.contains.amount, 5));
        nodeScript.contains.amount -= pickupAmount;
        cargo.amount += pickupAmount;
        cargo.nodeName = nodeScript.contains.nodeName;

        //Return to core
        SetTarget(core.position);
    }

    void SetNode() {
        node = GameObject.Find("Ruby Node").transform;
        nodeScript = node.GetComponent<Node>();

        if (nodeScript.contains.amount == 0) {
            node = null;
            nodeScript = null;
        }
    }

    void SetTarget(Vector3 targetPos) {
        navAgent.SetDestination(targetPos);
        navAgent.Resume();
        state = State.Moving;
    }
}