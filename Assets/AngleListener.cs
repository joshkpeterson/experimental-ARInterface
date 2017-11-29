using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class AngleListener : MonoBehaviour {

    private float fXRot;
    private float startTime;
    private float currentTime;

    public GameObject wishPrefab;
    public List<Wish> wishes = new List<Wish>();


    PostProcessingProfile m_Profile;

    float mapRange(float a1,float a2,float b1,float b2,float s)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }

	void Start () {
		var behaviour = GetComponent<PostProcessingBehaviour>();

        if (behaviour.profile == null)
        {
            enabled = false;
            return;
        }

        m_Profile = Instantiate(behaviour.profile);
        behaviour.profile = m_Profile;
	}
	
	void Update () {
        fXRot = Camera.main.transform.eulerAngles.x;
		// Debug.Log(fZRot);

        // Debug.Log(fXRot);

        var vignette = m_Profile.vignette.settings;
        // vignette.smoothness = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup) * 0.99f) + 0.01f;

        var color = m_Profile.colorGrading.settings;

        if (fXRot < 360 && fXRot > 270)
        {
            // Debug.Log(fXRot);

            // vignette.smoothness = 0.99f;
            vignette.smoothness = mapRange(360f, 270f, 0f, 1f, fXRot);
            color.channelMixer.red.x = mapRange(360f, 270f, 1f, 2f, fXRot);
            color.channelMixer.red.y = mapRange(360f, 270f, 0f, 2f, fXRot);
            // Debug.Log(color.channelMixer.red.x);
            // Debug.Log(color.channelMixer.red.x);



            // If we look up for longer than 2 seconds, do something
            if (fXRot < 315)
            {
                currentTime = Time.time;

                if (startTime < 0f)
                {
                    startTime = currentTime;
                }

                if (currentTime - startTime > 2.0f) {
                    Debug.Log("Looked up 2 seconds");

                    // if no wishes or if wish is not active


                    // Do this in start
                    // List<string> myList = new List<string>();
                    //  if list is not empty
                    if (wishes.Count > 0) {
                        wish = myList [ myList.Count-1 ];
                        // Do I need to getcomponent or nah?
                        if (!wish.GetComponent<WishController>.isActive) {
                            GameObject wish = Instantiate(wishPrefab, Camera.main.transform.forward + 1);
                            wishes.Add(wish);
                        }
                    }

                    


                    // Goes inside WishController:

                    // public Plane leftRightDivider;

                    // public Vector3 leftRightDividerNormal;

                    // leftRightDividerNormal = Vector3.Cross(Camera.main.transform.up, Camera.main.transform.forward);
                    // leftRightDivider.SetNormalandPosition(leftRightDividerNormal, Camera.main.transform.position);

                    // Later, on update:
                    // leftRightDivider.GetDistanceToPoint(Vector3 point);



                }
            }
            else 
            {
                startTime = -1;
            }






        }
        else
        {
            vignette.smoothness = 0f;
            // Debug.Log(color.channelMixer.red);
            color.channelMixer.red.x = 1f;
            color.channelMixer.red.y = 0f;

        }

        // Debug.Log(vignette.smoothness);

        m_Profile.vignette.settings = vignette;
        m_Profile.colorGrading.settings = color;


	}
}
