using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.WebApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GeradorDeTestes.WebApp.Services;

public static class PdfGenerator
{
    public static void GerarPdfSemGabarito(DetalhesTesteViewModel model)
    {
        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(14));

                page.Header()
                    .Text(model.Titulo)
                    .SemiBold().FontSize(24).FontColor(Colors.Black);

                page.Content()
                    .Column(col =>
                    {
                        col.Spacing(15);

                        col.Item().Text($"Disciplina: {model.Disciplina}");
                        col.Item().Text($"Matéria: {model.Materia}");
                        col.Item().Text($"Série: {model.Serie}");

                        int numeroQuestao = 1;
                        var letras = new[] { "a)", "b)", "c)", "d)", "e)", "f)", "g)", "h)" };

                        foreach (var questao in model.QuestoesSorteadas)
                        {
                            col.Item().Text(texto =>
                            {
                                texto.Span($"{numeroQuestao++}) {questao.Enunciado}")
                                    .Bold().FontSize(16);
                            });


                            foreach (var (alternativa, i) in questao.Alternativas.Select((alt, idx) => (alt, idx)))
                            {
                                col.Item().Text($"{letras[i]} {alternativa.Resposta}");
                            }

                            col.Item().PaddingBottom(10);
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                    });
            });
        });


        var nomeArquivo = $"{model.Titulo}.pdf";
        var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "testes");

        if (!Directory.Exists(caminhoPasta))
            Directory.CreateDirectory(caminhoPasta);

        var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

        pdf.GeneratePdf(caminhoCompleto);
    }

    public static void GerarPdfComGabarito(DetalhesTesteViewModel model)
    {
        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(14));

                page.Header()
                    .Text(model.Titulo + " (com Gabarito)")
                    .SemiBold().FontSize(24).FontColor(Colors.Black);

                page.Content()
                    .Column(col =>
                    {
                        col.Spacing(15);

                        col.Item().Text($"Disciplina: {model.Disciplina}");
                        col.Item().Text($"Matéria: {model.Materia}");
                        col.Item().Text($"Série: {model.Serie}");

                        int numeroQuestao = 1;
                        var letras = new[] { "a)", "b)", "c)", "d)", "e)", "f)", "g)", "h)" };

                        foreach (var questao in model.QuestoesSorteadas)
                        {
                            col.Item().Text(texto =>
                            {
                                texto.Span($"{numeroQuestao++}) {questao.Enunciado}")
                                    .Bold().FontSize(16);
                            });

                            foreach (var (alternativa, i) in questao.Alternativas.Select((alt, idx) => (alt, idx)))
                            {
                                col.Item().Text($"{letras[i]} {alternativa.Resposta}");
                            }

                            var respostaCorreta = questao.Alternativas.FirstOrDefault(a => a.Correta)?.Resposta;
                            if (!string.IsNullOrWhiteSpace(respostaCorreta))
                            {
                                col.Item().Text($"Gabarito: {respostaCorreta}")
                                    .Italic().FontColor(Colors.Green.Medium);
                            }

                            col.Item().PaddingBottom(10);
                        }
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                    });
            });
        });

        var nomeArquivo = $"{model.Titulo} - Gabarito.pdf";
        var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "testes");

        if (!Directory.Exists(caminhoPasta))
            Directory.CreateDirectory(caminhoPasta);

        var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

        pdf.GeneratePdf(caminhoCompleto);
    }

}
