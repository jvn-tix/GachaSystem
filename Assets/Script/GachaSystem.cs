using UnityEngine.UI;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    [SerializeField] InputField metalInputField;
    [SerializeField] InputField bronzeInputField;
    [SerializeField] InputField silverInputField;
    [SerializeField] InputField goldInputField;
    [SerializeField] InputField diamondInputField;
    int[] ReadInputs()
    {
        return new int[]
        {
            ParseInput(metalInputField.text),
            ParseInput(bronzeInputField.text),
            ParseInput(silverInputField.text),
            ParseInput(goldInputField.text),
            ParseInput(diamondInputField.text)
        };
    }
    Rarity Roll(int[] inputs)
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

    private int ParseInput(string text)
    {
        int.TryParse(text, out int input);
        return Mathf.Max(0, input);
    }
}
