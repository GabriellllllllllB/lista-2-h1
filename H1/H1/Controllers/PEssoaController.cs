using Microsoft.AspNetCore.Mvc;

namespace MinhaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        public class Pessoa
        {
            public string Nome { get; set; }
            public double Peso { get; set; }
            public double Altura { get; set; }
        }
        [HttpPost("imc")]
        public IActionResult CalcularIMC([FromBody] Pessoa pessoa)
        {
            if (pessoa.Altura <= 0)
                return BadRequest("Erro, altura não pode ser 0");

            double imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);

            return Ok(new
            {
                pessoa.Nome,
                IMC = imc
            });
        }
        [HttpPost("consulta tabela imc")]
        public IActionResult ConsultaTabelaIMC([FromBody] Pessoa pessoa)
        {
            if (pessoa.Altura <= 0)
                return BadRequest("Erro, altura não pode ser 0");

            double imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);

            string resultado = imc switch
            {
                < 18.5 => "Abaixo do peso",
                >= 18.5 and < 24.9 => "Peso normal",
                >= 25 and < 29.9 => "Sobrepeso",
                >= 30 and < 34.9 => "Obesidade grau 1",
                >= 35 and < 39.9 => "Obesidade grau 2",
                _ => "Obesidade grau 3"
            };

            return Ok(new
            {
                pessoa.Nome,
                IMC = imc,
                Resultado = resultado
            });
        }
    }
}
