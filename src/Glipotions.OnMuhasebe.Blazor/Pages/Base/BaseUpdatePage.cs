using System;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Localization;
using Glipotions.OnMuhasebe.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Base;

public abstract class BaseUpdatePage<TGetOutputDto, TGetListOutputDto, TGetListInput,
    TCreateInput, TUpdateInput> : AbpComponentBase
    where TGetOutputDto : class, IEntityDto
    where TGetListOutputDto : class, IEntityDto
    where TGetListInput : class, IEntityDto
    where TCreateInput : class, IEntityDto
    where TUpdateInput : class, IEntityDto  
{
    /// <ÖZET>
    /// Bu Page i yapmanın nedeni;
    ///     Yapı olarak BaseListPage e benziyor ancak çok daha az işi yaparak yükten kurtarıyor
    ///     

    public BaseUpdatePage()
    {
        LocalizationResource = typeof(OnMuhasebeResource);
    }

    #region Services

    protected ICrudAppService<TGetOutputDto, TGetListOutputDto, TGetListInput,
              TCreateInput, TUpdateInput> BaseCrudService { get; set; }

    #endregion

    #region Crud Functions
    /// <ÖZET>
    /// Geriye bir çıkış gönderir. BaseCrudService Kullanıldı.
    /// (3/5) 26. video 31.dk
    /// HandleErrorAsync= AbpComponentBase deki bir fonksiyon.
    ///     CRUD işlemlerinde oluşacak hataları yakalamak için yapılmış bir fonksiyondur.    
    /// 
    /// return default= hata olursa null döndermiş olur.
    /// Not: fonksiyonu direkt olarak kullanmak yerine try catch yapmamızın nedeni
    ///     hatayı yakalayıp kullanıcıya göstermektir.
    protected async Task<TGetOutputDto> GetAsync(Guid id)
    {
        try
        {
            return await BaseCrudService.GetAsync(id);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }

        return default;
    }
    /// <ÖZET>
    /// Yeni Kayıt Üretir.
    ///
    /// HandleErrorAsync= AbpComponentBase deki bir fonksiyon.
    ///     CRUD işlemlerinde oluşacak hataları yakalamak için yapılmış bir fonksiyondur. 
    ///     
    /// return default= hata olursa null döndermiş olur.
    /// 
    /// Not: fonksiyonu direkt olarak kullanmak yerine try catch yapmamızın nedeni
    ///     hatayı yakalayıp kullanıcıya göstermektir.
    protected async Task<TGetOutputDto> CreateAsync(TCreateInput input)
    {
        try
        {
            return await BaseCrudService.CreateAsync(input);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }

        return default;
    }
    /// <ÖZET>
    /// Kayıt'ı Güncelleme Kısmı. gönderilen id deki Kayıt'ı input'a göre günceller.
    ///
    /// HandleErrorAsync= AbpComponentBase deki bir fonksiyon.
    ///     CRUD işlemlerinde oluşacak hataları yakalamak için yapılmış bir fonksiyondur. 
    ///     
    /// return default= hata olursa null döndermiş olur.
    /// 
    /// Not: fonksiyonu direkt olarak kullanmak yerine try catch yapmamızın nedeni
    ///     hatayı yakalayıp kullanıcıya göstermektir.
    protected async Task<TGetOutputDto> UpdateAsync(Guid id, TUpdateInput input)
    {
        try
        {
            return await BaseCrudService.UpdateAsync(id, input);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }

        return default;
    }

    #endregion
}