using System.Collections.Generic;

using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Wordle;
public class WordRow : Panel
{
	public List<LetterBox> Letters = new();
	public int Index;

	public WordRow(int index)
	{
		Index = index;
		for ( int i = 0; i < 5; i++ )
		{
			Letters.Add( AddChild<LetterBox>( "box" ) );
		}
	}

	public override void Tick()
	{
		if ( Index != Wordle.CurrentRow ) return;

		for ( int i = 0; i < 5; i++ )
		{
			if (Wordle.CurrentWord.Length > i)
			{
				string letter = Wordle.CurrentWord[i].ToString();
				Letters[i].Letter.Text = letter;
			}
			else
			{
				Letters[i].Letter.Text = "";
			}
		}
	}
}
