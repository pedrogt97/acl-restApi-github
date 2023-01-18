# acl-restApi-github

Ao rodar o projeto, será redirecionado para logar no GitHub. Após isso, o callback irá gerar um JSON na tela com o acess_token que pode ser usado para testar.

Vá para https://localhost:{porta}/swagger e clique em Authorize caso queira testar autenticado.

![image](https://user-images.githubusercontent.com/68344109/213170376-3d0c6de6-a8ce-4d02-8467-3868221b32b7.png)

Não consegui arrumar o swagger para que mostre os schemas corretos, por exemplo, no Post para criar um novo repositório apenas o parâmetro "name" é obrigatório, mas no schema aparece como nullable.
