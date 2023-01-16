
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using aclGitHub.Controllers;

namespace ControllerTests{
using Octokit;
    public class RepositorioControllerTest{

        [Fact]
        public async void PostRepositorio_retornaMensagem_tokenInvalido(){
            // Arrange
            var repo = new NewRepository("Dom1ngo");
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "123456";

            var controller = new RepositorioController(){
                ControllerContext = new ControllerContext(){
                    HttpContext = httpContext,
                }
            };

            var actionResult = await controller.PostRepositorio(repo);
            

            // Assert
            Assert.IsType<ObjectResult>(actionResult.Result);

        }


        [Fact]
        public async void PostRepositorio_retornaUnauthorized_semHeader(){

            var repo = new NewRepository("Dom1ngo");
            
            var controller = new RepositorioController();

            var actionResult = await controller.PostRepositorio(repo);

            Assert.IsType<UnauthorizedResult>(actionResult.Result);

        }       

        [Fact]
        public async void getRetorna_retornaUnauthorized_semHeader(){
            long id = 1;

            var controller = new RepositorioController();

            var actionResult = await controller.branches(id);

            Assert.IsType<UnauthorizedResult>(actionResult.Result);
            
        }
    }
}