using Octokit;
using Models.GitHub;
namespace aclGitHub.Services
{
    public interface IGithubService
    {
        Task<string> CreateRepository(string token, NewRepository repo);
        Task<List<Branch>> listaBranches(string token, long id);
        Task<List<RepositoryHook>> listaHooks(string token, long id);
        Task<string> criaHook(string token, NovoHookRepositorio hook);
        Task<RepositoryHook> atualizaHook(string token, AtualizaHook infosHook);
    }
}