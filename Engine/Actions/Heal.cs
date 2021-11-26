using System;
using Engine.Models;

namespace Engine.Actions
{
    public class Heal : IAction
    {
        private readonly GameItem _item;
        private readonly int _hitPointsToHeal;

        public event EventHandler<string> OnActionPerformed;
        
        public Heal(GameItem item, int hitPointsToHeal)// Heal constructor. Takes in item & hitPointsToHeal
        {
            if (item.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{item.Name} is not consumable!");
            }

            _item = item;
            _hitPointsToHeal = hitPointsToHeal;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "yourself" : $"the {target.Name.ToLower()}";

            ReportResult($"{actorName} heal {targetName} for {_hitPointsToHeal} point{(_hitPointsToHeal > 1 ? "s" : "")}.");
            target.Heal(_hitPointsToHeal);// I would like to change the message to say "You use Granola bar and heal X points of damage
        }

        private void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }


    }
}
