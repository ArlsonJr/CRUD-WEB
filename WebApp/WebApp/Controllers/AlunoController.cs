using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        public IEnumerable<Aluno> Get()
        {
            Aluno aluno = new Aluno();

            return aluno.ListarAlunos(); //new string[] { "Arlson", "Junior" };
        }

        // GET: api/Aluno/5
        public Aluno Get(int id)
        {
            Aluno aluno = new Aluno();

            return aluno.ListarAlunos().Where( x => x.id == id ).FirstOrDefault();// "value";
        }

        // POST: api/Aluno
        public List<Aluno> Post(Aluno aluno)//POST inseri
        {
            Aluno _aluno = new Aluno();
            _aluno.Inserir(aluno);
            return _aluno.ListarAlunos();
            //List<Aluno> alunos = new List<Aluno>();

            //alunos.Add(aluno);

            //return alunos;
        }

        // PUT: api/Aluno/5
        public Aluno Put(int id, [FromBody]Aluno aluno)//Put Atualiza
        {
            Aluno _aluno = new Aluno();

            return _aluno.Atualizar(id, aluno);
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)//Deleta
        {
            Aluno _aluno = new Aluno();
            _aluno.Deletar(id);
        }
    }
}
