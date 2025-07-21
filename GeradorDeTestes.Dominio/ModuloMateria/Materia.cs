using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloQuestao;

namespace GeradorDeTestes.Dominio.ModuloMateria;

public class Materia : EntidadeBase<Materia>
{
    public string Nome { get; set; }
    public Disciplina Disciplina { get; set; }
    public Serie Serie { get; set; }
    public List<Questao> Questoes { get; set; }

    public Materia() { }

    public Materia(
        string nome,
        Disciplina disciplina,
        Serie serie
    )
    {
        Nome = nome;
        Disciplina = disciplina;
        Serie = serie;
    }

    public Materia(
        Guid id,
        string nome,
        Disciplina disciplina,
        Serie serie
    ) : this(nome, disciplina, serie)
    {
        Id = id;
    }

    public override void AtualizarRegistro(Materia registroEditado)
    {
        Nome = registroEditado.Nome;
        Disciplina = registroEditado.Disciplina;
        Serie = registroEditado.Serie;
    }
}
