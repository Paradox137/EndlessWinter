using UniRx;

namespace GameModule.UIModule.MVC.Model
{
	public class TextModel
	{
		 public ReactiveProperty<string> Text { get; set; }

		 public TextModel()
		 {
			 Text = new ReactiveProperty<string>();
		 }
	}
}
