using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WiimoteApi;

public class Menu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI statusText;

    [SerializeField]
    GameObject lightHousePivot,
        connectPanel,
        menuPanel;

    [SerializeField]
    string gameVer;

    Wiimote wiimote;

    [SerializeField]
    bool isMenu, isWii;

    bool setOffset, doneFirstConnection;

    Vector3 wmpOffset = Vector3.zero;

    [SerializeField]
    Vector3 deadzone;

    bool singleDeadDetect;

    public void connectController()
    {
        WiimoteManager.FindWiimotes();
    }

    private void Start()
    {
        if (isMenu)
        {
            PlayerPrefs.DeleteAll();
        }
    }
    private void Update()
    {
        //limitRot();
        
        if (isWii)
        {
            if (WiimoteManager.HasWiimote() && isMenu ||
                       WiimoteManager.HasWiimote() && !isMenu)
            {
                if (isMenu)
                    menuPanel.SetActive(true);

                connectPanel.SetActive(false);

                wiimote = WiimoteManager.Wiimotes[0];

                int ret;
                do
                {
                    ret = wiimote.ReadWiimoteData();

                    if (ret > 0 && wiimote.current_ext == ExtensionController.MOTIONPLUS)
                    {
                        Vector3 offset = new Vector3(0, 0, wiimote.MotionPlus.YawSpeed) / 95;

                        float deadzoneValue = wiimote.MotionPlus.YawSpeed / 95;

                        if (!singleDeadDetect && deadzoneValue < -0.04 && deadzoneValue > -0.08)
                        {
                            deadzone = new Vector3(0, 0, wiimote.MotionPlus.YawSpeed) / 95;
                            singleDeadDetect = true;
                        }

                        if (!setOffset)
                        {
                            wmpOffset += offset;
                            setOffset = true;
                        }

                        lightHousePivot.transform.Rotate(offset - deadzone, Space.Self);


                    }
                    else
                    {
                        wiimote.RequestIdentifyWiiMotionPlus();
                        wiimote.ActivateWiiMotionPlus();
                    }
                } while (ret > 0);

                if (wiimote.Button.a || wiimote.Button.b || wiimote.Button.one || wiimote.Button.two ||
                    wiimote.Button.d_up || wiimote.Button.d_down || wiimote.Button.d_left || wiimote.Button.d_right ||
                    wiimote.Button.plus || wiimote.Button.minus ||wiimote.Button.home)
                {
                    lightHousePivot.transform.rotation = Quaternion.EulerRotation(Vector3.zero);
                    singleDeadDetect = false;
                }
            }
            else if (!WiimoteManager.HasWiimote() && isMenu ||
               !WiimoteManager.HasWiimote() && !isMenu)
            {
                connectPanel.SetActive(true);
                statusText.text = "[GVer: " + gameVer + "] Controller Status: Not connected.";
            }
        }
        else
        {
            if (isMenu)
                menuPanel.SetActive(true);
        }
    }

    void limitRot()
    {
        Vector3 playerEulerAngles = lightHousePivot.transform.rotation.eulerAngles;

        playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, -200, 200);


        lightHousePivot.transform.rotation = Quaternion.Euler(playerEulerAngles);
    }
}
