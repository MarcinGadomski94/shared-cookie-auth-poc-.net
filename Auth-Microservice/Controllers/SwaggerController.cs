using Microsoft.AspNetCore.Mvc;

namespace Auth_Microservice;

public class SwaggerController
{
    [HttpGet]
    [Route("/swagger/v1/swagger.json")]
    public string SwaggerDocument()
    {
        var swaggerJsonContents = File.ReadAllText(Environment.CurrentDirectory + "/swagger.yml");
        return swaggerJsonContents;
    }
}