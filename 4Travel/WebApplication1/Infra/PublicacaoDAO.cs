using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Infra
{
    public class PublicacaoDAO
    {
        TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;

        public byte[] GetImageById(int id)
        {
            using(var contexto = new BlogContext())
            {
                var img = (from p in contexto.Publicacoes
                          where p.id == id
                          select p.imagem).FirstOrDefault();
                return img;
            }
        }

        public IEnumerable<PublicacaoDestino> GetListaPublicacao(string tipo)
        {
            var listaPublicacao = Enumerable.Empty<PublicacaoDestino>();

            if (tipo == "NACIONAL")
            {
                using (var contexto = new BlogContext())
                {
                    listaPublicacao = (from p in contexto.Publicacoes
                                       where p.idPais == 1
                                       select p).ToArray();
                }

                foreach (var p in listaPublicacao)
                {
                    p.nomeCidade = textInfo.ToLower(p.nomeCidade);
                    p.nomeEstado = textInfo.ToLower(p.nomeEstado);

                    p.nomeCidade = textInfo.ToTitleCase(p.nomeCidade);
                    p.nomeEstado = textInfo.ToTitleCase(p.nomeEstado);
                }

            }
            else
            {
                using (var contexto = new BlogContext())
                {
                    listaPublicacao = (from p in contexto.Publicacoes
                                       where p.idPais != 1
                                       select p).ToArray();

                    foreach (var pub in listaPublicacao)
                    {
                        pub.Pais = (from p in contexto.Paises
                                    where p.id == pub.idPais
                                    select p).FirstOrDefault();
                    }
                }

                foreach (var p in listaPublicacao)
                {
                    p.nomeCidade = textInfo.ToLower(p.nomeCidade);
                    p.nomeEstado = textInfo.ToLower(p.nomeEstado);
                    p.Pais.nome = textInfo.ToLower(p.Pais.nome);

                    p.nomeCidade = textInfo.ToTitleCase(p.nomeCidade);
                    p.nomeEstado = textInfo.ToTitleCase(p.nomeEstado);
                    p.Pais.nome = textInfo.ToTitleCase(p.Pais.nome);
                }
            }

            return listaPublicacao;
        }
        public IEnumerable<Pais> GetListaPaises(int id)
        {
           
                var listaPaises = Enumerable.Empty<Pais>();
                //INTERNACIONAL
                if (id == 1)
                {
                    using (var contexto = new BlogContext())
                    {
                        listaPaises = contexto.Paises.Where(p => p.id > 1).ToList();
                    }
                }
                else
                {
                //NACIONAL
                using (var contexto = new BlogContext())
                    {
                        listaPaises = contexto.Paises.Where(p => p.id == 1).ToList();
                    }
                }
                foreach (Pais pais in listaPaises)
                {
                    pais.nome = pais.nome.ToLower();
                    pais.nome = textInfo.ToTitleCase(pais.nome);
                }

                return listaPaises;
         }

         public void AdicionaPublicacao(PublicacaoDestino nova)
        {
            using (var contexto = new BlogContext())
            {
                var estado = (from e in contexto.Estados
                              where e.nome == nova.nomeEstado && e.idPais == nova.idPais
                              select e).FirstOrDefault();

                if (estado == null)
                {
                    estado = new Estado(nova.nomeEstado.ToUpper(), nova.idPais);
                    contexto.Estados.Add(estado);
                    contexto.SaveChanges();
                }

                var cidade = (from c in contexto.Cidades
                              where c.nome == nova.nomeCidade && c.idEstado == estado.id && c.idPais == nova.idPais
                              select c).FirstOrDefault();

                if (cidade == null)
                {
                    cidade = new Cidade(nova.nomeCidade.ToUpper(), estado.id, nova.idPais);
                    contexto.Cidades.Add(cidade);
                    contexto.SaveChanges();
                }

                nova.nomeEstado = nova.nomeEstado.ToUpper();
                nova.nomeCidade = nova.nomeCidade.ToUpper();

                contexto.Publicacoes.Add(nova);
                contexto.SaveChanges();
            }
        }

    }
}