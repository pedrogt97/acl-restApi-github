using Microsoft.AspNetCore.Mvc;
using Octokit;

public class HomeController : Controller
{
    
    const string clientId = "59bce3ccbae90be33fed";
    private const string clientSecret = "40ee697597f0744af0f783637fd3f450cd7f09ae";
    readonly GitHubClient client =
        new GitHubClient(new ProductHeaderValue("api-gh-terra"));

    public async Task<ActionResult> Index()
    {
            return Redirect(GetOauthLoginUrl());
    }

    // public async Task<ActionResult> Index()
    // {
    //         return Json(GetDeviceAuth());
    // }

    // This is the Callback URL that the GitHub OAuth Login page will redirect back to.
    public async Task<ActionResult<OauthToken>> autenticar(string code, string state)
    {
        
        if (!String.IsNullOrEmpty(code))
        {
            var token = await client.Oauth.CreateAccessToken(
                new OauthTokenRequest(clientId, clientSecret, code)
                );

            client.Credentials = new Credentials(token.AccessToken);

            return Json(token);
            
            //var mudandoHook = await client.Repository.Hooks.Edit()

            //return token;
            //return repos.ToString() + Environment.NewLine + novoRepo.ToString() + Environment.NewLine + branchesDeUmRepo.ToString() + Environment.NewLine + addHookEmRepo.ToString() + Environment.NewLine + hooksDeUmRepo.ToString();
        }

        var token2 = new OauthToken();
        return token2;
        //return "";

    }

    private string GetOauthLoginUrl()
    {
        var request = new OauthLoginRequest(clientId)
        {
            Scopes = {"user", "notifications", "public_repo"},
        };

        //client.Oauth.CreateAccessToken
        var oauthLoginUrl = client.Oauth.GetGitHubLoginUrl(request);

        //
        // var iniRequest = new OauthDeviceFlowRequest(clientId);

        // var oAuthDeviceFlow = client.Oauth.InitiateDeviceFlow(iniRequest);

        //return oAuthDeviceFlow.Result.VerificationUri;
        //
        return oauthLoginUrl.ToString();
    }

    private OauthDeviceFlowResponse GetDeviceAuth()
    {
        var request = new OauthLoginRequest(clientId)
        {
            Scopes = {"user", "notifications", "public_repo"},
        };

        //client.Oauth.CreateAccessToken
        var oauthLoginUrl = client.Oauth.GetGitHubLoginUrl(request);

        //
        var iniRequest = new OauthDeviceFlowRequest(clientId);

        var oAuthDeviceFlow = client.Oauth.InitiateDeviceFlow(iniRequest);

        //client.Oauth.CreateAccessTokenForDeviceFlow()
        return oAuthDeviceFlow.Result;
        //
        //return oauthLoginUrl.ToString();
    }

    // public async Task<ActionResult> Emojis()
    // {
    //     var emojis = await client.Miscellaneous.GetEmojis();

    //     return View(emojis);
    // }
}