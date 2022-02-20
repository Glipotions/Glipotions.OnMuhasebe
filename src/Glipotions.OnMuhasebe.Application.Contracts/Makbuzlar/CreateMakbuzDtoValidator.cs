using System;
using Glipotions.OnMuhasebe.Consts;
using Glipotions.OnMuhasebe.Localization;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Glipotions.OnMuhasebe.Makbuzlar;

public class CreateMakbuzDtoValidator : AbstractValidator<CreateMakbuzDto>
{
    public CreateMakbuzDtoValidator(IStringLocalizer<OnMuhasebeResource> localizer)
    {
        RuleFor(x => x.MakbuzTuru)
            .IsInEnum()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["ReceiptType"]])

            .NotEmpty()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["ReceiptType"]]);

        RuleFor(x => x.MakbuzNo)
            .NotEmpty()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["ReceiptNo"]])

            .MaximumLength(MakbuzConsts.MaxMakbuzNoLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["ReceiptNo"], MakbuzConsts.MaxMakbuzNoLength]);

        RuleFor(x => x.Tarih)
            .NotEmpty()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Date"]]);

        RuleFor(x => x.KasaId)
            .NotEmpty()
            .When(x => x.MakbuzTuru == MakbuzTuru.KasaIslem)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Cash"]]);

        RuleFor(x => x.KasaId)
            .Empty()
            .When(x => x.MakbuzTuru != MakbuzTuru.KasaIslem)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.IsNull,
             localizer["Cash"]]);

        RuleFor(x => x.CariId)
            .NotEmpty()
            .When(x => x.MakbuzTuru == MakbuzTuru.Tahsilat || x.MakbuzTuru == MakbuzTuru.Odeme)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Customer"]]);

        RuleFor(x => x.CariId)
            .Empty()
            .When(x => x.MakbuzTuru != MakbuzTuru.Tahsilat && x.MakbuzTuru != MakbuzTuru.Odeme)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.IsNull,
             localizer["Customer"]]);

        RuleFor(x => x.BankaHesapId)
            .NotEmpty()
            .When(x => x.MakbuzTuru == MakbuzTuru.BankaIslem)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["BankAccount"]]);

        RuleFor(x => x.BankaHesapId)
            .Empty()
            .When(x => x.MakbuzTuru != MakbuzTuru.BankaIslem)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.IsNull,
             localizer["BankAccount"]]);

        RuleFor(x => x.HareketSayisi)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["NumberOfTransactions"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["NumberOfTransactions"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.CekToplam)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["CheckTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["CheckTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.SenetToplam)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["BillOfExchangeTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["BillOfExchangeTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.PosToplam)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["PosTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["PosTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.NakitToplam)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["CashTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["CashTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.BankaToplam)
            .NotNull()
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["BankTotal"]])

            .GreaterThanOrEqualTo(0)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.GreaterThanOrEqual,
             localizer["BankTotal"], localizer["ToZero"], localizer["ThanZero"]]);

        RuleFor(x => x.SubeId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Branch"]]);

        RuleFor(x => x.DonemId)
            .Must(x => x.HasValue && x.Value != Guid.Empty)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.Required,
             localizer["Period"]]);

        RuleFor(x => x.Aciklama)
            .MaximumLength(EntityConsts.MaxAciklamaLength)
            .WithMessage(localizer[OnMuhasebeDomainErrorCodes.MaxLenght,
             localizer["Description"], EntityConsts.MaxAciklamaLength]);

        RuleForEach(x => x.MakbuzHareketler)
            .SetValidator(y => new MakbuzHareketDtoValidator(localizer));
    }
}