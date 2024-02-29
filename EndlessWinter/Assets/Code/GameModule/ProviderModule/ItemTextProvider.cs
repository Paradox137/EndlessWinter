using GameModule.StorageModule;
using UnityEngine;
using Zenject;

namespace GameModule.UIModule.MVP.Presenter
{
        //todo: abstract
    public class ItemTextProvider 
    {
        private NovelStorage Storage;
        //todo: убрать из сабконтейнера
        public ItemTextProvider(NovelStorage __storage)
        {
            Storage = __storage;
        }

        public string GetNextText(TextDataType __type)
        {
            return __type switch
            {
                TextDataType.Narration => Storage._actor.startReplicas.Peek(),
                
                //todo: dataconverter по ref dataextinsions 
                TextDataType.ActorName => Storage._actor.actorName.ToString(),
                _ => null
            };
        }
    }
}