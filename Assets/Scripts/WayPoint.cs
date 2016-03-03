using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour {

    public GameObject nextA;
    public GameObject nextB;
    public GameObject next;
    public Vector3 start, end, segment, unitVec;
    float mag;

	// Use this for initialization
	void Start () {
        start = transform.position;
        if (Random.Range(0, 5) > 1)
        {
            end = nextA.transform.position;
            next = nextA;
        }
        else {
            end = nextB.transform.position;
            next = nextB;
        }
        segment = end - start;
        mag = segment.magnitude;
        unitVec = segment.normalized;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(start, end, Color.green);
	}

    public Vector3 closestPoint(Vector3 pt)
    {
        Vector3 startToPt = pt - start;
        float projection = Vector3.Dot(startToPt, unitVec);
        if (projection<= 0)
        {
            return start;
        }
        if (projection >= mag)
        {
            return end;
        }

        return start + unitVec * projection;
    }
}
