using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Infra;
using WebApplication1.Models;

namespace WebApplication1.Views.Home
{
    public class HomeController : Controller
    {
        public ActionResult Show(int id)
        {
            PublicacaoDAO pub = new PublicacaoDAO();
            return File(pub.GetImageById(id), "image/jpg");
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DestinosInternacionais()
        {
            PublicacaoDAO pub = new PublicacaoDAO();
            return View(pub.GetListaPublicacao("INTERNACIONAL"));
        }

        public ActionResult DestinosNacionais()
        {
            PublicacaoDAO pub = new PublicacaoDAO();
            return View(pub.GetListaPublicacao("NACIONAL"));
        }

        public ActionResult IntercambioEscolha()
        {
            return View();
        }

        public ActionResult Intercambio()
        {
            return View();
        }

        public ActionResult IntercambioIdioma()
        {
            return View();
        }

        public ActionResult IntercambioTrabalho()
        {
            return View();
        }

        public ActionResult NovoDestino(int id)
        {
            if (id != 1 && id != 2)
            {
                return RedirectToAction("Index");
            }
            else
            {
                PublicacaoDAO pub = new PublicacaoDAO();
                ViewBag.Paises = pub.GetListaPaises(id);
                ViewBag.Id = id;
                return View();
            }
        }

        [HttpPost]
        public ActionResult AdicionaPublicacao(int idPais, string nomeEstado, string nomeCidade, string descricao, string autor, HttpPostedFileBase imagem)
        { 

            byte[] img = new byte[imagem.ContentLength];
            imagem.InputStream.Read(img,0, imagem.ContentLength);

            PublicacaoDestino nova = new PublicacaoDestino(idPais, nomeEstado, nomeCidade, descricao, autor,img);
            PublicacaoDAO pub = new PublicacaoDAO();
            pub.AdicionaPublicacao(nova);

            
            if (nova.idPais == 1)
            {
                return RedirectToAction("DestinosNacionais");
            }
            else
            {
                return RedirectToAction("DestinosInternacionais");
            }
        }
    }
}