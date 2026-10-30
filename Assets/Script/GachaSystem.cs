using UnityEngine;
public class GachaSystem
{
    
    public Rarity Roll(int[] inputs)
    {
       int total = 0;
        foreach (int weight in inputs)
        {
            total += weight;
        }

        int randomValue = Random.Range(0, total);
        int cumulativeInput = 0;

        for (int i = 0; i < inputs.Length; i++)
        {
            cumulativeInput += inputs [i];
            if (randomValue < cumulativeInput)
            {
                return (Rarity)i;
            }
        }

        return Rarity.Metal; // Default case, should not reach here
    }
}
