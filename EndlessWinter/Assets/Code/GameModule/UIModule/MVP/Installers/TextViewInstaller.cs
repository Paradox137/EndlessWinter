using GameModule.ServiceModule;
using GameModule.SettingsModule;
using GameModule.UIModule.MVP.Model;
using GameModule.UIModule.MVP.Presenter;
using GameModule.UIModule.MVP.View;
using SharedModule.UIModule.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameModule.UIModule.MVP.Installers
{
    //todo: s : provider
    public abstract class TextViewInstaller<T> : MonoInstaller
        where T : TextView
    {
        [SerializeField] private TextMeshProUGUI _textComponent;
        [SerializeField] private TextDataType _dataType;
        public override void InstallBindings()
        {
           Container.BindInterfacesAndSelfTo<T>()
                .FromSubContainerResolveAll()
                .ByMethod(InstallFacade)
                .AsSingle()
                .NonLazy();
        }

        private void InstallFacade(DiContainer subContainer)
        {
            //subContainer.Bind<ItemTextProvider>().FromNew().AsSingle();
            subContainer.Bind<TextModel>().FromNew().AsSingle();
            subContainer.Bind<TextPresenter>().AsSingle().WithArguments(_dataType);
            subContainer.BindInterfacesAndSelfTo<T>().FromNew().AsSingle().WithArguments(_textComponent);
        }
    }
}