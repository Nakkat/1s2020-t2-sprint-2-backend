using System;
using System.Collections.Generic;

namespace Senai.InLock.WebApi.DataBaseFirst.Domains
{
    public partial class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? IdTipoUsuario { get; set; } //Aceita valores nulos

        public TiposUsuario IdTipoUsuarioNavigation { get; set; }
    }
}
