using UnityEngine;
using UnityEngine.AddressableAssets;


namespace GameModule.SettingsModule
{	
	[CreateAssetMenu(fileName = "ChapterConfig", menuName = "MyAssets/Game/Settings/ChapterConfig")]
	public class ChapterLoadConfig : ScriptableObject
	{
		[SerializeField]
		private AssetReferenceT<TextAsset> _testAssetBig;
		[SerializeField]  
		private AssetReferenceT<TextAsset> _testAssetActor;
		[SerializeField]
		private AssetLabelReference _assetLabelTest;
		
		public AssetReferenceT<TextAsset> TestAssetBig => _testAssetBig;
		public AssetReferenceT<TextAsset> TestAssetActor => _testAssetActor;
		public AssetLabelReference AssetLabelTest => _assetLabelTest;
	}
}
