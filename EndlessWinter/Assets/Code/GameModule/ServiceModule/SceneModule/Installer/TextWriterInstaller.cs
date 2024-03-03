using GameModule.SettingsModule;
using GameModule.UIModule.MVP.Installers;
using GameModule.UIModule.MVP.Presenter;
using UnityEngine;
using Zenject;

namespace GameModule.ServiceModule.SceneModule
{
    public class TextWriterInstaller : MonoInstaller
    {
        [SerializeField] private CharsTimeDelayConfig _charsTimeDelaySettings;
        public override void InstallBindings()
        {
            Container.Bind<TextWriterService>().FromNew().AsSingle().WithArguments(_charsTimeDelaySettings).CopyIntoAllSubContainers();
            
            Container.Bind<ItemTextProvider>().FromNew().AsSingle().CopyIntoAllSubContainers();
            Debug.Log("HERE 1");
        }
    }
}