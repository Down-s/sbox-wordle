using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Wordle;
public class KeyboardKey : Panel
{
	public Label Letter;

	public KeyboardKey(string c)
	{
		Letter = Add.Label( c, "char" );
	}

	protected override void OnClick( MousePanelEvent e )
	{
		switch (Letter.Text)
		{
			case "<":
				if ( Wordle.CurrentWord.Length == 0 ) return;

				Wordle.CurrentWord = Wordle.CurrentWord[..^1];
				break;
			case "GO":
				Wordle.InputWord();
				break;
			default:
				Wordle.CurrentWord += Letter.Text.ToUpper();
				break;
		}
	}

	public override void Tick()
	{

	}
}
