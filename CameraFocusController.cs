using UnityEngine;
using System.Collections;
using Vuforia;

public class CameraFocusController : MonoBehaviour
{

    private bool mVuforiaStarted = false;

    void Start()
    {
		
			VuforiaApplication.Instance.OnVuforiaStarted += StartVuforiaFocus;
    }

    private void StartAfterVuforia()
    {
        mVuforiaStarted = true;
        StartVuforiaFocus();
    }

    void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            // App resumed
            if (mVuforiaStarted)
            {
                // App resumed and vuforia already started
                // but lets start it again...
                StartVuforiaFocus(); // This is done because some android devices lose the auto focus after resume
                // this was a bug in vuforia 4 and 5. I haven't checked 6, but the code is harmless anyway
            }
        }
    }

    public void StartVuforiaFocus()
    {
        if (VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            Debug.Log("Autofocus set");
        }
        else
        {
            // never actually seen a device that doesn't support this, but just in case
            Debug.Log("this device doesn't support auto focus");
        }
    }
}
