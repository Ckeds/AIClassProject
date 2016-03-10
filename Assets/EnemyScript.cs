using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

    public GameObject explosionPrefab;

    private MeshRenderer attractFlag;

    public float counter = 0;
    private float findInterval = .25f;
    private bool attracted = false;
    public bool Attracted
    {
        get { return attracted; }
    }
    private bool stunned = false;

    public float speed = 4;
    public float Speed
    {
        get
        {
            if (stunned)
                return 0;
            else if (!Attracted)
                return speed;
            else
                return speed * .25f;
        }
        set { speed = value; }
    }
    public float accel;
    public float Accel
    {
        get
        {
            if (stunned)
                return 0;
            else if (!Attracted)
                return accel;
            else
                return accel * 2f;
        }
        set { accel = value; }
    }

    public Vector3 target;

    // Use this for initialization
    void Start()
    {
        FindTarget();
        attractFlag = null;
        //attractFlag.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        if ((target - transform.position).magnitude < 20)
        {
            counter += Time.deltaTime;
            if (counter > 0)
            {
                counter = -findInterval;
                //attractFlag.enabled = false;
                attracted = false;
                stunned = false;
            }

            Vector3 delta = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);

            //GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, target, 7));
            delta.Normalize();
            delta *= Accel;
            GetComponent<Rigidbody>().AddForce(delta, ForceMode.Impulse);
            if (GetComponent<Rigidbody>().velocity.magnitude > Speed)
            {
                Vector3 slow = -GetComponent<Rigidbody>().velocity;
                slow.Normalize();
                slow *= GetComponent<Rigidbody>().velocity.magnitude - Speed;

                GetComponent<Rigidbody>().AddForce(slow, ForceMode.Impulse);
            }

        }

    }

    void OnCollisionEnter(Collision coll)
    {

    }

    private void FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
		{
			float distance = 10000;
			for(int i = 0; i < players.Length; i++)
			{
				Vector3 deltaDist = players[i].transform.position - this.transform.position;
				float m_distance = Mathf.Abs(deltaDist.x) + Mathf.Abs(deltaDist.z);
				if(m_distance < distance)
				{
					target = players[i].transform.position;
					distance = m_distance;
				}
			}
		}
        else
        {
            //Explode();
        }
    }

    public void AttractTo(Vector3 point)
    {
        target = point;
        GetComponent<NetworkView>().RPC("AttractedTo", RPCMode.All, GetComponent<NetworkView>().viewID);
    }

}
