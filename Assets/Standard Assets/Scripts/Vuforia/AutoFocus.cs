using UnityEngine;
using System.Collections;

public class AutoFocus : MonoBehaviour
{
    // Use this for initialization  
    void Start()
    {
        Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    int m_frame = 0;

    // Update is called once per frame  
    void Update()
    {
        if (m_frame > 10)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0))
#elif UNITY_ANDROID || UNITY_IPHONE
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)  
#endif
            {
                Vuforia.CameraDevice.Instance.SetFocusMode(Vuforia.CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            }
            m_frame = 0;
        }
        m_frame++;
    }
}