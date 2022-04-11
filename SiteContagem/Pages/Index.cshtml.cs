using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteContagem.Logging;

namespace SiteContagem.Pages;

public class IndexModel : PageModel
{
    private static readonly Contador _CONTADOR = new();
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;

    public IndexModel(ILogger<IndexModel> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void OnGet()
    {
        int valorAtual;
        lock (_CONTADOR)
        {
            _CONTADOR.Incrementar(Convert.ToInt32(_configuration["Incremento"]));
            valorAtual = _CONTADOR.ValorAtual;
        }

        _logger.LogValorAtual(valorAtual);

        TempData["Contador"] = valorAtual;
        TempData["Local"] = _CONTADOR.Local;
        TempData["Kernel"] = _CONTADOR.Kernel;
        TempData["Framework"] = _CONTADOR.Framework;
        TempData["MensagemFixa"] = "Teste";
        TempData["MensagemVariavel"] = _configuration["MensagemVariavel"];
    }
}