using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameModule.SettingsModule;
using UnityEngine;
using Dre0Dru.AddressableAssets;
using Dre0Dru.AddressableAssets.Downloaders;
using Dre0Dru.AddressableAssets.Loaders;
using GameModule.DataModule;
using GameModule.StorageModule;
using Newtonsoft.Json;

namespace GameModule.PlayerModule
{
	public class NovelLoadService
	{
		private readonly NovelStorage _novelStorage;
		private readonly IAssetsReferenceLoader<TextAsset> _loader;
		private readonly ChapterLoadSettings _chapterLoadSettings;
		public NovelLoadService(NovelStorage ___novelStorage, ChapterLoadSettings __chapterLoadSettings)
		{
			_novelStorage = ___novelStorage;
			_chapterLoadSettings = __chapterLoadSettings;
			
			_loader = new AssetsReferenceLoader<TextAsset>();  
		}


		public Task PlaceInStorage()
		{
			TextAsset text = _loader.GetAsset(_chapterLoadSettings.TestAssetActor);
			
			_novelStorage.SetNewActor(text);

			return Task.CompletedTask;
		}

		public async UniTask LoadPack()
		{
			UniTask<TextAsset[]> r = _loader.LoadAssetsAsync(_chapterLoadSettings.TestAssetBig);
			UniTask<TextAsset[]> z = _loader.LoadAssetsAsync(_chapterLoadSettings.TestAssetActor);
			
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
			
			
			Debug.Log(_loader.IsAssetLoaded(_chapterLoadSettings.TestAssetActor));
		}
		
		
		private void DisplayStatus(AssetsDownloadStatus status)  
		{
			Debug.Log($"Percent: {status.PercentProgress:F}");
		}
	}
}
