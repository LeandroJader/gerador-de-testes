using GeradorDeTestes.Dominio.Compartilhado;

namespace GeradorDeTestes.Dominio.ModuloMateria;

public class Materia : EntidadeBase<Materia>
{
    public string Nome { get; set; }
    public string Disciplina { get; set; }
    public Serie Serie { get; set; }

    public Materia() { }

    public Materia(
        string nome,
        string disciplina,
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
        string disciplina,
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
