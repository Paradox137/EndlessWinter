using Zenject;

namespace GameModule.ProviderModule.Text
{
    public class TextProvider : IItemProvider<string>
    {
        private string _text;
        public string Text
        {
            get => _text;
            set => _text = value;
        }
        
        [Inject]
        public TextProvider()
        {
            
        }

        public string GetItem()
        {
            return _text;
        }
    }
}