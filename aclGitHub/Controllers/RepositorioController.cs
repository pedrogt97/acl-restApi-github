using Microsoft.AspNetCore.Mvc;
using Octokit;
using Models.GitHub;

//namespace aclGitHub.Controllers;

//40ee697597f0744af0f783637fd3f450cd7f09ae
[ApiController]
[Route("[controller]")]
public class RepositorioController : ControllerBase
{
    // private string clientId = "59bce3ccbae90be33fed";
    // private string secretPass = "40ee697597f0744af0f783637fd3f450cd7f09ae";
    readonly GitHubClient client =
        new GitHubClient(new ProductHeaderValue("api-gh-terra"));
    
    [HttpPost]
    public async Task<ActionResult<Repository>> PostRepositorio(NewRepository repo)
    {
        //var gitHubClient = new GitHubClient(new ProductHeaderValue("api-gh-terra"));
        try
        {
            client.Credentials = new Credentials(Request.Headers.Authorization);
        }
        catch(System.ArgumentNullException ex){
            return Unauthorized();
        }
        catch(System.NullReferenceException ex){
            return Unauthorized();
        }


        try
        {
            var repositorio = await client.Repository.Create(repo);
            return Created(repositorio.Id.ToString(), repositorio);
        }
        catch(Octokit.ApiException ex){
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]/{id:long}")]
    public async Task<ActionResult<List<Branch>>> branches(long id)
    {
        
        // var gitHubClient = new GitHubClient(new ProductHeaderValue("api-gh-terra"));
        
        // gitHubClient.
        try
        {
            client.Credentials = new Credentials(Request.Headers.Authorization);
        }
        catch(System.ArgumentNullException ex){
            return Unauthorized();
        }
        catch(System.NullReferenceException ex){
            return Unauthorized();
        }

        try
        {
            var branchesDeUmRepo = await client.Repository.Branch.GetAll(id);

            return Ok(branchesDeUmRepo);
        }
        catch(Octokit.ApiException ex){
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]/{id:long}")]
    public async Task<ActionResult<List<RepositoryHook>>> webhooks(long id)
    {
        
        //        var gitHubClient = new GitHubClient(new ProductHeaderValue("api-gh-terra"));
        try
        {
            client.Credentials = new Credentials(Request.Headers.Authorization);
        }
        catch(System.ArgumentNullException ex){
            return Unauthorized();
        }
        catch(System.NullReferenceException ex){
            return Unauthorized();
        }

        // Dictionary<string,string> config = new Dictionary<string, string>();
        // config.Add("url", "	https://webhook.site/e78cc8f0-1e02-4d51-a429-e5797eeb0079");
        // config.Add("token", gitHubClient.Credentials.GetToken());

        // var addHookEmRepo = await gitHubClient.Repository.Hooks.Create(id, new NewRepositoryHook("web", config) );
        try
        {
            var hooksDeUmRepo = await client.Repository.Hooks.GetAll(id);
            return Ok(hooksDeUmRepo);

        }
        catch(Octokit.ApiException ex)
        {
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }

    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<RepositoryHook>> webhooks(NovoHookRepositorio hook){

        //var gitHubClient = new GitHubClient(new ProductHeaderValue("api-gh-terra"));
        try
        {
            client.Credentials = new Credentials(Request.Headers.Authorization);
        }
        catch(System.ArgumentNullException ex){
            return Unauthorized();
        }
        catch(System.NullReferenceException ex){
            return Unauthorized();
        }
        
        Dictionary<string,string> configs = new Dictionary<string, string>();

        //configs.Add("url", hook.url);
        configs.Add("url", hook.Url);
        configs.Add("token", client.Credentials.GetToken());
        try
        {
            // var novoHook = await client.Repository.Hooks.Create(hook.id,  new NewRepositoryHook("web", configs) );
            var novoHook = await client.Repository.Hooks.Create(hook.IdRepositorio, new NewRepositoryHook("web",configs) );
            return Created(novoHook.Id.ToString(), novoHook);    
        }
        catch(Octokit.ApiException ex)
        {
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<ActionResult<RepositoryHook>> webhooks(AtualizaHook infosHook){
        try{
            client.Credentials = new Credentials(Request.Headers.Authorization);
        }
        catch(System.ArgumentNullException ex){
            return Unauthorized();
        }
        catch(System.NullReferenceException ex){
            return Unauthorized();
        }
        
        try{
            var atualizaHook = await client.Repository.Hooks.Edit(infosHook.RepoId, infosHook.HookId, infosHook.InfosHook);
            return Ok(atualizaHook);
        }
        catch(Octokit.ApiException ex)
        {
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }
    }
}


