namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public string ImageName { get; }
        public int MinimumDamage { get; }// add two new properties to the Monster class: MinimumDamage and MaximumDamage (both integer properties)
        public int MaximumDamage { get; }// add two new properties to the Monster class: MinimumDamage and MaximumDamage (both integer properties)

        public int RewardExperiencePoints { get; }

        public Monster(string name, string imageName,
            int maximumHitPoints, int currentHitPoints,
            int minimumDamage, int maximumDamage,// To populate these properties, we’ll add two new parameters to the Monster constructor
            int rewardExperiencePoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold)
        {
            ImageName = $"/Engine;component/Images/Monsters/{imageName}";// $ means string interpolation with curly brace pair with the variable name inside of it.  Consistant with string concatenation
            MinimumDamage = minimumDamage;// and set the properties with the passed-in parameter values
            MaximumDamage = maximumDamage;// and set the properties with the passed-in parameter values
            RewardExperiencePoints = rewardExperiencePoints;
        }
    }
}
