using System;
using System.Security.Cryptography;

namespace Engine
{
    // This is the more complex version
    public static class RandomNumberGenerator
    {
        private static readonly RNGCryptoServiceProvider generator = new RNGCryptoServiceProvider();

        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];
            generator.GetBytes(randomNumber);
            double asciiValueOfRandomChaacter = Convert.ToDouble(randomNumber[0]);

            // Using Math.Max, and subtracting 0.00000000001,
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomChaacter / 255d) - 0.00000000001d);

            // Add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomeValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomeValueInRange);
        }

    }
}
