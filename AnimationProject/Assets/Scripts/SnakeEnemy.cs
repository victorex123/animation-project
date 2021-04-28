using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : MonoBehaviour
{
    public List<Transform> BodyParts = new List<Transform>();

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

        BodyParts[0].Translate(BodyParts[0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);

        if (Input.GetAxis("Horizontal") != 0)
        {
            BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

        for (int i = 1; i < BodyParts.Count; i++)
        {
            currentBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            dis = Vector3.Distance(prevBodyPart.position, currentBodyPart.position);

            Vector3 newpos = prevBodyPart.position;
            newpos.y = BodyParts[0].position.y;

            float T = Time.deltaTime * dis / distanceBody * currentSpeed;
            if (T > 0.5f)
                T = 0.5f;

            currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newpos, T);
            currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        Transform newpart = Instantiate(bodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation).transform;
        newpart.SetParent(transform);
        BodyParts.Add(newpart);
    }
}
