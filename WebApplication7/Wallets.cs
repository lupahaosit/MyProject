using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7
{
    public class Wallets
    {
        public List<wallet> TakeWallets()
        {
            List<wallet> wallets = new List<wallet>();
            var web = new HtmlWeb();
            var html = web.Load("https://cbr.ru/currency_base/daily/");
            var elements = html.DocumentNode.SelectNodes("//div[@class='table']");
            List<string> walletItems = new List<string>();
            if (elements != null)
            {

                foreach (var item in elements)
                {
                    string[] separator = { "\n", "\r", "     ", null };
                    var result = item.InnerText.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 3; i < result.Length - 7; i += 7)
                    {
                        wallet walletItem = new wallet()
                        {
                            Cod = result[i],
                            Symbols = result[i + 1],
                            Amount = result[i + 2],
                            walletName = result[i + 3],
                            Curse = result[i + 4],

                        };
                        wallets.Add(walletItem);
                    }
                }
            }         
            return wallets;
        }
    }
}
        
