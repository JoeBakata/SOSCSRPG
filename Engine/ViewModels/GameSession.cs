using System;
using System.Linq;
using Engine.EventArgs;
using Engine.Factories;
using Engine.Models; // Added this so it looks in Engine.Models for Player

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;

        #region Properties

        private Player currentPlayer;// Backing variable
        private Location currentLocation;// Backing variable
        private Monster currentMonster;// Backing variable
        private Trader currentTrader;// Backing variable

        public World CurrentWorld { get; }

        public Player CurrentPlayer // Player was not found, had to add a using statement to fix.
        {
            get { return currentPlayer; }
            set
            {
                if (currentPlayer != null)
                {
                    currentPlayer.OnActionPerformed -= OnCurrentPlayerPerformedAction;
                    currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                }

                currentPlayer = value;

                if (currentPlayer != null)
                {
                    currentPlayer.OnActionPerformed += OnCurrentPlayerPerformedAction;
                    currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    currentPlayer.OnKilled += OnCurrentPlayerKilled;
                }
            }
        } 

        public Location CurrentLocation 
        {
            get { return currentLocation; }
            set
            {
                currentLocation = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToWest));

                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();

                CurrentTrader = CurrentLocation.TraderHere;// Sets currentTrader when player moves to new location
            }
        }

        public Monster CurrentMonster
        {
            get { return currentMonster; }
            set
            {
                if (currentMonster != null)
                {
                    currentMonster.OnActionPerformed -= OnCurrentMonsterPerformedAction;
                    currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                currentMonster = value;

                if (currentMonster != null)
                {
                    currentMonster.OnActionPerformed += OnCurrentMonsterPerformedAction;
                    currentMonster.OnKilled += OnCurrentMonsterKilled;

                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
            }
        }
        
        public Trader CurrentTrader// Add new property CurrentTrader
        {
            get { return currentTrader; }
            set
            {
                currentTrader = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTrader));
            }
        }

        public bool HasLocationToNorth =>
                CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;// Converted to a lambda. This doesn’t change the code. But, it is a little easier to read

        public bool HasLocationToEast =>
               CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;// Converted to a lambda. This doesn’t change the code. But, it is a little easier to read

        public bool HasLocationToSouth =>
                CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;// Converted to a lambda. This doesn’t change the code. But, it is a little easier to read

        public bool HasLocationToWest =>
                CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;// Converted to a lambda. This doesn’t change the code. But, it is a little easier to read

        public bool HasMonster => CurrentMonster != null; // Lets us know if the location has a monster.
                                                          // =>, called Lambda, is an expression body. Same as saying return whatever the calculation is. In this case, returns CurrentMonster property not equal to null
        public bool HasTrader => CurrentTrader != null;// Create boolean property
        // HasTrader displays the 'Trade' button if at location with a CurrentTrader
        #endregion 

        public GameSession() // Constructor
        {
            CurrentPlayer = new Player("Jera", "Fighter", 0, 10, 10, 1000000);

            if (!CurrentPlayer.Weapons.Any())// If not any weapons
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));// check the player’s Weapons property. If there are not any objects in that property,
            }                                                                      // we will get a Pointy Stick (item 1001) from the ItemFactory, and give it to the player

            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(2001));
            CurrentPlayer.LearnRecipe(RecipeFactory.RecipeByID(1));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3001));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3002));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3003));

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }
        #region Move N,E,S, or W
        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }

        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }

        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }

        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }
        #endregion Move N,E,S or W
        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                QuestStatus questToComplete = 
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && 
                                                            !q.IsCompleted);

                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        CurrentPlayer.RemoveItemsFromInventory(quest.ItemsToComplete);

                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest");

                        // Give the player the quest rewards
                        RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");
                        CurrentPlayer.AddExperience(quest.RewardExperiencePoints);

                        RaiseMessage($"You receive {quest.RewardGold} gold");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);

                            RaiseMessage($"You receive a {rewardItem.Name}");
                            CurrentPlayer.AddItemToInventory(rewardItem);
                        }

                        // Mark the Quest as completed
                        questToComplete.IsCompleted = true;// Cannot be assigned to, it is read only. Added set; to bool IsCompleted in the QuestStatus.cs to fix this
                    }
                }
            }
        }
        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere) 
            {
                if(!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));

                    RaiseMessage("");
                    RaiseMessage($"You receive the \n'{quest.Name}' quest.");
                    RaiseMessage(quest.Description);

                    RaiseMessage($"Return with");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($" {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    RaiseMessage("And you will receive:");
                    RaiseMessage($" {quest.RewardExperiencePoints} experience points");
                    RaiseMessage($" {quest.RewardGold} gold");
                    foreach(ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($" {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        public void AttackCurrentMonster()// Function to attack current monster
        {
            if (CurrentPlayer.CurrentWeapon == null)// Lines167-171, we check if there is no weapon selected
            {
                RaiseMessage("You must select a weapon, to attack.");
                return;// todo This method is void, why use a return statement here***
            }

            CurrentPlayer.UseCurrentWeaponOn(CurrentMonster);
            // If monster is killed, collect rewards
            if (CurrentMonster.IsDead)
            {
                // Get another monster to fight
                GetMonsterAtLocation();
            }
            else
            {
                // If monster is still alive, let the monster attack
                CurrentMonster.UseCurrentWeaponOn(CurrentPlayer);
            }
        }

        public void UseCurrentConsumable()
        {
            CurrentPlayer.UseCurrentConsumable();// Called a helper function
        }

        public void CraftItemUsing(Recipe recipe)
        {
            if (CurrentPlayer.HasAllTheseItems(recipe.Ingredients))
            {
                CurrentPlayer.RemoveItemsFromInventory(recipe.Ingredients);

                foreach (ItemQuantity itemQuantity in recipe.OutputItems)
                {
                    for (int i = 0; i < itemQuantity.Quantity; i++)
                    {
                        GameItem outputItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                        CurrentPlayer.AddItemToInventory(outputItem);
                        RaiseMessage($"You craft 1 {outputItem.Name}");
                    }
                }
            }
            else
            {
                RaiseMessage("You do not have the required ingredients:");
                foreach (ItemQuantity itemQuantity in recipe.Ingredients)
                {
                    RaiseMessage($"  {itemQuantity.Quantity} {ItemFactory.ItemName(itemQuantity.ItemID)}");
                }
            }
        }

        private void OnCurrentPlayerPerformedAction(object sender, string result)
        {
            RaiseMessage(result);
        }

        private void OnCurrentMonsterPerformedAction(object sender, string result)
        {
            RaiseMessage(result);
        }
        private void OnCurrentPlayerKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage("You have been killed.");

            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.CompletelyHeal();
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!");

            RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
            CurrentPlayer.AddExperience(currentMonster.RewardExperiencePoints);

            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            CurrentPlayer.ReceiveGold(currentMonster.Gold);

            foreach (GameItem gameItem in CurrentMonster.Inventory)
            {
                RaiseMessage($"You receive one {gameItem.Name}.");
                currentPlayer.AddItemToInventory(gameItem);
            }
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage($"You are now level {CurrentPlayer.Level}!");
            RaiseMessage($"Your new Maximum HitPoints are {CurrentPlayer.MaximumHitPoints}");
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}
