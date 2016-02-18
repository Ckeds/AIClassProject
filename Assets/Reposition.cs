using UnityEngine;
using System.Collections;

public class Reposition : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject);
        if(col.gameObject.name == "Sphere")
        this.transform.position = new Vector3(Random.Range(-15, 15),0, Random.Range(-15, 15));
    }
}
