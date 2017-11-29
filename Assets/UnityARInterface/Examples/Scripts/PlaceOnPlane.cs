using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;

public class PlaceOnPlane : ARBase
{

    // public InstantiateWorld instantiateWorld;

    [SerializeField]
    private Transform m_ObjectToPlace;
    private bool hasPlaced = false;

    private Vector3 worldCenter;

    public GameObject starParentPrefab;
    public GameObject world;

    private

    void Update ()
    {
        if (Input.GetMouseButton(0))

        {
            // Debug.Log(hasPlaced);
            var camera = GetCamera();

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

			int layerMask = 1 << LayerMask.NameToLayer("ARGameObject"); // Planes are in layer ARGameObject

            RaycastHit rayHit;
            if (!hasPlaced && Physics.Raycast(ray, out rayHit, float.MaxValue, layerMask))
            {
                worldCenter = rayHit.point;
                // instantiateWorld(rayHit.point);
                m_ObjectToPlace.transform.position = worldCenter;
                TriggerStars();
                hasPlaced = true;
            }
        }
    }

    void TriggerStars ()
    {
        Debug.Log("got it");
        InvokeRepeating("InstantiateStar", 1.0f, 1.0f);
    }

    void InstantiateStar ()
    {
        //  make two random values here
        float offsetX = 0.1f - Random.value * 0.2f;
        float offsetZ = 0.1f - Random.value * 0.2f;
        // offsetX = 0;
        // offsetZ = 0;
        Vector3 offset = new Vector3 (offsetX, 0, offsetZ);

        Debug.Log(offset);


        // GameObject star = Instantiate(starParentPrefab, world.transform + offset);
        GameObject star = Instantiate(starParentPrefab, world.transform);
        star.transform.localPosition = offset;

        Animation animation = star.transform.GetChild(0).GetComponent<Animation>();
        // Debug.Log(animation);

        animation["FallingStar"].wrapMode = WrapMode.Once;
        animation.Play("FallingStar");
    }
}
