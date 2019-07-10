/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Licensed under the Oculus Utilities SDK License Version 1.31 (the "License"); you may not use
the Utilities SDK except in compliance with the License, which is provided at the time of installation
or download, or which otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at
https://developer.oculus.com/licenses/utilities-1.31

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using UnityEngine;
using System.Collections;

/// <summary>
/// Simple helper script that conditionally enables rendering of a controller if it is connected.
/// </summary>
public class OVRControllerHelper : MonoBehaviour
{
	/// <summary>
	/// The root GameObject that represents the GearVr Controller model.
	/// </summary>
	public GameObject m_modelGearVrController;

	/// <summary>
	/// The root GameObject that represents the Oculus Go Controller model.
	/// </summary>
	public GameObject m_modelOculusGoController;

	/// <summary>
	/// The root GameObject that represents the Oculus Touch for Quest And RiftS Controller model (Left).
	/// </summary>
	public GameObject m_modelOculusTouchQuestAndRiftSLeftController;

	/// <summary>
	/// The root GameObject that represents the Oculus Touch for Quest And RiftS Controller model (Right).
	/// </summary>
	public GameObject m_modelOculusTouchQuestAndRiftSRightController;

	/// <summary>
	/// The root GameObject that represents the Oculus Touch for Rift Controller model (Left).
	/// </summary>
	public GameObject m_modelOculusTouchRiftLeftController;

	/// <summary>
	/// The root GameObject that represents the Oculus Touch for Rift Controller model (Right).
	/// </summary>
	public GameObject m_modelOculusTouchRiftRightController;

    public GameObject m_modelBat;//追加

    /// <summary>
    /// The controller that determines whether or not to enable rendering of the controller model.
    /// </summary>
    public OVRInput.Controller m_controller;//Type of HMD

	private enum ControllerType
	{
		GearVR, Go, QuestAndRiftS, Rift
	}

    public bool isBat = false;
    public bool isController = false;//true;//

    private ControllerType activeControllerType = ControllerType.Rift;

	private bool m_prevControllerConnected = false;
	private bool m_prevControllerConnectedCached = false;

	void Start()
	{
        

		OVRPlugin.SystemHeadset headset = OVRPlugin.GetSystemHeadsetType();
		switch (headset)
		{
			case OVRPlugin.SystemHeadset.Oculus_Go:
				activeControllerType = ControllerType.Go;
				break;
			case OVRPlugin.SystemHeadset.Oculus_Quest:
				activeControllerType = ControllerType.QuestAndRiftS;
				break;
			case OVRPlugin.SystemHeadset.Rift_CV1:
				activeControllerType = ControllerType.Rift;
				break;
			case OVRPlugin.SystemHeadset.Rift_S:
				activeControllerType = ControllerType.QuestAndRiftS;
				break;
			case OVRPlugin.SystemHeadset.GearVR_R320:
			case OVRPlugin.SystemHeadset.GearVR_R321:
			case OVRPlugin.SystemHeadset.GearVR_R322:
			case OVRPlugin.SystemHeadset.GearVR_R323:
			case OVRPlugin.SystemHeadset.GearVR_R324:
			case OVRPlugin.SystemHeadset.GearVR_R325:
				activeControllerType = ControllerType.GearVR;
				break;
			default:
#if UNITY_EDITOR || !UNITY_ANDROID
				activeControllerType = ControllerType.Rift;
#else
				activeControllerType = ControllerType.GearVR;
#endif
				break;
		}

		Debug.LogFormat("OVRControllerHelp: Active controller type: {0} for product {1}", activeControllerType, OVRPlugin.productName);
		if ((activeControllerType != ControllerType.GearVR) && (activeControllerType != ControllerType.Go))
		{
			if (m_controller == OVRInput.Controller.LTrackedRemote)
			{
				//m_controller = OVRInput.Controller.Bat;//追加
                m_controller = OVRInput.Controller.LTouch;
            }
			else if (m_controller == OVRInput.Controller.RTrackedRemote)
			{
				//m_controller = OVRInput.Controller.Bat;
                m_controller = OVRInput.Controller.RTouch;
            }
        }
		else
		{
			if (m_controller == OVRInput.Controller.LTouch)
			{
				//m_controller = OVRInput.Controller.Bat;             
                m_controller = OVRInput.Controller.LTrackedRemote;//追加
            }
			else if (m_controller == OVRInput.Controller.RTouch)
			{
                //m_controller = OVRInput.Controller.Bat;
                m_controller = OVRInput.Controller.RTrackedRemote;
            }
        }
	}

    void Update()
    {
        bool controllerConnected = OVRInput.IsControllerConnected(m_controller);

        DetectRightTouch();

        if (Input.GetKey(KeyCode.C))//追加
            isBat = !isBat;//boolを反転させる

        if (isBat == false)
        { //バットとコントローラーの切り替えを行う
            m_modelBat.SetActive(false);
            m_modelOculusTouchQuestAndRiftSRightController
                .SetActive(isController);//boolでまとめる
        }
        else
        {
            m_modelBat.SetActive(isController);
            m_modelOculusTouchQuestAndRiftSRightController.SetActive(false);
        }
        
		if ((controllerConnected != m_prevControllerConnected) || !m_prevControllerConnectedCached)
		{
			if (activeControllerType == ControllerType.GearVR || activeControllerType == ControllerType.Go)
			{
				m_modelOculusGoController.SetActive(controllerConnected && (activeControllerType == ControllerType.Go));
				m_modelGearVrController.SetActive(controllerConnected && (activeControllerType != ControllerType.Go));
				m_modelOculusTouchQuestAndRiftSLeftController.SetActive(false);
				m_modelOculusTouchQuestAndRiftSRightController.SetActive(false);
				m_modelOculusTouchRiftLeftController.SetActive(false);
				m_modelOculusTouchRiftRightController.SetActive(false);
                m_modelBat.SetActive(false);//追加
            }
            else if (activeControllerType == ControllerType.QuestAndRiftS)
            {
                m_modelOculusGoController.SetActive(false);
                m_modelGearVrController.SetActive(false);
                m_modelOculusTouchQuestAndRiftSLeftController.SetActive(false);
                m_modelOculusTouchQuestAndRiftSRightController.SetActive(isController);//controllerConnected && (m_controller == OVRInput.Controller.RTouch));
                m_modelOculusTouchRiftLeftController.SetActive(false);
                m_modelOculusTouchRiftRightController.SetActive(false);
                m_modelBat.SetActive(false);
            }
            else // if (activeControllerType == ControllerType.Rift)
            {
				m_modelOculusGoController.SetActive(false);
				m_modelGearVrController.SetActive(false);
				m_modelOculusTouchQuestAndRiftSLeftController.SetActive(false);
				m_modelOculusTouchQuestAndRiftSRightController.SetActive(false);
				m_modelOculusTouchRiftLeftController.SetActive(controllerConnected && (m_controller == OVRInput.Controller.LTouch));
                m_modelOculusTouchRiftRightController.SetActive(isController);//controllerConnected && (m_controller == OVRInput.Controller.RTouch));
                m_modelBat.SetActive(false);
            }

			m_prevControllerConnected = controllerConnected;
			m_prevControllerConnectedCached = true;
		}

		if (!controllerConnected)
		{
			return;
		}

        void DetectRightTouch()//右コントローラー表示bool管理関数
        {
            if (m_controller == OVRInput.Controller.RTouch)
                isController = true;
            else
                isController = false;
        }
	}
}
