const AUTH0_CLIENT_ID = "YOUR KEY HERE";
const AUTH0_DOMAIN = "dev-hinp90hw.us.auth0.com";
var lock = new Auth0Lock(AUTH0_CLIENT_ID, AUTH0_DOMAIN, {
    auth: {

        params: { scope: 'openid email profile' },
        configurationBaseUrl: 'https://cdn.auth0.com',
        responseType: 'token id_token'
    }
});
function PopUpLogin() {
    localStorage.clear();
    lock.show();
    return false;
};
lock.on("authenticated", function (authResult) {
    lock.getProfile(authResult.accessToken, function (error, profile) {
        if (error) {
            console.log("Auth0 getProfile failed", error);
            return;
        }
        localStorage.setItem('id_token', authResult.idToken);
        show_profile_info(profile);
        $.ajaxPrefilter(function (options) {
            if (!options.beforeSend) {
                options.beforeSend = function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + localStorage.getItem('id_token'));
                }
            }
        });
        dohvacanjeCijena()
        getData();
    });
});
function getItems() {
    $.getJSON('https://blazordb-2a00.restdb.io/rest/data', function (data) {
        console.log(data);
    });
}
var nick = ""; //NICKNAME GLOBALNA VARIJABLA
var show_profile_info = function (profile) {
    console.log(profile.picture);
    console.log(profile.nickname);
    nick = profile.nickname;
};
var rowID;
function getData() {
    var p = 'https://blazordb-2a00.restdb.io/rest/data?q={';
    var u = `"username": "${nick}"}`;
    $.getJSON(p + u, function (data) {
        if (data[0] == undefined) {
            createTodo(nick); //ovde dodat broj stranja = 1
            console.log("NE POSTOJI - STAVLJAM 1000 USDC");
            localStorage.setItem("username", nick);
            localStorage.setItem("userUSDC", 1000);
            localStorage.setItem("userBTC",0);
            localStorage.setItem("userLTC",0);
            localStorage.setItem("userETH",0);
            localStorage.setItem("userEOS",0);
            localStorage.setItem("userLINK", 0);
            localStorage.setItem("brojStanja", 1);
            }
        else {
            var dict = {};
            for (var i = 0; i < data.length; i++) {
                var ime = i;
                dict[ime] = data[i].brojStanja
            }
            var rezultat = Object.keys(dict).reduce((a, b) => dict[a] > dict[b] ? a : b);
            localStorage.setItem("idRow", data[rezultat]._id);
            localStorage.setItem("username", nick);
            localStorage.setItem("userUSDC", data[rezultat].usdc);
            localStorage.setItem("userBTC", data[rezultat].btc);
            localStorage.setItem("userLTC", data[rezultat].ltc);
            localStorage.setItem("userETH", data[rezultat].eth);
            localStorage.setItem("userEOS", data[rezultat].eos);
            localStorage.setItem("userLINK", data[rezultat].link);
            localStorage.setItem("brojStanja", data[rezultat].brojStanja)
        }
    });
}
function createTodo(nickname) {
    var jsondata = {
        "username": `${nickname}`,
        "usdc": 1000,
        "btc": 0,
        "ltc": 0,
        "eth": 0,
        "eos": 0,
        "link": 0,
        "brojStanja": 1
    };
    $.ajax({
        type: "POST",
        url: 'https://blazordb-2a00.restdb.io/rest/data',
        contentType: "application/json",
        data: JSON.stringify(jsondata)
    });
}
    function dohvacanjeCijena() {
        $.getJSON('https://api.coingecko.com/api/v3/coins/usd-coin', function (data) {
            localStorage.setItem("priceEOS", data.market_data.current_price.eos);
            localStorage.setItem("priceBTC", data.market_data.current_price.btc);
            localStorage.setItem("priceETH", data.market_data.current_price.eth);
            localStorage.setItem("priceLTC", data.market_data.current_price.ltc);
            localStorage.setItem("priceLINK", data.market_data.current_price.link);
            localStorage.setItem("priceUSDC", 1)
        });
}
function updatingData() {
    $.ajaxPrefilter(function (options) {
        if (!options.beforeSend) {
            options.beforeSend = function (xhr) {
                xhr.setRequestHeader('x-apikey', 'YOUR KEY HERE');
            }
        }
    });
    var jsondata = {
        "username": `${localStorage.getItem("username")}`,
        "usdc": parseFloat(localStorage.getItem("userUSDC")),
        "btc": parseFloat(localStorage.getItem("userBTC")),
        "ltc": parseFloat(localStorage.getItem("userLTC")),
        "eth": parseFloat(localStorage.getItem("userETH")),
        "eos": parseFloat(localStorage.getItem("userEOS")),
        "link": parseFloat(localStorage.getItem("userLINK")),
        "brojStanja": parseInt(localStorage.getItem("brojStanja"))
    };
    $.ajax({
        type: "POST",
        url: 'https://blazordb-2a00.restdb.io/rest/data',
        contentType: "application/json",
        data: JSON.stringify(jsondata),
        success: function (result) {
            window.location = "/Wallet";
        }
    });
}
function Prefilter() {
    $.ajaxPrefilter(function (options) {
        if (!options.beforeSend) {
            options.beforeSend = function (xhr) {
                xhr.setRequestHeader('x-apikey', '5f5ea9bbc5e01c1e033b8f95');
            }
        }
    });
    $.getJSON('https://blazordb-2a00.restdb.io/rest/data', function (data) {
        console.log(data);
        for (var i = 0; i < data.length; i++) {
            if (data[i].username == localStorage.getItem("username")) {
                document.getElementById("data").innerHTML += '<nav aria-label="breadcrumb">' +
                    '<ol class="breadcrumb">' +
                    '<li class="breadcrumb-item active" aria-current="page">' + "NAME: " + data[i].username + " BTC: " + data[i].btc + " USDC: " + data[i].usdc + " LTC: " + data[i].ltc + " ETH: " + data[i].eth + " EOS: " + data[i].eos + " LINK: " + data[i].link + '</li>' +
                    '</ol >' +
                    '</nav >';
            }
        }
    });
}