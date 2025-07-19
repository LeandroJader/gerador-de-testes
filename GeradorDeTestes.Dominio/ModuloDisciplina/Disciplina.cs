using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloMateria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorDeTestes.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public string Nome { get; set;}
        //public Materia Materia { get; set; }
       
        public Disciplina(string nome) : this ()
        {
            Nome = nome;
            //Materia = materia;
        }
        public Disciplina()
        {
            
        }
        public override void AtualizarRegistro(Disciplina registroEditado)
        {
            Nome = registroEditado.Nome;
            //Materia = registroEditado.Materia;
        }
    }
}
