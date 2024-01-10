using Cysharp.Threading.Tasks;
using GameModule.SettingsModule;
using UnityEngine;
using Dre0Dru.AddressableAssets;
using Dre0Dru.AddressableAssets.Downloaders;
using Dre0Dru.AddressableAssets.Loaders;

namespace GameModule.PlayerModule
{
	public class NovelLoadService
	{
		public NovelLoadService()
		{
			
		}

		public async UniTask LoadPack(IAssetsReferenceLoader<TextAsset> __loader, ChapterLoadSettings __chapterSettings)
		{
			UniTask<TextAsset[]> r = __loader.LoadAssetsAsync(__chapterSettings.TestAssetBig);
			UniTask<TextAsset[]> z = __loader.LoadAssetsAsync(__chapterSettings.TestAssetActor);
			
			int x = 0;
			while (r.Status != UniTaskStatus.Succeeded && z.Status != UniTaskStatus.Succeeded)
			{
				await UniTask.Yield();
				Debug.Log(x);
				x++;
			}
			
			//await __loader.LoadAssetAsync(__chapterSettings.TestAssetBig);
			
			Debug.Log("downloaded");
			var progress = Progress.Create<float>(x => Debug.Log(x));
			
			
			//AssetLabelsDownloadPack downloadPack = new AssetLabelsDownloadPack(__chapterSettings.AssetLabelTest);
			
			//downloadPack.TrackProgress(Progress.Create<AssetsDownloadStatus>(DisplayStatus));
			
			//await downloadPack.StartDownloadAsync();
			
			Debug.Log(__loader.IsAssetLoaded(__chapterSettings.TestAssetActor));
		}
		
		private void DisplayStatus(AssetsDownloadStatus status)  
		{
			Debug.Log($"Percent: {status.PercentProgress:F}");
		}
	}
}
