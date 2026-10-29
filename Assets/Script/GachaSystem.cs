using TMPro;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    [SerializeField] TMP_InputField metalInputField;
    [SerializeField] TMP_InputField bronzeInputField;
    [SerializeField] TMP_InputField silverInputField;
    [SerializeField] TMP_InputField goldInputField;
    [SerializeField] TMP_InputField diamondInputField;
    int[] ReadInputs()
    {
        return new int[]
        {
            ParseInput(metalInputField),
            ParseInput(bronzeInputField),
            ParseInput(silverInputField),
            ParseInput(goldInputField),
            ParseInput(diamondInputField)
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

    private int ParseInput(TMP_InputField field)
    {
        int.TryParse(field.text, out int input);
        return Mathf.Max(0, input);
    }
}
