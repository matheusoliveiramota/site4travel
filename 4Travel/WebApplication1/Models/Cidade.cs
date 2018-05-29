using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{

    [Table("Cidades")]
    public class Cidade
    {
        [Key]
        public int id { get; set; }

        public string nome { get; set; }

        [ForeignKey("Estado")]
        public int idEstado { get; set; }
        public Estado Estado { get; set; }

        [ForeignKey("Pais")]
        public int idPais { get; set; }
        public Pais Pais { get; set; }

        public Cidade(string nome, int idEstado, int idPais)
        {
            this.nome = nome;
            this.idEstado = idEstado;
            this.idPais = idPais;
        }
        public Cidade() { }
    }
}