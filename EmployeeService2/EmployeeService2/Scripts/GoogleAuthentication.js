
function getAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken)
            {
                isUserRegistered(accessToken)
            }
        }
    } 
}

function isUserRegistered(accessToken) {
    $.ajax({
        url: '/api/Account/UserInfo',
        method: 'GET',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            if (response.HasRegistered) {
                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('userName', response.Email);
                window.location.href = "Data.html";
            }

            else
            {
                registerExternalUser(accessToken, response.LoginProvider);
            }
        }
    });
}

function registerExternalUser(accessToken, provider) {
    $.ajax({
        url: '/api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            if (response.HasRegistered) {
                window.location.href = "/api/Account/ExternalLogin?provider=" +
                    provider +
                    "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A58188%2Flogin.html&state=bt57iU3b03haLsd3p_2wUxoYAzKFz8bB9cVZaK7d3OQ1";
            }
      }
    });
}
