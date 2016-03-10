using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    GameObject self;
    Rigidbody body;
    public float speed;
    float rotation;

	// Use this for initialization
	void Start () {
        self = GameObject.FindGameObjectWithTag("Player");
        body = self.GetComponent<Rigidbody>();
        rotation = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
           rotation -= 1.0f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation += 1.0f;
        }
        self.transform.rotation = Quaternion.Euler(0, rotation, 0);

        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = (Quaternion.Euler(0, rotation, 0)) * (new Vector3(speed, body.velocity.y, body.velocity.z));
        }
        if (Input.GetKey(KeyCode.W))
        {
            body.velocity = (Quaternion.Euler(0, rotation, 0)) * (new Vector3(body.velocity.x, body.velocity.y, speed));
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = (Quaternion.Euler(0, rotation, 0)) * (new Vector3(-speed, body.velocity.y, body.velocity.z));
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.velocity = (Quaternion.Euler(0, rotation, 0)) * (new Vector3(body.velocity.x, body.velocity.y, -speed));
        }
    }
}
