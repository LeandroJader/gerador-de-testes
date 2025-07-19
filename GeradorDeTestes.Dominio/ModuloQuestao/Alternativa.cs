using GeradorDeTestes.Dominio.Compartilhado;

namespace GeradorDeTestes.Dominio.ModuloQuestao;

public class Alternativa : EntidadeBase<Alternativa>
{
    public string Resposta { get; set; }
    public bool Correta { get; set; }
    public Questao Questao { get; set; }

    public Alternativa() { }

    public Alternativa(string resposta, bool correta) : this()
    {
        Resposta = resposta;
        Correta = correta;
    }


    public override void AtualizarRegistro(Alternativa registroEditado)
    {
        throw new NotImplementedException();
    }
}
