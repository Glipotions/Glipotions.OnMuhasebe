using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;

namespace Glipotions.OnMuhasebe.Blazor.Helpers;

public static class ExtensionFunctions
{
    public static string CreateValidationErrorMessage(
        this IList<ValidationFailure> errors, IStringLocalizer localizer)
    {
        var builder = new StringBuilder();
        builder.Append(localizer["ValidationErrorMessageBoxTitle"] + "\r\n");

        for (int i = 0; i < errors.Count; i++)
        {
            builder.Append($"- {errors[i]}");
            if (i + 1 < errors.Count)
                builder.Append("\r\n");
        }

        return builder.ToString();
    }
}