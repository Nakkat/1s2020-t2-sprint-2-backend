using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Filmes.WebApi.Domains
{
    /// <summary>
    /// Classe que representa a tabela Usuarios
    /// </summary>

    public class UsuarioDomain
    {
        public int IdUsuario { get; set; }

        // Define que o campo é obrigatório
        // e adciona o DataType de email
        [Required(ErrorMessage = "Informe o e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // Define que o campo é obrigatório
        [Required(ErrorMessage = "Informe a senha")]
        // Define que a senha precisa ter no mínimo 3 caracteres e no máximo 20
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo senha precisa ter no mínimo 3 caracteres")]
        public string Senha { get; set; }
        public string Permissao { get; set; }
    }
}

   
