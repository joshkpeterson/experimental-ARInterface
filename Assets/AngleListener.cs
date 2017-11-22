using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class AngleListener : MonoBehaviour {

    private float fXRot;

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

        Debug.Log(fXRot);

        var vignette = m_Profile.vignette.settings;
        // vignette.smoothness = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup) * 0.99f) + 0.01f;

        if (fXRot < 340 && fXRot > 270) {
            // vignette.smoothness = 0.99f;
            vignette.smoothness = mapRange(340f, 270f, 0f, 1f, fXRot);

        } else {
            vignette.smoothness = 0f;
        }

        Debug.Log(vignette.smoothness);

        m_Profile.vignette.settings = vignette;

	}
}
