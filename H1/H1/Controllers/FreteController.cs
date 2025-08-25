using Microsoft.AspNetCore.Mvc;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreteController : ControllerBase
    {
        public class Produto
        {
            public string Nome { get; set; }
            public float Peso { get; set; }
            public float Altura { get; set; }
            public float Largura { get; set; }
            public float Comprimento { get; set; }
            public string UF { get; set; }
        }

        [HttpPost("calcular frete")]
        public IActionResult CalcularFrete([FromBody] Produto produto)
        {
            if (produto.Altura <= 0 || produto.Largura <= 0 || produto.Comprimento <= 0)
                return BadRequest("Erro, os valores não podem ser zero");

            double volume = produto.Altura * produto.Largura * produto.Comprimento;

            double txPCm3 = 0.01;

            double txestado = produto.UF switch
            {
                "SP" => 50.0,
                "RJ" => 60.0,
                "MG" => 55.0,
                _ => 70.0
            };
            double valorf = (volume * txPCm3) + txestado;

            return Ok(new
            {
                produto.Nome,
                Volume = volume,
                UF = produto.UF,
                ValorFrete = valorf
            });
        }
    }
}
