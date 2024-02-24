using GameModule.DataModule;
using Newtonsoft.Json;
using UnityEngine;

namespace GameModule.StorageModule
{
    public class NovelStorage
    {
        public Actor _actor;

        public void SetNewActor(TextAsset text)
        {
            _actor = JsonConvert.DeserializeObject<Actor>(text.text);
            
            Debug.Log(text.text);
		
            if (_actor != null)
                Debug.Log(_actor.actorName);
        }
        public NovelStorage()
        {
            
        }
    }
}