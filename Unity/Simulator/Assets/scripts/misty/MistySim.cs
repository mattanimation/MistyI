using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Misty {
	
	public class MistySim {

		public LEDData ledColor;

		public TOFData frontLeftTOFSensor;
		public TOFData frontMidTOFSensor;
		public TOFData frontRightTOFSensor;
		public TOFData rearMidTOFSensor;

		public List<ImageAssetInfo> images;
		public List<AudioClipInfo> audioFiles;

		public MistyHeadPositionData headPosition;
		public MistyImageData currentImage;

		public MistySim(){
			this.ledColor = new LEDData ();

			this.frontLeftTOFSensor = new TOFData ();
			this.frontMidTOFSensor = new TOFData ();
			this.frontRightTOFSensor = new TOFData ();

			this.rearMidTOFSensor = new TOFData ();

			this.headPosition = new MistyHeadPositionData();
			this.currentImage = new MistyImageData();

			this.images = new List<ImageAssetInfo> ();
			this.audioFiles = new List<AudioClipInfo> ();
			//add some default images for testing
			ImageAssetInfo angImg = new ImageAssetInfo (270,480, null, "Angry.jpg");
			ImageAssetInfo hapImg = new ImageAssetInfo (270,480, null, "Happy.jpg");
			ImageAssetInfo conImg = new ImageAssetInfo (270,480, null, "Content.jpg");
			this.images.Add (angImg);
			this.images.Add (hapImg);
			this.images.Add (conImg);

			//add some audio for testing
			AudioClipInfo aciA = new AudioClipInfo(0.0f, "somehwere", "some_clip.wav", true);
			this.audioFiles.Add (aciA);
		}

	}
}
