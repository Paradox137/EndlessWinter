using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using GameModule.SettingsModule;
using UnityEngine;
using Dre0Dru.AddressableAssets;
using Dre0Dru.AddressableAssets.Downloaders;
using Dre0Dru.AddressableAssets.Loaders;
using GameModule.DataModule;
using GameModule.StorageModule;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.UI;

namespace GameModule.PlayerModule
{
	public class NovelLoadService
	{
		private readonly NovelStorage _novelStorage;
		private readonly IAssetsReferenceLoader<TextAsset> _loader;
		private readonly ChapterLoadConfig _chapterLoadSettings;

		private TextAsset _textAsset;
		public NovelLoadService(NovelStorage ___novelStorage, ChapterLoadConfig __chapterLoadSettings)
		{
			_novelStorage = ___novelStorage;
			_chapterLoadSettings = __chapterLoadSettings;
			
			_loader = new AssetsReferenceLoader<TextAsset>();  
		}


		public Task PlaceInStorage()
		{
			//TextAsset text = _loader.GetAsset(_chapterLoadSettings.TestAssetActor);
			_novelStorage.SetNewActor(_textAsset);

			return Task.CompletedTask;
		}

		public async UniTask LoadPack()
		{
			/*UniTask<TextAsset> r = _loader.TryGetAsset(_chapterLoadSettings.TestAssetActor);
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
			*/
			AsyncOperationHandle<IList<TextAsset>> l = Addressables.LoadAssetsAsync<TextAsset>("chapter1", Callback);
			
			Addressables.Release(l);
			int x = 0;
			while (l.Status != AsyncOperationStatus.Succeeded)
			{
				await UniTask.Yield();
				Debug.Log(x);
				x++;
			}
			
			await l.Task;
			//Addressables.LoadResourceLocationsAsync("chapter1", typeof(TextAsset)).Completed += OnCompleted;
			/*AssetLabelsDownloadPack downloadPack = new AssetLabelsDownloadPack(_chapterLoadSettings.AssetLabelTest);
			await downloadPack.StartDownloadAsync();
			
			downloadPack.TrackProgress(Progress.Create<AssetsDownloadStatus>(DisplayStatus));*/


		}
		private void Callback(TextAsset __obj)
		{
			if (__obj.name == "large-file")
					return;
			Debug.Log(__obj.name);
			Debug.Log(__obj.text);
			_textAsset = __obj;
		}
	/*	private async void OnCompleted(AsyncOperationHandle<IList<IResourceLocation>> __obj)
		{
			foreach (IResourceLocation resource in __obj.Result)
			{
				var guid = AssetDatabase.AssetPathToGUID(resource.PrimaryKey);
				
				AssetReferenceT<TextAsset> s = new AssetReferenceT<TextAsset>(guid);
				
				await _loader.LoadAssetAsync(s);
				
				Debug.Log(_loader.IsAssetLoaded(_chapterLoadSettings.TestAssetActor));

			}
		}*/


		private void DisplayStatus(AssetsDownloadStatus status)  
		{
			Debug.Log($"Percent: {status.PercentProgress:F}");
		}
	}
}
