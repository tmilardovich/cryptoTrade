using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptoTrade.Pages
{
    public class PricesBase : ComponentBase
    {
        [Inject]
        public ISyncLocalStorageService localStorage { get; set; }

        public bool ShowGraph { get; set; } = true;

        public List<CryptoPrice> cpData;
        public string btcPrice;

        protected override void OnInitialized()
        {
            cpData = new List<CryptoPrice>();
            btcPrice = localStorage.GetItem<string>("priceBTC");
            string eosPrice = localStorage.GetItem<string>("priceEOS");
            string ethPrice = localStorage.GetItem<string>("priceETH");
            string ltcPrice = localStorage.GetItem<string>("priceLTC");
            string linkPrice = localStorage.GetItem<string>("priceLINK");
            cpData.Add(new CryptoPrice("bitcoin", btcPrice, "img/bitcoinLogo.png"));
            cpData.Add(new CryptoPrice("eos", eosPrice, "img/eos.png"));
            cpData.Add(new CryptoPrice("ethereum", ethPrice, "img/ethereumLogo.png"));
            cpData.Add(new CryptoPrice("litecoin", ltcPrice, "img/litecoinLogo.png"));
            cpData.Add(new CryptoPrice("link", linkPrice, "img/chainlinkLogo.png"));
            cpData.Add(new CryptoPrice("0x", "2,01751886", "img/zrx.png"));
            cpData.Add(new CryptoPrice("basic-attention-token", "3,79064626", "img/batLogo.png"));
            cpData.Add(new CryptoPrice("dai", "0,97501368", "img/daiLogo.png"));
            cpData.Add(new CryptoPrice("monero", "0,01142506", "img/monero.png"));
        }
        public class CryptoPrice
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string Image { get; set; }
            public CryptoPrice(string coinName, string coinValue, string img)
            {
                Name = coinName;
                Value = coinValue;
                Image = img;
            }
        }
    }
}
