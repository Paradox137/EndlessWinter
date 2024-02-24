using System.Net.Mime;
using UniRx;

namespace GameModule.UIModule.MVP.Model
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
