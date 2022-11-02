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
    GameObject lightHouseObj,
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

        if (WiimoteManager.HasWiimote())
        {
            statusText.text = "[GVer: " + gameVer + "] Controller Status: Connected.";
            doneFirstConnection = true;
        }
        else
        {
            statusText.text = "[GVer: " + gameVer + "] Controller Status: Failed connection.";
        }
    }

    private void Update()
    {
        if (isWii)
        {
            if (WiimoteManager.HasWiimote() && doneFirstConnection && isMenu ||
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

                        if (!singleDeadDetect && deadzoneValue < -0.04 && deadzoneValue > -0.07)
                        {
                                deadzone = new Vector3(0, 0, wiimote.MotionPlus.YawSpeed) / 95;
                                singleDeadDetect = true;
                        }

                        if (!setOffset)
                        {
                            wmpOffset += offset;
                            setOffset = true;
                        }

                        lightHouseObj.transform.Rotate(offset - deadzone, Space.Self);

                        limitRot();
                    }
                    else
                    {
                        wiimote.RequestIdentifyWiiMotionPlus();
                        wiimote.ActivateWiiMotionPlus();
                    }

                    //Reset Gyro
                    if (wiimote.Button.b)
                    {
                        singleDeadDetect = false;
                        lightHouseObj.transform.rotation = Quaternion.Euler(wmpOffset);
                    }
                } while (ret > 0);

            }
            else if (!WiimoteManager.HasWiimote() && doneFirstConnection && isMenu ||
               !WiimoteManager.HasWiimote() && !isMenu)
            {
                connectPanel.SetActive(true);
                statusText.text = "[GVer: " + gameVer + "] Controller Status: Not connected.";
            }
        }
    }

    void limitRot()
    {
        Vector3 playerEulerAngles = lightHouseObj.transform.rotation.eulerAngles;

        playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, -100, 100);


        lightHouseObj.transform.rotation = Quaternion.Euler(playerEulerAngles);
    }
}
