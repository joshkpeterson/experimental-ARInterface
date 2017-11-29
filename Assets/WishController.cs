using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishController : MonoBehaviour {

    public Plane leftRightDivider;

    public Vector3 leftRightDividerNormal;
    public Vector3 leftRightDistance;

	// Use this for initialization
	void Start () {
        leftRightDividerNormal = Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward);
        leftRightDivider.SetNormalandPosition(leftRightDividerNormal, Camera.main.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        leftRightDistance = leftRightDivider.GetDistanceToPoint(Camera.main.transform.position);
	}
}
