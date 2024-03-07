using GameModule.ProviderModule;
using GameModule.ProviderModule.Text;
using GameModule.UIModule.MVC.Model;
using GameModule.UIModule.MVC.Presenter;
using GameModule.UIModule.MVC.View.Text;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameModule.UIModule.MVC.Installers.Contracts
{
    public abstract class TextViewInstaller<T> : MonoInstaller
        where T : TextView
    {
        [SerializeField] private TextMeshProUGUI _textComponent;
        [SerializeField] private ProviderID _providerID;
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
            TextProvider textProvider = Container.ResolveId<TextProvider>(_providerID);
            
            subContainer.Bind<TextModel>().FromNew().AsSingle();
            subContainer.Bind<TextPresenter>().AsSingle().WithArguments(textProvider);
            subContainer.BindInterfacesAndSelfTo<T>().FromNew().AsSingle().WithArguments(_textComponent);
        }
    }
}