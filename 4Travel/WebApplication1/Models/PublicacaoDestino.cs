using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PublicacaoDestino
    {
        [Key]
        public int id { get; set; }

        public byte[] imagem { get; set; }

        [ForeignKey("Pais")]
        public int idPais { get; set; }

        [DisplayName("País")]
        public Pais Pais { get; set; }

        [Required(ErrorMessage = "Digite o Estado !")]
        [StringLength(50)]
        [DisplayName("Estado")]
        public string nomeEstado { get; set; }

        [Required(ErrorMessage = "Digite a Cidade !")]
        [StringLength(50)]
        [DisplayName("Cidade")]
        public string nomeCidade { get; set; }

        [Required(ErrorMessage = "Digite uma Descrição para o Destino !")]
        [DisplayName("Descrição")]
        public string descricao { get; set; }

        public DateTime dataPublicacao { get; set; }

        [Required(ErrorMessage = "Digite o Autor !")]
        [StringLength(50)]
        [DisplayName("Autor")]
        public string autor { get; set; }

        public PublicacaoDestino(int idPais, string nomeEstado, string nomeCidade, string descricao, string autor, byte[] imagem)
        {
            this.idPais = idPais;
            this.nomeEstado = nomeEstado;
            this.nomeCidade = nomeCidade;
            this.descricao = descricao;
            this.dataPublicacao = DateTime.Now;
            this.autor = autor;
            this.imagem = imagem;
        }
        public PublicacaoDestino()
        {

        }
    }
}

