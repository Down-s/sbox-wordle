using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Wordle;
public class WordleHud : HudEntity<RootPanel>
{
	public WordleHud()
	{
		if ( !IsClient )
			return;

		RootPanel.StyleSheet.Load( "/styles/main.scss" );

		var Container = RootPanel.Add.Panel( "container" );

		Container.Add.Panel( "header" ).Add.Label( "S&Wordle", "logo" );
		var WordsContainer = Container.Add.Panel( "wordcontainer" );
	
		for (int i = 0; i < 6; i++)
		{
			WordRow row = new WordRow( i );
			WordsContainer.AddChild( row );

			Wordle.WordRows[i] = row;
		}

		Container.AddChild<Keyboard>();
	}
}
