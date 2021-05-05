using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : MonoBehaviour
{
    public List<GameObject> BodyParts = new List<GameObject>();

    public float distanceBody = 0.25f;

    public int beginSize;

    public float moveSpeed = 1;
    public float rotationSpeed = 50;

    public GameObject bodyPrefab;

    private float dis;
    private Transform currentBodyPart;
    private Transform prevBodyPart;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<beginSize-1;i++)
        {
            AddBodyPart();
        }
    }

    void Update()
    {
        Move();

        if(Input.GetKey(KeyCode.Q))
        {
            AddBodyPart();
        }
    }

    // Update is called once per frame
    void Move()
    {
        float currentSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed *= 2;
        }

        //BodyParts[0].transform.Translate(BodyParts[0].transform.forward * currentSpeed * Time.smoothDeltaTime, Space.World);
        BodyParts[0].GetComponent<Rigidbody>().AddForce(BodyParts[0].transform.forward * currentSpeed * Time.smoothDeltaTime);

        if (Input.GetAxis("Horizontal") != 0)
        {
            BodyParts[0].transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

        for (int i = 1; i < BodyParts.Count; i++)
        {
            currentBodyPart = BodyParts[i].transform;
            prevBodyPart = BodyParts[i - 1].transform;

            dis = Vector3.Distance(prevBodyPart.position, currentBodyPart.position);

            Vector3 newpos = prevBodyPart.position;
            newpos.y = BodyParts[0].transform.position.y;

            float T = Time.deltaTime * dis / distanceBody * currentSpeed;
            if (T > 0.5f)
                T = 0.5f;

            currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newpos, T);
            currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        GameObject newpart = Instantiate(bodyPrefab, BodyParts[BodyParts.Count - 1].transform.position, BodyParts[BodyParts.Count - 1].transform.rotation);
        newpart.transform.SetParent(transform);
        BodyParts.Add(newpart);
    }
}
