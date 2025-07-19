using GeradorDeTestes.Dominio.Compartilhado;

namespace GeradorDeTestes.Dominio.ModuloQuestao;

public class Questao : EntidadeBase<Questao>
{
    public string Materia { get; set; } // Trocar para class Materia posteriormente
    public string Enunciado { get; set; }
    public List<Alternativa> Alternativas { get; set; } = new List<Alternativa>();

    public Questao() { }

    public Questao(string materia, string enunciado) : this()
    {
        Materia = materia;
        Enunciado = enunciado;
    }

    public void AdicionarAlternativa(Alternativa alternativa)
    {
        Alternativas.Add(alternativa);
    }

    public void RemoverAlternativa(Alternativa alternativa)
    {
        Alternativas.Remove(alternativa);
    }

    public override void AtualizarRegistro(Questao registroEditado)
    {
        Materia = registroEditado.Materia;
        Enunciado = registroEditado.Enunciado;
        Alternativas.Clear();
        Alternativas = registroEditado.Alternativas;
    }
}
