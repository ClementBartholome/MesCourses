using System.ComponentModel;
using Microsoft.OpenApi.Attributes;

namespace MesCoursesApi.Enums;

public enum UnitEnum
{
    [Description("Pièces")]
    Piece = 1,
    [Description("Kg")]
    Kg = 2,
    [Description("L")]
    Litre = 3,
}