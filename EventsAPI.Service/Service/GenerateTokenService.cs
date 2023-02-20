using EventsAPI.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventsAPI.Service.Service
{
    public class GenerateTokenService : IGenerateTokenService
    {
        public string GenerateTokenCityEvent(string nome, string permissao)
        {
            var cryptoKey = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Issuer = "APIClientes.com", // Informação sobre o emissor do token
                Audience = "APIEvents.com", // Informação sobre o receptor/consumidor do token
                Expires = DateTime.UtcNow.AddHours(4), //Tempo de expiração do token
                //Claims do usuario, informacoes sobre a Pessoa
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, nome), // Claim com nome da pessoa 
                    new Claim(ClaimTypes.Role, permissao), // Claim com a permissao da pessoa
                    //new Claim("teste", "1234") // Claim de teste personalizada
                }),
                SigningCredentials = new SigningCredentials( // Adiciona credencial
                    new SymmetricSecurityKey(cryptoKey), // Adiciona chave de criptografia do token
                    SecurityAlgorithms.HmacSha256Signature // Forma de criptografia do token
                    )
            };

            // Cria objeto para manipular o token -- gerenciador
            var tokenManager = new JwtSecurityTokenHandler();

            // Gerenciador cria o token criptografado
            var token = tokenManager.CreateToken(tokenDescription);

            // Retorna token em forma de string criptografada
            return tokenManager.WriteToken(token);
        }
    }
}
