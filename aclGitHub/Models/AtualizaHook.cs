namespace Models.GitHub;
using Octokit;

public class AtualizaHook{
    public AtualizaHook(long repoId, int hookId, EditRepositoryHook infosHook){
        RepoId = repoId;
        HookId = hookId;
        InfosHook = infosHook;
    }

    public long RepoId {get;set;}
    public int HookId{get;set;}

    public EditRepositoryHook InfosHook {get;set;}

}
