﻿using GOP.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Repositorio.Repos
{
    public interface IEstructuraTipoRepositorio : IRepositorio<EstructuraTipo>
    {
        Task<List<EstructuraTipo>> GetEstructuraTipos();
        Task<EstructuraTipo> GetEstructuraTipoById(int id);
    }
}
