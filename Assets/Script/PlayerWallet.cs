public class PlayerWallet
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int currentCoins = 200;
    public int gachaCost = 30;

    public bool HasEnoughForGacha()
    {
        return currentCoins >= gachaCost;
    }

    public bool Spend()
    {
        if(!HasEnoughForGacha())
        {
            return false;
        }

        currentCoins -= gachaCost;
        return true;
       
    }

    public void Add(int amount)
    {
        currentCoins += amount;
    }
}
