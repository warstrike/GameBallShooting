using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiResScreenShots : MonoBehaviour
{

    private bool takeHiResShot = false;

    public string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    void LateUpdate()
    {
        takeHiResShot |= Input.GetKeyDown("k");
        if (takeHiResShot)
        {
            ScreenCapture.CaptureScreenshot(ScreenShotName(Screen.width, Screen.height));
            takeHiResShot = false;
        }
    }
}