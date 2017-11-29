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
    public List<WishController> wishes = new List<WishController>();


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
            vignette.smoothness = mapRange(360f, 270f, 0f, 1f, fXRot);
            color.channelMixer.red.x = mapRange(360f, 270f, 1f, 2f, fXRot);
            color.channelMixer.red.y = mapRange(360f, 270f, 0f, 2f, fXRot);


            // If we look up for longer than 2 seconds, create a wish
            if (fXRot < 315)
            {
                currentTime = Time.time;

                if (startTime < 0f)
                {
                    startTime = currentTime;
                }

                if (currentTime - startTime > 2.0f) {
                    Debug.Log("Looked up 2 seconds");

                    if (wishes.Count > 0) {
                        wish = myList[myList.Count - 1];
                        // Do I need to getcomponent or nah?
                        if (!wish.GetComponent<WishController>.isActive) {
                            GameObject wish = Instantiate(wishPrefab, Camera.main.transform.forward + 1);
                            wishes.Add(wish);
                        }
                    }
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
