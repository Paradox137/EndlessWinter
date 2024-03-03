using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Dre0Dru.AddressableAssets.Loaders;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class dwdw : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceT<Sprite> x;
    [SerializeField]
    private Image _imageRef;
    
    private  IAssetsReferenceLoader<Sprite> _loader;
    // Start is called before the first frame update
    void  Start()
    {
	   w();
    }

    private async void w()
    {
        Debug.Log(x);
        _loader = new AssetsReferenceLoader<Sprite>();
        await _loader.LoadAssetAsync(x);
        _imageRef.sprite = _loader.GetAsset(x);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
