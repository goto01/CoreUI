using CoreUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.CoreUI
{
	[InitializeOnLoad]
	public static class CoreUIStyleCreator
	{
		private static StylesRepository _stylesRepository;
		
		private static bool Loaded{get { return _stylesRepository != null; }}
		
		static CoreUIStyleCreator()
		{
			Load();
		}

		public static void Load()
		{
			var assets = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(StylesRepository).Name));
			if (assets.Length > 1) Debug.LogError("You have more than one instance of StylesRepository");
			else if (assets.Length == 0)
			{
				Debug.Log("You doesn't have any instance of StylesRepository");
				return;
			}
			_stylesRepository = AssetDatabase.LoadAssetAtPath<StylesRepository>(AssetDatabase.GUIDToAssetPath(assets[0]));
		}

		public static T CreateStyle<T>(string name) where T : BaseStyle
		{
			if (!Loaded) Load();
			var style = ObjectCreatorHelper.CreateAsset<T>(name);
			_stylesRepository.AddStyle(style);
			EditorUtility.SetDirty(_stylesRepository);
			return style;
		}
		
		public static T CreateStyle<T>() where T : BaseStyle
		{
			if (!Loaded) Load();
			var style = ObjectCreatorHelper.CreateAsset<T>();
			_stylesRepository.AddStyle(style);
			EditorUtility.SetDirty(_stylesRepository);
			return style;
		}
	}
}