﻿using System.IO;
using UnityEditor;

public class ExportAssetBundles {
	[MenuItem ("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles () {
		string assetBundleDirectory = "Assets/AssetBundles";
		if (!Directory.Exists (assetBundleDirectory)) {
			Directory.CreateDirectory (assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles (assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
	}
}