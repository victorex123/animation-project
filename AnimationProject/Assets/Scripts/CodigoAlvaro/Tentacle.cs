using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    [SerializeField]
    private List <Vector3> originalPoints;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private int niterations;
    [SerializeField]
    private int length;
    //List<Vector3> aux;
    // Start is called before the first frame update
    void Start()
    {
        points = transform.GetComponentsInChildren<Transform>();
        for (int x = 0; x < points.Length; x++)
        {
            if (x + 1 != points.Length)
            {
                points[x] = points[x + 1];
                originalPoints.Add(points[x].position);
                //aux.Add(points[x].position);
            }
            else
            {
                points[x] = null;
            }

        }
       

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.transform.position,originalPoints[0]) <= length * originalPoints.Count)
        {
            for (int i = 0; i < niterations; i++)
            {
                originalPoints = secondPart(firstPart(originalPoints));
            }
            for (int i = 0; i < points.Length - 1; i++)
            {
                points[i].position = originalPoints[i];
            }
        }
    }

    public List<Vector3> firstPart(List<Vector3> original)
       {
        for (int i = original.Count - 1; i >= 0; i--)
        {
            if(i != original.Count - 1)
            {
                Vector3 result1 = (original[i] - original[i + 1]).normalized;
                float result2 = length;
                //original[i] = (original[i + 1] - original[i]).normalized * Vector3.Distance(points[i + 1].position,points[i].position);
                original[i] = original[i + 1] + result1 * result2;
            }
            else
            {
                original[original.Count - 1] = target.transform.position;
            }
        }
        return original;
    }
    public List<Vector3> secondPart(List<Vector3> original)
    {
        for (int i = 0; i < original.Count; i++)
        {
            if(i == 0)
            {
                original[0] = points[0].position;
                points[original.Count - 1].forward = (target.transform.position - points[original.Count - 1].position).normalized;
            }
            else
            {
                Vector3 result1 = (original[i] - original[i - 1]).normalized;
                float result2 = length;
                //original[i] = (original[i] - original[i - 1]).normalized * Vector3.Distance(points[i - 1].position, points[i].position);
                original[i] = original[i - 1] + result1 * result2;
                points[i - 1].forward = result1;
            }
        }
        return original;
    }
}
