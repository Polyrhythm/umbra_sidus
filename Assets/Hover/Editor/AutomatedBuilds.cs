using System;
using UnityEditor;

namespace Hover.Editor {

	/*================================================================================================*/
	public class AutomatedBuilds {

		public const string DemoPath = "Assets/HoverDemos/";
		public const string CastCubesPath = DemoPath+"CastCubes/Scenes/";


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public static void BuildCastCubes() {
			Build(BuildTarget.StandaloneWindows, CastCubesPath, "Hover-CastCubes-LeapVR");
			Build(BuildTarget.StandaloneWindows, CastCubesPath, "HovercastDemo-LeapLookVR");
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private static void Build(BuildTarget pPlatform, string pPath, string pScene) {
			BuildPipeline.BuildPlayer(
				new[] { pPath+pScene+".unity" },
				GetPath(pPlatform, pScene),
				pPlatform,
				BuildOptions.None
			);
		}
		
		/*--------------------------------------------------------------------------------------------*/
		private static string GetPath(BuildTarget pPlatform, string pScene) {
			string platformLabel;
			string outputFilename = pScene;

			switch ( pPlatform ) {
				case BuildTarget.StandaloneWindows:
					platformLabel = "PC";
					outputFilename += ".exe";
					break;

				case BuildTarget.StandaloneOSXIntel:
					platformLabel = "Mac";
					break;

				default:
					throw new Exception("Unsupported build target: "+pPlatform);
			}

			string demoGroup = pScene.Substring(0, pScene.IndexOf('-'));
			//string date = DateTime.UtcNow.ToString("yyyy-MM-dd");
			return "../Builds/Auto/"+demoGroup+"-"+/*date+"-"+*/platformLabel+"/"+outputFilename;
		}

	}

}
