using GameModule.BusinessLogicModule.PlayerUIActions;
using GameModule.ConfigsModule;
using GameModule.ServiceModule.InGameModule;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameModule.ServiceModule.SceneModule
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private CharsTimeDelayConfig _charsTimeDelaySettings;
        [SerializeField] private TextViewConfig _textViewConfig;
        
        [SerializeField] private RectTransform _panelRect;
        [SerializeField] private GameObject _perksParent;
        [SerializeField] private GameObject _perkChild;

        [SerializeField] private Button _backToMainMenuButton;
        public override void InstallBindings()
        {
            Container.Bind<TextWriterService>().FromNew().AsSingle().WithArguments(_charsTimeDelaySettings, _textViewConfig);
            Container.Bind<UIPerkViewerService>().FromNew().AsSingle().WithArguments(_panelRect, _perksParent, _perkChild).NonLazy();
            
            Container.Bind<IImageService>().WithId(ServiceID.ActorImage).To<ActorImageService>().FromNew().AsSingle();
            Container.Bind<IImageService>().WithId(ServiceID.MainImage).To<MainImageService>().FromNew().AsSingle();

            InGameAction inGameAction = Container.Resolve<InGameAction>();
            _backToMainMenuButton.onClick.AddListener(() => inGameAction.Rise(InGameLogicItem.BackToMainMenu));
        }
    }
}