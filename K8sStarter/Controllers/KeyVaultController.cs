using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using K8sStarter.Services;


namespace K8sStarter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeyVaultController : ControllerBase
    {

       private readonly ILogger<KeyVaultController> _logger;
       private readonly IKeyVaultService _kvService;

       private readonly string _subscriptionId = "";

       public KeyVaultController(ILogger<KeyVaultController> logger, IKeyVaultService kvService)
       {
           _logger = logger;
           _kvService = kvService;
       }

       [HttpGet]
       public IActionResult Get()
       {
           return Ok(_kvService.KVClient.GetDeletedSecrets().Count().ToString());
       }

       [HttpGet("GetRotatingSecret")]
       public async Task<IActionResult> GetRotatingSecret()
       {
           var response = new StringBuilder();
           
           response.AppendLine($"The csiRotatingSecret in the pod env is: {Environment.GetEnvironmentVariable("CSI_ROTATING_SECRET")}");
           var secretFromPod = await System.IO.File.ReadAllTextAsync("/mnt/secrets-store/csiRotatingSecret");
           response.AppendLine($"The csiRotatingSecret in the pod secret mount is: {secretFromPod}");
           var key = await _kvService.GetSecretAsync("csiRotatingSecret");
           response.AppendLine($"The csiRotatingSecret from key vault is: {key}");

           return Ok(response.ToString());
       }

       [HttpGet("RotateSecret")]
       public async Task<string> RotateSecret()
       {
           var key = await _kvService.SetSecretAsync(DateTime.Now.ToString());
           return key;
       }
    }
}   
