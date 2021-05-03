using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableController : MonoBehaviour
{

    public Camera playerCamera;
    [SerializeField]
    private float interactRange = 10f;
    private bool equipped = false;
    private bool buttonCheck = false;
    private GameObject equipedObject;
    private RaycastHit hit;
    private int layerMask = 1 << 3;
    [SerializeField]
    private GameObject equipPosition;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float dropForwardForce = 10;
    [SerializeField]
    private float dropUpwardForce = 10;
    private HingeJoint joint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        manager();
    }
     public bool checkInteract()
    {
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward,out hit, interactRange, layerMask))
        {
            equipedObject = hit.collider.gameObject;
            return true;
        }
        return false;
    }
    public void manager()
    {
        //Debug.DrawRay(transform.position, gameObject.transform.forward * interactRange, Color.yellow);
        if (Input.GetKeyDown(KeyCode.E) && !equipped && !buttonCheck && checkInteract())
        {
            equipJoint();
            buttonCheck = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && equipped && !buttonCheck)
        {
            unequipJoint();
            buttonCheck = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && equipped && !buttonCheck)
        {
            throwing();
            buttonCheck = true;
        }

        if (!Input.GetKeyDown(KeyCode.E))
        {
            buttonCheck = false;
        }
    }

    public void equip()
    {
        equipedObject.transform.position = equipPosition.transform.position;
        equipedObject.transform.SetParent(player.transform);
        equipped = true;
        equipedObject.GetComponent<Rigidbody>().useGravity = false;
    }

    public void unequip()
    {
        equipedObject.transform.SetParent(null);
        equipedObject.GetComponent<Rigidbody>().useGravity = true;
        equipedObject = null;
        equipped = false;
    }


    public void throwing()
    {
        Destroy(equipedObject.GetComponent<HingeJoint>());
        equipedObject.GetComponent<Rigidbody>().useGravity = true;
        equipedObject.GetComponent<Rigidbody>().AddForce(transform.forward * dropForwardForce, ForceMode.Impulse);
        equipedObject.GetComponent<Rigidbody>().AddForce(transform.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        equipedObject.GetComponent<Rigidbody>().AddTorque(new Vector3(random, random, random) * 10);
        equipedObject = null;
        equipped = false;
    }

    public void equipJoint()
    {
        equipedObject.transform.position = equipPosition.transform.position;
        equipped = true;
        joint = equipedObject.AddComponent<HingeJoint>();
        configureJoint();
    }

    public void unequipJoint()
    {
        Destroy(equipedObject.GetComponent<HingeJoint>());
        equipedObject = null;
        equipped = false;
    }

    public void configureJoint()
    {
        JointSpring aux = new JointSpring();
        aux.spring = 100;
        aux.damper = 10;
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector3.right;
        joint.axis = new Vector3(1, 1, 1);
        joint.connectedAnchor = Vector3.zero;
        joint.useSpring = true;
        joint.spring = aux;
        joint.connectedBody = equipPosition.GetComponent<Rigidbody>();
    }

}
