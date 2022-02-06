using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Wordle;
public class LetterBox : Panel
{
	public Label Letter;

	public LetterBox()
	{
		Letter = Add.Label( "", "letter" );
	}

	public override void Tick()
	{

	}
}
