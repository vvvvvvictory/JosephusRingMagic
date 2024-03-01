namespace JosephusRingMagic
{
  internal class Poker
  {
    public static readonly string Spades = "Spades";     // 黑桃
    public static readonly string Hearts = "Hearts";     // 红桃
    public static readonly string Clubs = "Clubs";       // 梅花
    public static readonly string Diamonds = "Diamonds"; // 方块
    private static readonly string[] PokerUriStrings;

    static Poker()
    {
      PokerUriStrings = new string[55];
      string[] pokerSuits = { Spades, Hearts, Clubs, Diamonds };
      int i = 0;
      foreach (string suit in pokerSuits) {
        for (int j = 0; j < 13; ++j) {
          PokerUriStrings[i++] =
            $"pack://application:,,,/JosephusRingMagic;component/images/{suit}_{j + 1}.png";
        }
      }

      // 添加大小王和背面花色
      PokerUriStrings[i++] =
        "pack://application:,,,/JosephusRingMagic;component/images/Joker_1.png";
      PokerUriStrings[i++] =
        "pack://application:,,,/JosephusRingMagic;component/images/Joker_2.png";
      PokerUriStrings[i] =
        "pack://application:,,,/JosephusRingMagic;component/images/back.png";
    }

    /// <summary>
    /// 获取随机的n张牌
    /// </summary>
    /// <param name="n">希望获得几张牌</param>
    /// <returns></returns>
    public static string[] GetRandomCard(int n)
    {
      if (n is > 54 or < 1) {
        throw new ArgumentException($"n must satisfy 0 < n < 55. but got {n}");
      }

      Random rnd = new Random();
      HashSet<int> rndIndex = new HashSet<int>();
      while (rndIndex.Count < n) {
        rndIndex.Add(rnd.Next(54));
      }

      string[] ans = new string[n];
      int i = 0;
      foreach (int j in rndIndex) {
        ans[i++] = PokerUriStrings[j];
      }

      return ans;
    }

    /// <summary>
    /// J: 11, Q: 12, K: 13, 小王: 14, 大王: 15, 背面: 16
    /// </summary>
    /// <param name="suit">花色</param>
    /// <param name="order">序号</param>
    /// <returns></returns>
    public static string GetCard(string suit, int order)
    {
      return order switch {
        < 1 or > 16 => throw new ArgumentException("illegal order"),
        > 13 and <= 16 => PokerUriStrings[52 + order - 13 - 1],
        _ => suit switch {
          "Spades" => PokerUriStrings[order - 1],
          "Hearts" => PokerUriStrings[13 + order - 1],
          "Clubs" => PokerUriStrings[26 + order - 1],
          "Diamonds" => PokerUriStrings[39 + order - 1],
          _ => throw new ArgumentException("illegal suits or order")
        }
      };
    }
  }
}