using System.ComponentModel.DataAnnotations;

namespace GeradorDeTestes.Dominio.ModuloMateria;

public enum Serie
{
    [Display(Name = "1º Ano - EF")]
    PrimeiroAnoEF = 1,

    [Display(Name = "2º Ano - EF")]
    SegundoAnoEF = 2,

    [Display(Name = "3º Ano - EF")]
    TerceiroAnoEF = 3,

    [Display(Name = "4º Ano - EF")]
    QuartoAnoEF = 4,

    [Display(Name = "5º Ano - EF")]
    QuintoAnoEF = 5,

    [Display(Name = "6º Ano - EF")]
    SextoAnoEF = 6,

    [Display(Name = "7º Ano - EF")]
    SetimoAnoEF = 7,

    [Display(Name = "8º Ano - EF")]
    OitavoAnoEF = 8,

    [Display(Name = "9º Ano - EF")]
    NonoAnoEF = 9,

    [Display(Name = "1º Ano - EM")]
    PrimeiroAnoEM = 10,

    [Display(Name = "2º Ano - EM")]
    SegundoAnoEM = 11,

    [Display(Name = "3º Ano - EM")]
    TerceiroAnoEM = 12
}
