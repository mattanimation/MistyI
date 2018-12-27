using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misty;

public class MistyTests : MonoBehaviour {

	public AudioClip sample;

	public Texture testImage;

	// ===== GENERAL ==== //
	public void TestGetRobotInfo(){
		MysticalMisty.Instance.api.GetDeviceInformation ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}

	public void TestGetHelp(){
		MysticalMisty.Instance.api.GetHelp ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}

	public void TestGetHelpAboutCommand(){
		MysticalMisty.Instance.api.GetHelpAboutCommand ("LED", (APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}

	public void TestGetBattery(){
		MysticalMisty.Instance.api.GetBatteryLevel ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}

	public void TestConnectWifi(){
		MysticalMisty.Instance.api.ConnectWiFi ("herp", "derp123*&^", (APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}

	public void TestPerformUpdate(){
		MysticalMisty.Instance.api.PerformSystemUpdate ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}

	public void TestGetStoreUpdate(){
		MysticalMisty.Instance.api.GetStoreUpdateAvailable ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}

	public void TestGetSensorValues(){
		MysticalMisty.Instance.api.GetStringSensorValues ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}

	// ===== MISC ===== //
	public void TestLEDChange(){
		MysticalMisty.Instance.api.ChangeLED (MysticalMisty.Instance.core.ui.picker.CurrentColor, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestLEDChangeByValue(){
		Color32 col = MysticalMisty.Instance.core.ui.picker.CurrentColor;
		MysticalMisty.Instance.api.ChangeLEDByValue (col.r, col.g, col.b, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}


	// ===== AUDIO ===== //
	public void TestGetAudioClips(){
		MysticalMisty.Instance.api.GetListOfAudioClips ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestPlayAudioClip(){
		
		MysticalMisty.Instance.api.PlayAudioClip ("EeeeeeE", (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestDeleteAudioClip(){
		MysticalMisty.Instance.api.DeleteAudioClip ("ewok1.wav", (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestSaveAudioClip(){
		string fName = "ewok1.wav";
		string tmpClipName = "";
		byte[] dat = WavUtility.FromAudioClip (sample, out tmpClipName, true);
		MysticalMisty.Instance.api.SaveAudioAssetToRobot (fName, dat, true,true, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}
		
	public void TestGetAudioFiles(){
		MysticalMisty.Instance.api.GetListOfAudioFiles ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	// ===== IMAGES ===== //
	public void TestChangeDisplayImage(){
		MysticalMisty.Instance.api.ChangeDisplayImage ("Angry.jpg", (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestSaveImage(){
		
		MysticalMisty.Instance.api.SaveImageAssetToRobot ("derp.png", testImage, 480, 360, true, true, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestDeleteImage(){
		MysticalMisty.Instance.api.DeleteImage ("derp.png", (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestGetImages(){
		MysticalMisty.Instance.api.GetListOfImages ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestRevertDisplay(){
		MysticalMisty.Instance.api.RevertDisplay ( (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}


	// ===== MOVEMENT ===== //
	public void TestDrive(){
		//forward = 0 angle, 30 forward
		MysticalMisty.Instance.api.Drive (30, 0, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestDriveTime(){
		MistyDriveTime dt = new MistyDriveTime (30, 0, 5);
		// 1000 =  1 second
		MysticalMisty.Instance.api.DriveTime (dt, 1000, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestDriveTimeValue(){
		MysticalMisty.Instance.api.DriveTimeByValue (30,0, 0, 1000, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestDriveLocation(){
		MysticalMisty.Instance.api.DriveToLocation (0,0, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestLocomotionTrack(){
		MysticalMisty.Instance.api.LocomotionTrack (1,1, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}


	public void TestMoveArm(){
		MysticalMisty.Instance.api.MoveArm ("rightside", 5, 5, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestMoveHead(){
		MysticalMisty.Instance.api.MoveHead (5,0,0,5, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestMoveHeadLocation(){
		MysticalMisty.Instance.api.MoveHeadToLocation (5,5, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestSetHead(){
		MysticalMisty.Instance.api.SetHeadPosition ("x", 5,5, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStopRobot(){
		MysticalMisty.Instance.api.StopRobot ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);	
		});
	}

	// ===== FACE =====
	public void TestStartFaceDetect(){
		MysticalMisty.Instance.api.StartFaceDetection ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStopFaceDetect(){
		MysticalMisty.Instance.api.StopFaceDetection ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStartFaceTrain(){
		MysticalMisty.Instance.api.StartFaceTraining ("derp", (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStopFaceTrain(){
		MysticalMisty.Instance.api.StopFaceTraining ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStartFaceRecog(){
		MysticalMisty.Instance.api.StartFacialRecognition ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStopFaceRecog(){
		MysticalMisty.Instance.api.StopFacialRecognition ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestClearFaces(){
		MysticalMisty.Instance.api.ClearLearnedFaces ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	// ===== SLAM =====
	public void TestStartMapping(){
		MysticalMisty.Instance.api.StartMapping ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStopMapping(){
		MysticalMisty.Instance.api.StopMapping ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStartTracking(){
		MysticalMisty.Instance.api.StartTracking ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStopTracking(){
		MysticalMisty.Instance.api.StopTracking ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStartRecording(){
		MysticalMisty.Instance.api.StartRecording ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestStopRecording(){
		MysticalMisty.Instance.api.StopRecording ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestGetMap(){
		MysticalMisty.Instance.api.GetMap ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestGetRawMap(){
		MysticalMisty.Instance.api.GetRawMap ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestGetPath(){
		MysticalMisty.Instance.api.GetPath ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestFollowPath(){
		MysticalMisty.Instance.api.FollowPath ("1,23,4", (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestFollowPathOthers(){
		MysticalMisty.Instance.api.FollowPathOther (5,5, (APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestGetStatus(){
		MysticalMisty.Instance.api.GetStatus ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

	public void TestResetMapping(){
		MysticalMisty.Instance.api.ResetMapping ((APIResponse obj) => {
			Debug.Log(obj.rawJSON);
		});
	}

}
