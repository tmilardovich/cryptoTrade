using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptoTrade.Pages
{
    public class WalletBase : ComponentBase
    {
        [Inject]
        public ISyncLocalStorageService localStorage { get; set; }
        protected string ButtonText { get; set; } = "Show only tradeable";
        protected string CssClass { get; set; } = null;
        protected void Button_Click()
        {
            if (ButtonText == "Show all")
            {
                ButtonText = "Show only tradeable";
                CssClass = null;
            }
            else
            {
                CssClass = "HideRows";
                ButtonText = "Show all";
            }
        }
        public UserData podaci;
        protected override void OnInitialized()
        {
            string _username = localStorage.GetItem<string>("username");
            string _usdc = localStorage.GetItem<string>("userUSDC");
            string _btc = localStorage.GetItem<string>("userBTC");
            string _ltc = localStorage.GetItem<string>("userLTC");
            string _eth = localStorage.GetItem<string>("userETH");
            string _eos = localStorage.GetItem<string>("userEOS");
            string _link = localStorage.GetItem<string>("userLINK");
            podaci = new UserData(_username, _usdc, _btc, _ltc, _eth, _eos, _link);
            CalculateInDollars(podaci);
        }
        public double valueInDollars = 0.0;
        public class UserData
        {
            public string Name { get; set; }
            public string USDC { get; set; }
            public string BTC { get; set; }
            public string LTC { get; set; }
            public string ETH { get; set; }
            public string EOS { get; set; }
            public string LINK { get; set; }
            public UserData(string _name, string _usdc, string _btc, string _ltc, string _eth, string _eos, string _link)
            {
                Name = _name;
                USDC = _usdc;
                BTC = _btc;
                LTC = _ltc;
                ETH = _eth;
                EOS = _eos;
                LINK = _link;
            }
        }
        public void CalculateInDollars(UserData data)
        {
            string btcPrice = localStorage.GetItem<string>("priceBTC");
            var b = double.Parse(btcPrice.Replace(".", ","));
            string eosPrice = localStorage.GetItem<string>("priceEOS");
            var e = double.Parse(eosPrice.Replace(".", ","));
            string ethPrice = localStorage.GetItem<string>("priceETH");
            var et = double.Parse(ethPrice.Replace(".", ","));
            string ltcPrice = localStorage.GetItem<string>("priceLTC");
            var l = double.Parse(ltcPrice.Replace(".", ","));
            string linkPrice = localStorage.GetItem<string>("priceLINK");
            var li = double.Parse(linkPrice.Replace(".", ","));
            valueInDollars = Math.Round((1 / b) * double.Parse(data.BTC) +
                (1 / e) * double.Parse(data.EOS) +
                (1 / et) * double.Parse(data.ETH) +
                (1 / l) * double.Parse(data.LTC) +
                (1 / li) * double.Parse(data.LINK) +
                double.Parse(data.USDC), 2);
        }
    }
}
