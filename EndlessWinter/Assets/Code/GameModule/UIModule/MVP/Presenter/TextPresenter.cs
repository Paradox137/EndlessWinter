using GameModule.UIModule.MVP.Model;
using Zenject;

namespace GameModule.UIModule.MVP.Presenter
{
    public class TextPresenter
    {
        private readonly TextDataType _textDataType;
        private readonly TextModel _model;
        private readonly ItemTextProvider _provider;

        [Inject]
        public TextPresenter(TextModel __model, ItemTextProvider __provider, TextDataType __textDataType)
        {
            _model = __model;
            _provider = __provider;
            _textDataType = __textDataType;
        }

        public void OnNext() => _model.Text.Value = _provider.GetNextText(_textDataType);
    }

    public enum TextDataType
    {
        ActorName,
        Narration
    }
}