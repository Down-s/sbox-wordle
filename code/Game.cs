
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Wordle;
public partial class Wordle : Game
{
	public static int CurrentRow;

	public static WordRow[] WordRows = new WordRow[6];
	public static Keyboard Keyboard;

	public static string Word;
	private static List<string> AllWords = new();

	private static float ResetIn;

	private static string m_CurrentWord = "";
	public static string CurrentWord
	{
		get
		{
			return m_CurrentWord;
		}
		set
		{
			if ( value.Length > 5 ) return;
			m_CurrentWord = value;
		}
	}

	public Wordle()
	{
		if ( IsServer )
			new WordleHud();

		string data = FileSystem.Mounted.ReadAllText( "words.txt" );
		foreach (string str in data.Split( ";" ) )
		{
			string rstr = str.Trim().ToUpper();
			if ( rstr.Length == 5)
				AllWords.Add( rstr );
		}

		ResetGame();
	}

	static bool InitialRestart = true;
	public static void ResetGame()
	{
		Word = AllWords[(new Random()).Next( AllWords.Count )];

		CurrentRow = 0;
		CurrentWord = "";

		if (InitialRestart)
        {
			InitialRestart = false;
        }
		else
		{
			foreach ( WordRow row in WordRows )
			{
				foreach ( LetterBox letter in row.Letters )
				{
					letter.SetClass( "correct", false );
					letter.SetClass( "wrong", false );
					letter.SetClass( "incorrect", false );

					letter.Letter.Text = "";
				}
			}

			foreach ( KeyboardKey key in Keyboard.keyboardKeys )
			{
				key.SetClass( "correct", false );
				key.SetClass( "wrong", false );
				key.SetClass( "incorrect", false );
			}
		}
	}

 	public static void InputWord()
	{
		if ( CurrentWord.Length != 5 ) return;
		if ( !AllWords.Contains(CurrentWord) ) return;

		if (CurrentRow == 5)
		{
			ResetIn = Time.Now + 2.5f;
		}

		WordRow row = WordRows[CurrentRow];
		for ( int i = 0; i < 5; i++ )
		{
			if (Word[i] == CurrentWord[i])
			{
				row.Letters[i].SetClass( "correct", true );
				Keyboard.KeyLookup[CurrentWord[i].ToString()].SetClass( "correct", true );
			}
			else if (Word.Contains(CurrentWord[i]))
			{
				row.Letters[i].SetClass( "wrong", true );
				Keyboard.KeyLookup[CurrentWord[i].ToString()].SetClass( "wrong", true );
			}
			else
			{
				row.Letters[i].SetClass( "incorrect", true );
				Keyboard.KeyLookup[CurrentWord[i].ToString()].SetClass( "incorrect", true );
			}
		}


		if ( CurrentWord == Word )
		{
			ResetIn = Time.Now + 2.5f;
			return;
		}

		CurrentRow++;
		CurrentWord = "";
	}

	public override void Simulate( Client cl )
	{
		base.Simulate( cl );

		if ( ResetIn != 0 && ResetIn < Time.Now )
		{
			ResetIn = 0;
			ResetGame();
		}
	}

	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );
	}
}
