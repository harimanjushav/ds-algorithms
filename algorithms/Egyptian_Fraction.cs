/*
Egyptian Fraction tian Fraction: 
	An Egyptian fraction was written as a sum of unit fractions, meaning the numerator is always 1; further, no two denominators can be the same.
	WIKI : https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
	
The Approach :
	The current task is to write a program that calcualtes the ratio of two numbers as an EgyptionFraction
	To achieve this, we can use a greedy algorithm. However, thats not fun! adding some spice to life is always good. 
	Lets try and improve it to return the smallest amount of unit fractions.
	
	How : Caluclate the GCD between the numerator and denominator, then divide the numerator and denorminator by GCD. 
		  If the GCD is 1, then the fraction cannot be simplified. 
*/

public class EgyptionFractions
{
    public static List<int[]> GetFractions(int numerator, int denominator) {
        if (numerator >= denominator)
            throw new ArgumentOutOfRangeException ("denominator");
        if (numerator <= 0)
            throw new ArgumentOutOfRangeException ("numerator");
 
        var fractions = new List<int[]> ();
        int subDenominator = 2;
 
        do {
            // First find the next fraction to substract from that is small enough
            int leftNumerator = numerator * subDenominator;
            while (leftNumerator < denominator) { // Note: rightNumerator == denominator
                subDenominator++;
                leftNumerator += numerator;
            }
 
            // Now we have a valid unit fraction to substract with, lets continue
            // searching for the next unit fraction that yeilds a remainder that 
            // can be simplified (to keep the denominators small).
            while (true) {
                int remainingNumerator = leftNumerator - denominator;
                if(remainingNumerator == 0) {
                    // The fractions are the same
                    numerator = 0;
                    fractions.Add (new [] {1, subDenominator});
                    break;
                }
                int remainingDenominator = denominator * subDenominator;
                int gcd = GCD (remainingNumerator, remainingDenominator);
                if (gcd > 1 || remainingNumerator == 1) {
                    // The resultant fraction can be simplified using this denominator
                    numerator = remainingNumerator / gcd;
                    denominator = remainingDenominator / gcd;
                    fractions.Add (new [] {1, subDenominator});
 
                    // Finished?
                    if(numerator == 1) 
                        fractions.Add (new [] {1, denominator});
                    break;
                }
                subDenominator++;
                leftNumerator += numerator; // i.e. additive version of subDenominator * numerator;
            }
 
            subDenominator++;
        } while (numerator > 1);
 
        return fractions;
    }
 
    private static int GCD(int n1, int n2) {
        if (n2 == 0)
            return n1;
        return GCD (n2, n1 % n2);
    }
}