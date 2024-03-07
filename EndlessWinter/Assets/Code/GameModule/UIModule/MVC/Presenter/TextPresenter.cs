using GameModule.ProviderModule;
using GameModule.UIModule.MVC.Model;
using Zenject;

namespace GameModule.UIModule.MVC.Presenter
{
    public class TextPresenter
    {
        private readonly TextModel _model;
        private readonly IItemProvider<string> _provider;

        [Inject]
        public TextPresenter(TextModel __model, IItemProvider<string> __provider)
        {
            _model = __model;
            _provider = __provider;
        }
        
        public void OnNext() => _model.Text.Value = _provider.GetItem();
    }
    
}