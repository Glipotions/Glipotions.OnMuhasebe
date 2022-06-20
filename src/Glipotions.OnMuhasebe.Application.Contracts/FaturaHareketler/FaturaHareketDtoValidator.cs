using System;
using Glipotions.OnMuhasebe.Consts;
using Glipotions.OnMuhasebe.Faturalar;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Glipotions.OnMuhasebe.FaturaHareketler;

public class FaturaHareketDtoValidator : AbstractValidator<FaturaHareketDto>
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
    /// When ile koşula bağlı Validator Eklenir. Örnek: FaturaHareketTuru.Stok ise Stok alanı boş olamaz.
    /// </summary>
    /// <param name="localizer"></param>
    public FaturaHareketDtoValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Id)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Id"]]);

        RuleFor(x => x.HareketTuru)
            .IsInEnum()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["RowType"]])

            .NotEmpty()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["RowType"]]);

        RuleFor(x => x.StokId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Stok)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Stock"]]);

        RuleFor(x => x.HizmetId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Hizmet)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Service"]]);

        RuleFor(x => x.MasrafId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Masraf)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Expense"]]);

        RuleFor(x => x.DepoId)
            .NotEmpty()
            .When(x => x.HareketTuru == FaturaHareketTuru.Stok)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Warehouse"]]);

        RuleFor(x => x.Miktar)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Quantity"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["Quantity"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BirimFiyat)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["UnitPrice"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["UnitPrice"], localizer["ToZero"], localizer["ThanZero"]]);

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

        RuleFor(x => x.KdvOrani)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["ValueAddedTaxRate"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["ValueAddedTaxRate"], localizer["ToZero"], localizer["ThanZero"]]);

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

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);
    }
}