﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lojinha.FIAP.Core.Models
{
    public class Imagens
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }

        public string ImagemUrl { get; set; }

    }
}
