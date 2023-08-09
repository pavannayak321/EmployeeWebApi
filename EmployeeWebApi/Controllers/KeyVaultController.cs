using EmployeeWebApi.AzureKeyVaultUtility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyVaultController : ControllerBase
    {
        private readonly IKeyVaultManager _secretManager;
        public KeyVaultController(IKeyVaultManager secretManager)
        {
            _secretManager = secretManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string secretName)
        {
            try
            {
                if (string.IsNullOrEmpty(secretName))
                {
                    return BadRequest();
                }
                string secretValue  = await _secretManager.GetSecret(secretName);
                if (!string.IsNullOrEmpty(secretValue))
                {
                    return Ok(secretValue);
                }
                else
                {
                    return NotFound("Secret Noot Found");
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
