namespace Models.GitHub;
using Octokit;

public class NovoHookRepositorio {

    public NovoHookRepositorio(long idRepositorio, string url){
        this.IdRepositorio = idRepositorio;
        this.Url = url;
    }
    public long IdRepositorio{get; set; }
    public string Url{get; set;}

}