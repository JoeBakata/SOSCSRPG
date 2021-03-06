using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Engine.EventArgs;
using Engine.Models;
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
        private readonly Dictionary<Key, Action> _userInputActions = new Dictionary<Key, Action>();
       
        public MainWindow()// MainWindow constructor
        {
            InitializeComponent();

            InitializeUserInputActions();
           
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
        
        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
        {
            gameSession.UseCurrentConsumable();
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

        private void OnClick_Craft(object sender, RoutedEventArgs e)
        {
            Recipe recipe = ((FrameworkElement)sender).DataContext as Recipe;
            gameSession.CraftItemUsing(recipe);
        }


        private void InitializeUserInputActions()
        {
            _userInputActions.Add(Key.W, () => gameSession.MoveNorth());
            _userInputActions.Add(Key.A, () => gameSession.MoveWest());
            _userInputActions.Add(Key.S, () => gameSession.MoveSouth());
            _userInputActions.Add(Key.D, () => gameSession.MoveEast());
            _userInputActions.Add(Key.Z, () => gameSession.AttackCurrentMonster());
            _userInputActions.Add(Key.C, () => gameSession.UseCurrentConsumable());
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_userInputActions.ContainsKey(e.Key))
            {
                _userInputActions[e.Key].Invoke();
            }
        }
    }
}

