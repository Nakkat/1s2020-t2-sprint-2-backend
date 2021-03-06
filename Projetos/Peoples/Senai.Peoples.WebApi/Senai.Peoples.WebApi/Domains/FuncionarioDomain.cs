﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Domains
{
    public class FuncionarioDomain
    {
            public int IdFuncionario { get; set; }

            public string Nome { get; set; }

            public string Sobrenome { get; set; }

            public string DataNascimento { get; set; }

            public int IdUsuario { get; set; }

            public UsuarioDomain Usuario { get; set; }
    }
}
