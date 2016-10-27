using UnityEngine;
using System.Collections;

public class DisplayForward : MonoBehaviour {
    void OnDrawGizmos() {
        float lineLength = 0.5f;
        Vector3 targetPos = transform.position + (transform.forward * lineLength);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPos);
    }
}
