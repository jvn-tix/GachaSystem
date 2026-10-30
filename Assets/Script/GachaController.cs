using UnityEngine;
using UnityEngine.UI;

public class GachaController : MonoBehaviour
{
    [SerializeField] InputField metalInputField;
    [SerializeField] InputField bronzeInputField;
    [SerializeField] InputField silverInputField;
    [SerializeField] InputField goldInputField;
    [SerializeField] InputField diamondInputField;

    private PlayerWallet wallet;
    private StorageManager storage;
    private GachaSystem gacha;

    private void Awake()
    {
        wallet = new PlayerWallet();
        storage = new StorageManager();
        gacha = new GachaSystem();
    }
    public void TryGacha()
    {
        int[] weights = ReadInputs();

        if (!wallet.HasEnoughForGacha())
        {
            Debug.Log("Not enough coin");
            return;
        }
       
        if(GetTotal(weights) == 0)
        {
            Debug.Log("Invalid percentages: total cannot be zero");
            return;
        }

        wallet.Spend();
        Rarity rarity = gacha.Roll(weights);
        storage.AddItem(rarity);

        Debug.Log("Got: " + rarity);
    }
    private int[] ReadInputs()
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

    private int ParseInput(string text)
    {
        int.TryParse(text, out int input);
        return Mathf.Max(0, input);
    }

    private int GetTotal(int[] inputs)
    {
        int total = 0;
        foreach (int weight in inputs)
        {
            total += weight;
        }

        return total;
    }
}
