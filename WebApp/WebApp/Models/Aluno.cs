using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Data.Entity;

namespace WebApp.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobreNome { get; set; }
        public string telefone { get; set; }
        public int ra { get; set; }

        //public List<Alunos> listaAlunos()
        //{
        //    //Alunos aluno = new Alunos();

        //    //aluno.id = 1;
        //    //aluno.nome = "Arlson";
        //    //aluno.sobreNome = "Junior";
        //    //aluno.telefone = "9285842222";
        //    //aluno.ra = 1;

        //    //Alunos aluno1 = new Alunos();

        //    //aluno1.id = 2;
        //    //aluno1.nome = "Henrique";
        //    //aluno1.sobreNome = "Amorin";
        //    //aluno1.telefone = "0000000000009";
        //    //aluno1.ra = 2;

        //    //List<Alunos> listaAlunos = new List<Alunos>();

        //    //listaAlunos.Add(aluno);
        //    //listaAlunos.Add(aluno1);

        //    var caminho = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
        //    var json = File.ReadAllText(caminho);
        //    var listaAlunos = JsonConvert.DeserializeObject<List<Alunos>>(json);

        //    return listaAlunos;
        //}

        public List<Aluno> ListarAlunos()
        {
            var caminho = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = File.ReadAllText(caminho);
            var listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return listaAlunos;
        }

        public bool ReescreverArquivo( List<Aluno> listaAlunos)
        {
            var caminho = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminho, json);

            return true;
        }

        public Aluno Inserir( Aluno Aluno)
        {
            var listaAlunos = this.ListarAlunos();

            var maxId = listaAlunos.Max(x => x.id);
            Aluno.id = maxId + 1;
            listaAlunos.Add(Aluno);

            ReescreverArquivo(listaAlunos);
            return Aluno;
        }

        public Aluno Atualizar(int id, Aluno Aluno)
        {
            var listaAlunos = this.ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(x => x.id == id );

            if ( itemIndex >= 0 )
            {
                Aluno.id = id;
                listaAlunos[itemIndex] = Aluno;
            }
            else
            {
                return null;
            }

            ReescreverArquivo(listaAlunos);
            return Aluno;
        }

        public bool Deletar( int id)
        {
            var listaAlunos = this.ListarAlunos();

            var itemIndex = listaAlunos.FindIndex(p => p.id == id );

            if (itemIndex >= 0 )
            {
                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            ReescreverArquivo(listaAlunos);
            return true;
        }
    }
}