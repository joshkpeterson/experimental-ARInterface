using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class AngleListener : MonoBehaviour {

    private float fXRot;

    PostProcessingProfile m_Profile;

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
            vignette.smoothness = 0.99f;
        } else {
            vignette.smoothness = 0.01f;
        }

        Debug.Log(vignette.smoothness);

        m_Profile.vignette.settings = vignette;

	}
}
