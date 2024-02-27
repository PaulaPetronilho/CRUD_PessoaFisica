using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CRUD_Pessoa.Models;

namespace CRUD_Pessoa.Controllers
{
    public class PessoaFisicaController : Controller
    {
        private PessoaDBContext db = new PessoaDBContext();

        // GET: PessoaFisica
        public ActionResult Index()
        {
            return View(db.Pessoas.ToList());
        }

        // GET: PessoaFisica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PessoaFisicaModel pessoaFisicaModel = db.Pessoas.Find(id);
            if (pessoaFisicaModel == null)
            {
                return HttpNotFound();
            }
            return View(pessoaFisicaModel);
        }

        // GET: PessoaFisica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PessoaFisica/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomeCompleto,DataNascimento,ValorRenda,CPF")] PessoaFisicaModel pessoaFisicaModel)
        {
            if (ModelState.IsValid)
            {
                if (ValidarCPF(pessoaFisicaModel.CPF))
                {
                    db.Pessoas.Add(pessoaFisicaModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CPF", "CPF inválido. Por favor, insira um CPF válido.");
                    return View();
                }
            }
            return View(pessoaFisicaModel);
        }

        // GET: PessoaFisica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PessoaFisicaModel pessoaFisicaModel = db.Pessoas.Find(id);
            if (pessoaFisicaModel == null)
            {
                return HttpNotFound();
            }
            return View(pessoaFisicaModel);
        }

        // POST: PessoaFisica/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomeCompleto,DataNascimento,ValorRenda,CPF")] PessoaFisicaModel pessoaFisicaModel)
        {
            if (ModelState.IsValid)
            {
                if (ValidarCPF(pessoaFisicaModel.CPF))
                {
                    db.Entry(pessoaFisicaModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CPF", "CPF inválido. Por favor, insira um CPF válido.");
                    return View();
                }                
            }
            return View(pessoaFisicaModel);
        }

        // GET: PessoaFisica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PessoaFisicaModel pessoaFisicaModel = db.Pessoas.Find(id);
            if (pessoaFisicaModel == null)
            {
                return HttpNotFound();
            }
            return View(pessoaFisicaModel);
        }

        // POST: PessoaFisica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PessoaFisicaModel pessoaFisicaModel = db.Pessoas.Find(id);
            db.Pessoas.Remove(pessoaFisicaModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Metodo para verificar se todos os dígitos do CPF são iguais
        public static bool ValidarCPF(string cpf)
        {
            // Remover caracteres não numéricos
            cpf = cpf.Replace(".", "").Replace("-", "");

            // Verificar se todos os dígitos do CPF são iguais
            if (cpf.Distinct().Count() == 1)
            {
                return false;
            }

            return true;
        }

    }
}
