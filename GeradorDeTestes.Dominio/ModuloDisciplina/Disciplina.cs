using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;

namespace GeradorDeTestes.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public string Nome { get; set;}
        public List<Materia> Materias { get; set; }
        public List<Teste> Testes { get; set; }

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
            Materias = registroEditado.Materias;
            Testes = registroEditado.Testes;
        }
    }
}
