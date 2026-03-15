using VW_Cursos.Applications.Autenticacao;
using VW_Cursos.Domains;
using VW_Cursos.DTOs.AutenticacaoDto;
using VW_Cursos.Interfaces;
using VW_Cursos.Exceptions;

namespace VHBurguer.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly GeradorTokenJwt _tokenJwt;

        public AutenticacaoService(GeradorTokenJwt tokenJwt)
        {
            _tokenJwt = tokenJwt;
        }

        // compara a hash SHA256 
        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hashDigitado = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            return hashDigitado.SequenceEqual(senhaHashBanco)
        }
    }
}