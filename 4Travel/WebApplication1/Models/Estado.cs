using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("Estados")]
    public class Estado
    {
        [Key]
        public int id { get; set; }
        public string nome { get; set; }

        [ForeignKey("Pais")]
        public int idPais { get; set; }
        public Pais Pais { get; set; }

        public Estado(string nome, int idPais)
        {
            this.nome = nome;
            this.idPais = idPais;
        }

        public Estado()
        {
        }
    }

}