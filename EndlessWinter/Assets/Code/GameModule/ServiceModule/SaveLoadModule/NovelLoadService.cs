using System.Threading;
using Cysharp.Threading.Tasks;
using Dre0Dru.AddressableAssets.Loaders;
using GameModule.ConfigsModule;
using GameModule.StorageModule;
using SharedModule.CustomizeModule;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameModule.ServiceModule.SaveLoadModule
{
	public class NovelLoadService
	{
		private readonly NovelStorage _novelStorage;
		private readonly ChapterLoadConfig _chapterLoadConfig;
		private readonly DataConverterService _converterService;
		
		private readonly AssetsReferenceLoader<TextAsset> _textLoader;
		private readonly AssetsReferenceLoader<DialogueFlowConfig> _configLoader;
		private readonly AssetsReferenceLoader<Sprite> _spriteLoader;

		private DialogueFlowConfig[] _dialogueConfigs;
		public NovelLoadService(NovelStorage __novelStorage, ChapterLoadConfig __chapterLoadConfig)
		{
			_novelStorage = __novelStorage;
			_chapterLoadConfig = __chapterLoadConfig;
			
			_textLoader = new AssetsReferenceLoader<TextAsset>();
			_configLoader = new AssetsReferenceLoader<DialogueFlowConfig>();
			_spriteLoader = new AssetsReferenceLoader<Sprite>();

			_converterService = new DataConverterService(ref _spriteLoader, ref _textLoader);
		}
		
		public async void PreloadChapterData((ushort, ushort, ushort) __savePlace, CancellationToken __cancellationToken = new CancellationToken())
		{
			await Addressables.InitializeAsync();
			
			_dialogueConfigs = null;
			
			ChapterFlowConfig chapterConfig = GetSavedChapterFlowConfig(__savePlace);

			for (int i = 0; i < chapterConfig.DialogueFlowConfigs.Count; i++)
			{
				AssetReferenceT<DialogueFlowConfig> dialogueRef = chapterConfig.DialogueFlowConfigs[i];
				DialogueFlowConfig dialogueConfig = await GetDialogueConfig(dialogueRef, __cancellationToken);

				_dialogueConfigs![i] = dialogueConfig;

				_textLoader.PreloadAssetsAsync(dialogueConfig.ActorsTexts);
				
				PreLoadMainSprites(dialogueConfig);
				PreLoadFlowSprites(dialogueConfig.StartFlow);
				PreLoadFlowSprites(dialogueConfig.PositiveFlow);
				PreLoadFlowSprites(dialogueConfig.NegativeFlow);
				PreLoadFlowSprites(dialogueConfig.EndFlow);
				
			}
			
			OutputDownloaded("All Data Preloaded");
		}
		
		public async UniTask LoadChapterData((ushort, ushort, ushort) __savePlace, CancellationToken __cancellationToken = new CancellationToken())
		{
			await Addressables.InitializeAsync();
			
			ChapterFlowConfig chapterConfig = GetSavedChapterFlowConfig(__savePlace);
			
			for (int i = 0; i < chapterConfig.DialogueFlowConfigs.Count; i++)
			{ 
				if(_dialogueConfigs[i] == null)
					_dialogueConfigs[i] = await GetDialogueConfig(chapterConfig.DialogueFlowConfigs[i], __cancellationToken);
				
				await _textLoader.LoadAssetsAsync(_dialogueConfigs[i].ActorsTexts);
				await LoadMainSprites(__cancellationToken, _dialogueConfigs[i]);
				
				await LoadFlowSprites(__cancellationToken, _dialogueConfigs[i].StartFlow);
				await LoadFlowSprites(__cancellationToken, _dialogueConfigs[i].PositiveFlow);
				await LoadFlowSprites(__cancellationToken, _dialogueConfigs[i].NegativeFlow);
				await LoadFlowSprites(__cancellationToken, _dialogueConfigs[i].EndFlow);
			}
			
			OutputDownloaded("All Data Loaded");
			
			await UniTask.CompletedTask;
		}
		
		public async UniTask PlaceDataInStorage((ushort, ushort, ushort) __savePlace)
		{
			_novelStorage.SetData(_converterService.GenerateDialogueFlowClass(_dialogueConfigs, __savePlace.Item3, __savePlace.Item2), __savePlace);
			
			await UniTask.CompletedTask;
		}
		
		private ChapterFlowConfig GetSavedChapterFlowConfig((ushort, ushort, ushort) __savePlace)
		{
			ChapterFlowConfig chapterConfig = _chapterLoadConfig._ChapterFlowConfigs[__savePlace.Item1];
			
			_dialogueConfigs ??= new DialogueFlowConfig[_chapterLoadConfig._ChapterFlowConfigs[__savePlace.Item1].DialogueFlowConfigs.Count];
			
			return chapterConfig;
		}
		private async UniTask<DialogueFlowConfig> GetDialogueConfig(AssetReferenceT<DialogueFlowConfig> __dialogueKey, CancellationToken __cancellationToken)
		{
			DialogueFlowConfig dialogueConfig = await _configLoader
				.WithCancellationToken(__cancellationToken, () => OutputCanceled("Config data Canceled") )
				.OnComplete(() => OutputDownloaded("Config data Loaded"))
				.LoadAssetAsync(__dialogueKey);
			
			return dialogueConfig;
		}
		private async UniTask LoadMainSprites(CancellationToken __cancellationToken, DialogueFlowConfig __dialogueConfig)
		{
			foreach (ImageFlowItem item in __dialogueConfig.ImageFlow.Items)
			{
				if(_spriteLoader.IsAssetLoaded(item.ImageSprite))
					continue;
				
				await _spriteLoader
					.WithCancellationToken(__cancellationToken, () => OutputCanceled("MainSprites data Canceled"))
					.OnComplete(() => OutputDownloaded("MainSprites data Loaded"))
					.LoadAssetAsync(item.ImageSprite);
			}
			
			await UniTask.CompletedTask;
		}
		private async UniTask LoadFlowSprites(CancellationToken __cancellationToken, DialogueCustomDictionary __dialoguePartFlow)
		{
			foreach (DialogueItem item in __dialoguePartFlow.Items)
			{
				if(string.IsNullOrEmpty(item.ActorInfo.AssetGUID))
					continue;

				await _spriteLoader
					.WithCancellationToken(__cancellationToken, () => OutputCanceled("Flow data Canceled"))
					.OnComplete(() => OutputDownloaded("Flow data Loaded"))
					.LoadAssetAsync(item.ActorInfo);
			}
			
			await UniTask.CompletedTask;
		}
		private void PreLoadFlowSprites(DialogueCustomDictionary __dialoguePartFlow)
		{
			foreach (DialogueItem item in __dialoguePartFlow.Items)
			{
				if(string.IsNullOrEmpty(item.ActorInfo.AssetGUID))
					continue;
				
				_spriteLoader.PreloadAssetAsync(item.ActorInfo);
			}
		}
		private void PreLoadMainSprites(DialogueFlowConfig __dialogueConfig)
		{
			foreach (ImageFlowItem item in __dialogueConfig.ImageFlow.Items)
			{
				_spriteLoader.PreloadAssetAsync(item.ImageSprite);
			}
		}
		private void OutputCanceled(object __message)
		{
			CustomDebug.WriteLineWarning("NovelLoadService", __message, CustomDebugColors.Cyan);
		}
		private void OutputDownloaded(object __message)
		{
			CustomDebug.WriteLine("NovelLoadService", __message, CustomDebugColors.Cyan);
		}
		
		~NovelLoadService()
		{
			_configLoader.UnloadAllAssets();	
			_spriteLoader.UnloadAllAssets();
			_textLoader.UnloadAllAssets();
		}
	}
}
