namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public string ImageName { get; }

        public int RewardExperiencePoints { get; }

        public Monster(string name, string imageName,
            int maximumHitPoints, int currentHitPoints,
            int rewardExperiencePoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold)
        {
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";// $ means string interpolation with curly brace pair with the variable name inside of it.  Consistant with string concatenation
            RewardExperiencePoints = rewardExperiencePoints;
        }
    }
}
