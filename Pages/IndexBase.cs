using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptoTrade.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        IJSRuntime JsRuntime { get; set; }

        [Inject]
        public ISyncLocalStorageService localStorage2 { get; set; }

        public async Task LoginWindow()
        {
            await JsRuntime.InvokeAsync<string>(identifier: "PopUpLogin");
        }

        public string username { get; set; } = null;

        protected override void OnInitialized()
        {
            username = localStorage2.GetItem<string>("username");
        }

        public void Logout()
        {
            localStorage2.Clear();
            StateHasChanged();
        }
    }
}
