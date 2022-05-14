using System.Linq;
using AutoMapper;
using Glipotions.OnMuhasebe.BankaHesaplar;
using Glipotions.OnMuhasebe.Bankalar;
using Glipotions.OnMuhasebe.BankaSubeler;
using Glipotions.OnMuhasebe.Birimler;
using Glipotions.OnMuhasebe.Cariler;
using Glipotions.OnMuhasebe.Depolar;
using Glipotions.OnMuhasebe.Donemler;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Hizmetler;
using Glipotions.OnMuhasebe.Kasalar;
using Glipotions.OnMuhasebe.Makbuzlar;
using Glipotions.OnMuhasebe.Masraflar;
using Glipotions.OnMuhasebe.OzelKodlar;

namespace Glipotions.OnMuhasebe;

public class OnMuhasebeApplicationAutoMapperProfile : Profile
{
    /// <Özet>
    /// Map işlemleri yapılır.
    /// </summary>
    public OnMuhasebeApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Banka, SelectBankaDto>()
             .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
             .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<Banka, ListBankaDto>()
             .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
             .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<CreateBankaDto, Banka>();
        CreateMap<UpdateBankaDto, Banka>();

        // BankaSube
        CreateMap<BankaSube, SelectBankaSubeDto>()
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.Banka.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<BankaSube, ListBankaSubeDto>()
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.Banka.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<CreateBankaSubeDto, BankaSube>();
        CreateMap<UpdateBankaSubeDto, BankaSube>();

        // BankaHesap
        CreateMap<BankaHesap, SelectBankaHesapDto>()
            .ForMember(x => x.BankaId, y => y.MapFrom(z => z.BankaSube.Banka.Id))
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.BankaSube.Banka.Ad))
            .ForMember(x => x.BankaSubeAdi, y => y.MapFrom(z => z.BankaSube.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<BankaHesap, ListBankaHesapDto>()
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.BankaSube.Banka.Ad))
            .ForMember(x => x.BankaSubeAdi, y => y.MapFrom(z => z.BankaSube.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<CreateBankaHesapDto, BankaHesap>();
        CreateMap<UpdateBankaHesapDto, BankaHesap>();

        // Birim
        CreateMap<Birim, SelectBirimDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<Birim, ListBirimDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<CreateBirimDto, Birim>();
        CreateMap<UpdateBirimDto, Birim>();

        // Cari
        CreateMap<Cari, SelectCariDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<Cari, ListCariDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<CreateCariDto, Cari>();
        CreateMap<UpdateCariDto, Cari>();
        //CreateMap<SelectCariDto, CreateCariDto>();
        //CreateMap<SelectCariDto, UpdateCariDto>();

        // Depo
        CreateMap<Depo, SelectDepoDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        CreateMap<Depo, ListDepoDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.Giren, y => y.MapFrom(z => z.FaturaHareketler
                .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Alis).Sum(x => x.Miktar)))
            .ForMember(x => x.Cikan, y => y.MapFrom(z => z.FaturaHareketler
                .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Satis).Sum(x => x.Miktar)));
        CreateMap<CreateDepoDto, Depo>();
        CreateMap<UpdateDepoDto, Depo>();

        // Donem
        CreateMap<Donem, SelectDonemDto>();
        CreateMap<Donem, ListDonemDto>();
        CreateMap<CreateDonemDto, Donem>();
        CreateMap<UpdateDonemDto, Donem>();

        // Fatura


        // Hizmet
        CreateMap<Hizmet, SelectHizmetDto>()
            .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

        CreateMap<Hizmet, ListHizmetDto>()
            .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.Giren, y => y.MapFrom(z => z.FaturaHareketler
                .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Alis).Sum(x => x.Miktar)))
            .ForMember(x => x.Cikan, y => y.MapFrom(z => z.FaturaHareketler
                .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Satis).Sum(x => x.Miktar)));

        CreateMap<CreateHizmetDto, Hizmet>();
        CreateMap<UpdateHizmetDto, Hizmet>();
        CreateMap<SelectHizmetDto, CreateHizmetDto>();
        CreateMap<SelectHizmetDto, UpdateHizmetDto>();

        //Kasa
        CreateMap<Kasa, SelectKasaDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

        CreateMap<Kasa, ListKasaDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.Borc, y => y.MapFrom(z => z.MakbuzHareketler
                .Where(x => x.BelgeDurumu == BelgeDurumu.TahsilEdildi).Sum(x => x.Tutar)))
            .ForMember(x => x.Alacak, y => y.MapFrom(z => z.MakbuzHareketler
                .Where(x => x.BelgeDurumu == BelgeDurumu.Odendi).Sum(x => x.Tutar)));

        CreateMap<CreateKasaDto, Kasa>();
        CreateMap<UpdateKasaDto, Kasa>();
        CreateMap<SelectKasaDto, CreateKasaDto>();
        CreateMap<SelectKasaDto, UpdateKasaDto>();

        //Masraf
        CreateMap<Masraf, SelectMasrafDto>()
            .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

        CreateMap<Masraf, ListMasrafDto>()
            .ForMember(x => x.BirimAdi, y => y.MapFrom(z => z.Birim.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad))
            .ForMember(x => x.Giren, y => y.MapFrom(z => z.FaturaHareketler
                .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Alis).Sum(x => x.Miktar)))
            .ForMember(x => x.Cikan, y => y.MapFrom(z => z.FaturaHareketler
                .Where(x => x.Fatura.FaturaTuru == FaturaTuru.Satis).Sum(x => x.Miktar)));

        CreateMap<CreateMasrafDto, Masraf>();
        CreateMap<UpdateMasrafDto, Masraf>();
        CreateMap<SelectMasrafDto, CreateMasrafDto>();
        CreateMap<SelectMasrafDto, UpdateMasrafDto>();

        //OzelKod
        CreateMap<OzelKod, SelectOzelKodDto>();
        CreateMap<OzelKod, ListOzelKodDto>();
        CreateMap<CreateOzelKodDto, OzelKod>();
        CreateMap<UpdateOzelKodDto, OzelKod>();
        CreateMap<SelectOzelKodDto, CreateOzelKodDto>();
        CreateMap<SelectOzelKodDto, UpdateOzelKodDto>();

    }
}
