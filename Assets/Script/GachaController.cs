using UnityEngine;
using UnityEngine.UI;

public class GachaController : MonoBehaviour
{
    [Header("Input Field")]
    [SerializeField] InputField metalInputField;
    [SerializeField] InputField bronzeInputField;
    [SerializeField] InputField silverInputField;
    [SerializeField] InputField goldInputField;
    [SerializeField] InputField diamondInputField;

    [Header("Rarity Sprites")]
    [SerializeField] private Transform storageContent;
    [SerializeField] private Button storageItem;
    [SerializeField] private Sprite[] raritySprites;

    [Header("Coins Text")]
    [SerializeField] private Text coinText;
    [SerializeField] private Text storageValueText;

    private PlayerWallet wallet;
    private StorageManager storage;
    private GachaSystem gacha;

    private void Awake()
    {
        wallet = new PlayerWallet();
        storage = new StorageManager();
        gacha = new GachaSystem();
    }

    private void Start()
    {
        RefreshCoinText();
        RefreshStorageView();
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

        RefreshUI();
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

    private void CreateItemView(Rarity rarity)
    {
        Button items = Instantiate(storageItem, storageContent);
        Image itemImages = items.GetComponent<Image>();
        itemImages.sprite = raritySprites[(int)rarity];

        items.onClick.AddListener(() =>SellItem(rarity));
    }

    private void RefreshStorageView()
    {
        foreach (Transform child in storageContent)
        {
            Destroy(child.gameObject);
        }
        foreach (Rarity rarity in storage.Items)
        {
            CreateItemView(rarity);
        }
    }

    private void RefreshCoinText()
    {
        coinText.text = "Your total coin: " + wallet.currentCoins;
        storageValueText.text = "Storage coin value: " + storage.TotalValue;
    }

    private void RefreshUI()
    {
        RefreshStorageView();
        RefreshCoinText();
    }

    private void SellItem(Rarity rarity)
    {
        if(!storage.RemoveItem(rarity))
        {
            return;
        }

        wallet.Add(storage.SellValue(rarity));
        RefreshUI();
    }
}
