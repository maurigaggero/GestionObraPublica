﻿using GOP.Shared.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOP.Shared.DTOs.Entity
{
    public class ContratoItemControlParamDTO : DTOBase
    {
        [Required(ErrorMessage = "El Control del item del Contrato es obligatorio.")]
        public int ContratoItemControllId { get; set; }
        public ContratoItemControlDTO ContratoItemControl { get; set; }

        [Required(ErrorMessage = "La plantilla de item de referencia es obligatorio.")]
        public int ItemControlParamId { get; set; }
        public ItemControlParamDTO ItemControlParam { get; set; }

        [Required(ErrorMessage = "El Tipo de Parámetro es obligatorio.")]
        public TipoParam TipoParam { get; set; }

        [Required(ErrorMessage = "El Parámetro es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Longitud Maxima del parámetro es de {1} caracteres")]
        public string Parametro { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "Longitud Maxima de la descripción es de {1} caracteres")]
        public string Descripción { get; set; }

        [Required(ErrorMessage = "La unidad con que se mide el parámetro es obligatorio.")]
        public int UnidadId { get; set; }
        public UnidadDTO Unidad { get; set; }

        [Required(ErrorMessage = "Este Valor es un dato fijo para el cálculo del certificado .")]
        public decimal ValorDato { get; set; } = 0;

        [Required(ErrorMessage = "Valor Mínimo del Parámetro para ser aprobado.")]
        public decimal ValorMinimo { get; set; }

        [Required(ErrorMessage = "Valor Máximo del Parámetro para ser aprobado.")]
        public decimal ValorMaximo { get; set; }
    }
}
