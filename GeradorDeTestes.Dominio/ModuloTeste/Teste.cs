using GeradorDeTestes.Dominio.Compartilhado;
using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Dominio.ModuloMateria;

namespace GeradorDeTestes.Dominio.ModuloTeste;

public class Teste : EntidadeBase<Teste>
{
    public string Titulo { get; set; }
    public Disciplina Disciplina { get; set; }
    public Materia? Materia { get; set; }
    public Serie Serie { get; set; }
    public int QuantidadeQuestoes { get; set; }
    public TipoTeste TipoTeste {  get; set; }

    public override void AtualizarRegistro(Teste registroEditado)
    {
        Titulo = registroEditado.Titulo;
        Disciplina = registroEditado.Disciplina;
    }
}
