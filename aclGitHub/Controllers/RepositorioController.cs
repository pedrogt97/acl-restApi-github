using Microsoft.AspNetCore.Mvc;
using Octokit;
using Models.GitHub;
using aclGitHub.Services;

[ApiController]
[Route("[controller]")]
public class RepositorioController : ControllerBase
{

    readonly GitHubClient client =
        new GitHubClient(new ProductHeaderValue("api-gh-terra"));

    private readonly IGithubService _githubService;

    public RepositorioController(IGithubService githubService)
    {
        _githubService = githubService;
    }    

    [HttpPost]
    public async Task<ActionResult<Repository>> PostRepositorio(NewRepository repo)
    {
        var token = "";
        try
        {
            token = Request.Headers.Authorization;
            if(token == null)
                return Unauthorized();
        }
        catch(ArgumentNullException ex){
            return Unauthorized();
        }
        catch(NullReferenceException ex){
            return Unauthorized();
        }

        try
        {
            var repositorio = await _githubService.CreateRepository(token, repo);
            return Ok(repositorio);
        }
        catch(Octokit.ApiException ex){
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }
    }

    [HttpGet]
    [Route("[action]/{id:long}")]
    public async Task<ActionResult<List<Branch>>> branches(long id)
    {
        var token = "";
        try
        {
            token = Request.Headers.Authorization;
            if(token == null)
                return Unauthorized();
        }
        catch(ArgumentNullException ex){
            return Unauthorized();
        }
        catch(NullReferenceException ex){
            return Unauthorized();
        }

        try
        {
            var branches = await _githubService.listaBranches(token, id);
            return Ok(branches);
        }
        catch(Octokit.ApiException ex){
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }

    }

    [HttpGet]
    [Route("[action]/{id:long}")]
    public async Task<ActionResult<List<RepositoryHook>>> webhooks(long id)
    {
        
        var token = "";
        try
        {
            token = Request.Headers.Authorization;
            if(token == null)
                return Unauthorized();
        }
        catch(ArgumentNullException ex){
            return Unauthorized();
        }
        catch(NullReferenceException ex){
            return Unauthorized();
        }


        try
        {
            var hooks = await _githubService.listaHooks(token, id);
            return Ok(hooks);
        }
        catch(Octokit.ApiException ex){
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }

    }

    [HttpPost]
    [Route("[action]")]
    public async Task<ActionResult<RepositoryHook>> webhooks(NovoHookRepositorio hook){

        var token = "";
        try
        {
            token = Request.Headers.Authorization;
            if(token == null)
                return Unauthorized();
        }
        catch(ArgumentNullException ex){
            return Unauthorized();
        }
        catch(NullReferenceException ex){
            return Unauthorized();
        }
        
        try
        {
            var novoHook = await _githubService.criaHook(token, hook);
            return Ok(novoHook);    
        }
        catch(Octokit.ApiException ex)
        {
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }
    }

    [HttpPut]
    [Route("[action]")]
    public async Task<ActionResult<RepositoryHook>> webhooks(AtualizaHook infosHook){
        var token = "";
        try
        {
            token = Request.Headers.Authorization;
            if(token == null)
                return Unauthorized();
        }
        catch(ArgumentNullException ex){
            return Unauthorized();
        }
        catch(NullReferenceException ex){
            return Unauthorized();
        }
        
        try{
            var atualizaHook = await _githubService.atualizaHook(token, infosHook);
            return Ok(atualizaHook);
        }
        catch(Octokit.ApiException ex)
        {
            return Problem(statusCode: ((int)ex.StatusCode), detail: ex.Message);
        }
    }
}




