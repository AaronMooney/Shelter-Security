//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Valve.VR;

//public class ViveInput : MonoBehaviour
//{

//    [SteamVR_DefaultAction("Squeeze")]
//    public SteamVR_Action_Single squeezeAction;
//    public SteamVR_Action_Vector2 touchPadAction;
//    private GameObject overheadCamera;

//    void Update()
//    {
//        overheadCamera = GameObject.Find("OverheadCamera");
//        Vector2 touchPadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.LeftHand);
//        Vector3 cameraPos = overheadCamera.transform.position;
//        if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.LeftHand))
//        {
//            if (touchPadValue.x < 0 && touchPadValue.y < 0.7 && touchPadValue.y > -0.7)
//            {
//                cameraPos = overheadCamera.transform.position;
//                float pan = overheadCamera.transform.position.z + 2f;
//                overheadCamera.transform.position = new Vector3(cameraPos.x, cameraPos.y, pan);
//                print("move right");
//            }

//            if (touchPadValue.x > 0 && touchPadValue.y < 0.7 && touchPadValue.y > -0.7)
//            {
//                cameraPos = overheadCamera.transform.position;
//                float pan = overheadCamera.transform.position.z - 2f;
//                overheadCamera.transform.position = new Vector3(cameraPos.x, cameraPos.y, pan);
//                print("move left");
//            }

//            if (touchPadValue.y < 0 && touchPadValue.x < 0.7 && touchPadValue.x > -0.7)
//            {
//                cameraPos = overheadCamera.transform.position;
//                float pan = overheadCamera.transform.position.x - 2f;
//                overheadCamera.transform.position = new Vector3(pan, cameraPos.y, cameraPos.z);
//                print("move down");
//            }

//            if (touchPadValue.y > 0 && touchPadValue.x < 0.7 && touchPadValue.x > -0.7)
//            {
//                cameraPos = overheadCamera.transform.position;
//                float pan = overheadCamera.transform.position.x + 2f;
//                overheadCamera.transform.position = new Vector3(pan, cameraPos.y, cameraPos.z);
//                print("move up");
//            }
//            print("Teleport down");
//        }

//        if (SteamVR_Input._default.inActions.GrabPinch.GetStateUp(SteamVR_Input_Sources.RightHand))
//        {
//            print("GrabPinch up");
//        }

//        float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand);

//        if (triggerValue > 0.0f)
//        {
//            print(triggerValue);
//        }


//        if (touchPadValue != Vector2.zero)
//        {
//            print(touchPadAction);
//        }

//    }
//}
