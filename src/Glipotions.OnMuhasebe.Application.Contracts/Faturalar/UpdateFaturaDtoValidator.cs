using System;
using Glipotions.OnMuhasebe.Consts;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Glipotions.OnMuhasebe.Faturalar;

/// <ÖZET>
/// (2/5) Kurs
/// Validation işlemi FluentValidation ile yapılır, Volo içinde var
/// Bu yüzden AbstractValidator kullanılır.
/// </summary>
public class UpdateFaturaDtoValidator : AbstractValidator<UpdateFaturaDto>
{
    /// <ÖZET>
    /// (2/5) Kurs
    /// FluentValidator da RuleFor ile kural verilir.
    /// 
    /// hataları localizer ile vermemiz gerekir dil problemi olmaması için
    /// her bir property için kural varsa teker teker girilir.
    /// Örneğin Kod için Boş olmaması gerekir ve Maximum uzunluğu geçmemelidir hataları verildi.
    /// 
    /// Enumda olmayan için IsInEnum komutu kullanılır ve enumdaki kullanılmıyorsa hatayı verir.
    /// 
    /// Must kendi yapımızı oluşturmak için kullanılır, kendi kuralımızı koyduktan sonra validation yapılır.
    /// 
    /// GreaterThanOrEqualTo ile 0 dan büyük olması gerektiği belirtilir.
    /// 
    /// RuleForEach da Hareketler için Validator işlemi yapılır.
    /// SetValidator yaparak Hareketler için yapılan Validator kısmını çalıştırır.
    /// </summary>
    /// <param name="localizer"></param>
    public UpdateFaturaDtoValidator(IStringLocalizer<OnMuhasebeResource> localizer)
    {
        RuleFor(x => x.FaturaNo)
            .NotEmpty()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["InvoiceNumber"]])

            .MaximumLength(FaturaConsts.MaxFaturaNoLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["InvoiceNumber"], FaturaConsts.MaxFaturaNoLength]);

        RuleFor(x => x.Tarih)
            .NotEmpty()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Date"]]);

        RuleFor(x => x.BrutTutar)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["GrossAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["GrossAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.IndirimTutar)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["DiscountAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["DiscountAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.KdvHaricTutar)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["ExcludesValueAddedTaxAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["ExcludesValueAddedTaxAmount"], localizer["ToZero"],
             localizer["ThanZero"]]);

        RuleFor(x => x.KdvTutar)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["ValueAddedTaxAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["ValueAddedTaxAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.NetTutar)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["TotalAmount"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["TotalAmount"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.HareketSayisi)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["NumberOfTransactions"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["NumberOfTransactions"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.CariId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Customer"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);

        RuleForEach(x => x.FaturaHareketler)
            .SetValidator(y => new FaturaHareketDtoValidator(localizer));
    }
}