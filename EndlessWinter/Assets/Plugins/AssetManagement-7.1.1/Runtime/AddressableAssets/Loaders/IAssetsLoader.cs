using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Dre0Dru.AddressableAssets.Loaders
{
    public interface IAssetsLoader<in TKey, TAsset> : IAssetsUnloader
        where TAsset : Object
    {
        UniTask PreloadAssetAsync(TKey key);

        UniTask<TAsset> LoadAssetAsync(TKey key);
        
        bool IsAssetLoaded(TKey key);

        TAsset GetAsset(TKey key);

        bool TryGetAsset(TKey key, out TAsset asset);

        void UnloadAsset(TKey key);

    }
}