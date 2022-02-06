using System.Collections.Generic;

using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Wordle;
public class Keyboard : Panel
{
	public Dictionary<string, KeyboardKey> KeyLookup = new();
	public List<KeyboardKey> keyboardKeys = new List<KeyboardKey>();
	public static readonly List<string> Layout = new List<string>()
	{
		"q",
		"w",
		"e",
		"r",
		"t",
		"y",
		"u",
		"i",
		"o",
		"p",
		"\n",
		"a",
		"s",
		"d",
		"f",
		"g",
		"h",
		"j",
		"k",
		"l",
		"\n",
		"<",
		"z",
		"x",
		"c",
		"v",
		"b",
		"n",
		"m",
		"go"
	};

	public Keyboard()
	{
		Wordle.Keyboard = this;

		Panel row = Add.Panel( "row" );
		foreach ( string key in Layout )
		{
			if (key == "\n")
			{
				row = Add.Panel( "row" );
				continue;
			}

			KeyboardKey kbkey = new KeyboardKey( key.ToUpper() );
			row.AddChild( kbkey );

			keyboardKeys.Add( kbkey );
			KeyLookup.Add( key.ToUpper(), kbkey );
		}
	}

	public override void Tick()
	{

	}
}
