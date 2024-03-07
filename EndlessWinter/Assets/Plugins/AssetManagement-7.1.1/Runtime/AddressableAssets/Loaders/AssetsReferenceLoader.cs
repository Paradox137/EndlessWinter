using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Dre0Dru.AddressableAssets.Loaders
{
    public sealed class
        AssetsReferenceLoader<TAsset> : IAssetsReferenceLoader<TAsset>
        where TAsset : Object
    {
        private readonly Dictionary<object, AsyncOperationHandle<TAsset>> _operationHandles;
        private Action _onComplete;
        private Action _onCancel;
        private CancellationToken _cancellationToken;
        
        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public AssetsReferenceLoader() =>
            _operationHandles = new Dictionary<object, AsyncOperationHandle<TAsset>>();

        public UniTask PreloadAssetAsync(AssetReferenceT<TAsset> key) =>
            LoadAssetAsync(key);
        
        public AssetsReferenceLoader<TAsset> OnComplete(Action __onComplete) 
        {
            _onComplete = __onComplete;      
            return this;      
        }
        
        public AssetsReferenceLoader<TAsset> WithCancellationToken(CancellationToken __cancellationToken, Action __onCancel)
        {
            _cancellationToken = __cancellationToken;
            _onCancel += __onCancel;
            return this;      
        }
        
        private void Clear()
        {
            _onComplete = null;
            _onCancel = null;
            _cancellationToken = CancellationToken.None;
        }
        
        public async UniTask<TAsset> LoadAssetAsync(AssetReferenceT<TAsset> key)
        {
            if (key == null)
            {
                Debug.Log("RETURN NUll");
                return null;
            } 
            
            var handle = GetLoadHandle(key);

            try
            {
                TAsset asset = await handle.WithCancellation(_cancellationToken);
                _onComplete?.Invoke();
                Clear();
                return asset;
            }
            catch
            {
                if(_cancellationToken!=CancellationToken.None)
                    _onCancel?.Invoke();
                Addressables.Release(handle);
                _operationHandles.Remove(key.RuntimeKey);
                Clear();
                throw;
            }
        }

        public bool IsAssetLoaded(AssetReferenceT<TAsset> key)
        {
            if (_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                return handle.IsDone;
            }

            return false;
        }

        public TAsset GetAsset(AssetReferenceT<TAsset> key) =>
            _operationHandles[key.RuntimeKey].Result;

        public bool TryGetAsset(AssetReferenceT<TAsset> key, out TAsset asset)
        {
            asset = default;

            if (_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                if (handle.IsDone)
                {
                    asset = handle.Result;
                }

                return handle.IsDone;
            }

            return false;
        }

        public void UnloadAsset(AssetReferenceT<TAsset> key)
        {
            if (_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                Addressables.Release(handle);
                _operationHandles.Remove(key.RuntimeKey);
            }
        }

        public void UnloadAllAssets()
        {
            foreach (var handle in _operationHandles.Values)
            {
                Addressables.Release(handle);
            }

            _operationHandles.Clear();
        }

        private AsyncOperationHandle<TAsset> GetLoadHandle(AssetReferenceT<TAsset> key)
        {
            if (!_operationHandles.TryGetValue(key.RuntimeKey, out AsyncOperationHandle<TAsset> handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                _operationHandles.Add(key.RuntimeKey, handle);
            }

            return handle;
        }
    }
}