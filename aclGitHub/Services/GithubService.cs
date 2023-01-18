using Octokit;
using Models.GitHub;
using Octokit.Internal;

namespace aclGitHub.Services
{
    public class GithubService : IGithubService
    {

        public async Task<string> CreateRepository(string token, NewRepository repo)
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("api-gh-terra"));
     
            client.Credentials = new Credentials(token);
            var repositorio = await client.Repository.Create(repo);
            return repositorio.Id.ToString();  
        }

        public async Task<List<Branch>> listaBranches(string token, long id)
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("api-gh-terra"));
     
            client.Credentials = new Credentials(token);

            var branchesDeUmRepo = await client.Repository.Branch.GetAll(id);

            return branchesDeUmRepo.ToList();
      
        }

        public async Task<List<RepositoryHook>> listaHooks(string token, long id)
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("api-gh-terra"));
     
            client.Credentials = new Credentials(token);

            var hooks = await client.Repository.Hooks.GetAll(id);

            return hooks.ToList();
      
        }

        public async Task<string> criaHook(string token, NovoHookRepositorio hook)
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("api-gh-terra"));
            Dictionary<string,string> configs = new Dictionary<string, string>();
            
            configs.Add("url", hook.Url);
            configs.Add("token", token);
        
            var novoHook = await client.Repository.Hooks.Create(hook.IdRepositorio, new NewRepositoryHook("web",configs) );
            return novoHook.Id.ToString();    
        }

        public async Task<RepositoryHook> atualizaHook(string token, AtualizaHook infosHook)
        {
              GitHubClient client = new GitHubClient(new ProductHeaderValue("api-gh-terra"));

              var atualizaHook = await client.Repository.Hooks.Edit(infosHook.RepoId, infosHook.HookId, infosHook.InfosHook);

              return atualizaHook;

        }
    }
}
