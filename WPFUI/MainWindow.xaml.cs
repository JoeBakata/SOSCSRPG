using System.Windows;
using System.Windows.Documents;
using Engine.EventArgs;
using Engine.ViewModels;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameSession gameSession = new GameSession(); // readonly means the variable, gameSession, can only be set equal to something here 
                                 // where its declared on line 13 or inside a constructor. Not needed but protects us from accidently setting the value somewhere else.

        public MainWindow()// MainWindow constructor
        {
            InitializeComponent();
           
            gameSession.OnMessageRaised += OnGameMessageRaised;
            
            DataContext = gameSession;
        }

        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            gameSession.MoveNorth();
        }

        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            gameSession.MoveEast();
        }

        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            gameSession.MoveSouth();
        }

        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            gameSession.MoveWest();
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)// Lines46 through 49, a new function to attack the monster (OnClick_AttackMonster)
        {
            gameSession.AttackCurrentMonster();// Calls the AttackCurrentMonster function we will create next in the GameSession class(GameSession.cs)
        }
        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        private void OnClick_DisplayTradeScreen(object sender, RoutedEventArgs e)
        {
            TradeScreen tradeScreen = new TradeScreen();
            tradeScreen.Owner = this;
            tradeScreen.DataContext = gameSession;
            tradeScreen.ShowDialog();
        }
    }
}
