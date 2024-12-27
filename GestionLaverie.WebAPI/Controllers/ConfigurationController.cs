using Microsoft.AspNetCore.Mvc;
using gestionLaverie.WebAPI.Business;

[Route("api/[controller]")]
[ApiController]
public class ConfigurationController : ControllerBase
{
    private readonly IConfigurationService _configurationService;

    public ConfigurationController(IConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    [HttpGet]
    public IActionResult getConfig()
    {
        var configuration = _configurationService.getConfig();
        return Ok(configuration);
    }
}
