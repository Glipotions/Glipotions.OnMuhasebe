using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glipotions.Blazor.Core.Helpers;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Localization;
using Glipotions.OnMuhasebe.Services;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Base;

/// <ÖZET>
/// Tüm List Pageler bu sayfadan kalıtım alır.
/// 
/// ICrudService'den Kopyaladık.
/// (3/5) 26. Video 10. dk
public abstract class BaseListPage<TGetOutputDto, TGetListOutputDto, TGetListInput,
    TCreateInput, TUpdateInput, TGetCodeInput> : AbpComponentBase
    where TGetOutputDto : class, IEntityDto<Guid>, new()
    where TGetListOutputDto : class, IEntityDto<Guid>
    where TGetListInput : class, IEntityDto, IDurum, new()
    where TCreateInput : class, IEntityDto, new()
    where TUpdateInput : class, IEntityDto
    where TGetCodeInput : class, IEntityDto, IDurum, new()
{
    public BaseListPage()
    {
        LocalizationResource = typeof(OnMuhasebeResource);
    }

    public string DefaultPolicy { get; set; }
    public string CreatePolicy { get; set; }
    public string UpdatePolicy { get; set; }
    public string DeletePolicy { get; set; }
    public string PrintPolicy { get; set; }
    public string ReservePolicy { get; set; }

    #region Services

    /// <ÖZET>
    /// BaseCrudService CRUD işlemlerini yapmak için çağırdık. 
    /// Aynı parametre isimleriyle gönderilir.
    protected ICrudAppService<TGetOutputDto, TGetListOutputDto, TGetListInput,
              TCreateInput, TUpdateInput, TGetCodeInput> BaseCrudService
    { get; set; }

    /// <ÖZET>
    /// Oluşturduğumuz BaseService'i kullanırız.
    /// Görevi: Oluşturmuş olduğumuz servislere ulaşıp onlarla alakalı işleri yapmak 
    ///     için kullandık
    ///     
    /// Parametre Olarak 
    ///     TDataGridItem olarak TGetListOutputDto gönderildi.
    ///     TDataSource olarak TGetOutputDto gönderildi.
    public BaseService<TGetListOutputDto, TGetOutputDto> BaseService { get; set; }

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
    /// Geriye List Döndürür.
    ///
    /// HandleErrorAsync= AbpComponentBase deki bir fonksiyon.
    ///     CRUD işlemlerinde oluşacak hataları yakalamak için yapılmış bir fonksiyondur. 
    ///     
    /// return default= hata olursa null döndermiş olur.
    /// 
    /// Not: fonksiyonu direkt olarak kullanmak yerine try catch yapmamızın nedeni
    ///     hatayı yakalayıp kullanıcıya göstermektir.
    protected async Task<PagedResultDto<TGetListOutputDto>> GetListAsync(
        TGetListInput input)
    {
        try
        {
            return await BaseCrudService.GetListAsync(input);
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
    /// <ÖZET>
    /// Kayıt'ı Silme İşlemi, Id ye göre Kayıtın IsDeleted'ını True yapar Hard Delete Yapmaz!.
    ///
    /// HandleErrorAsync= AbpComponentBase deki bir fonksiyon.
    ///     CRUD işlemlerinde oluşacak hataları yakalamak için yapılmış bir fonksiyondur. 
    /// 
    /// Not: fonksiyonu direkt olarak kullanmak yerine try catch yapmamızın nedeni
    ///     hatayı yakalayıp kullanıcıya göstermektir.
    protected async Task DeleteAsync(Guid id)
    {
        try
        {
            await BaseCrudService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }
    /// <ÖZET>
    /// Geriye Kod Döndürür.
    ///
    /// HandleErrorAsync= AbpComponentBase deki bir fonksiyon.
    ///     CRUD işlemlerinde oluşacak hataları yakalamak için yapılmış bir fonksiyondur. 
    ///     
    /// return default= hata olursa null döndermiş olur.
    /// 
    /// Not: fonksiyonu direkt olarak kullanmak yerine try catch yapmamızın nedeni
    ///     hatayı yakalayıp kullanıcıya göstermektir.
    protected async Task<string> GetCodeAsync(TGetCodeInput input)
    {
        try
        {
            return await BaseCrudService.GetCodeAsync(input);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }

        return default;
    }

    #endregion

    /// <ÖZET>
    /// (3/5) 31. Video 13.dk
    /// BaseService.HasChanged = StateHasChanged bu atama ile 
    ///     componentlerin CommonServiceden HasChanged kullananların değiştirilmesini sağlar.
    /// 
    /// ListPage çalıştırıldığı anda Burası çalışır ilk çalışan yerdir.
    protected override async Task OnParametersSetAsync()
    {
        //if (DefaultPolicy != null) BaseService.IsGrantedDefault =
        //        await AuthorizationService.IsGrantedAsync(DefaultPolicy);

        //if (CreatePolicy != null) BaseService.IsGrantedCreate =
        //        await AuthorizationService.IsGrantedAsync(CreatePolicy);

        //if (UpdatePolicy != null) BaseService.IsGrantedUpdate =
        //        await AuthorizationService.IsGrantedAsync(UpdatePolicy);

        //if (DeletePolicy != null) BaseService.IsGrantedDelete =
        //        await AuthorizationService.IsGrantedAsync(DeletePolicy);

        //if (PrintPolicy != null) BaseService.IsGrantedPrint =
        //        await AuthorizationService.IsGrantedAsync(PrintPolicy);

        //if (ReservePolicy != null) BaseService.IsGrantedReserve =
        //        await AuthorizationService.IsGrantedAsync(ReservePolicy);

        await GetListDataSourceAsync();
        BaseService.HasChanged = StateHasChanged;
    }
    /// <ÖZET>
    /// Çalıştırdıktan sonra ilk satırı seçmeyi sağlar.
    protected override void OnAfterRender(bool firstRender)
    {
        BaseService.ShowListPage(firstRender);
    }

    /// <ÖZET>
    /// Görev: Listeleme İşlemi Yapar
    /// Çekilen liste verileri listDataSource a gönderilir.
    /// IsActiveCards durumu neyse durum o olarak seçilir.=> Aktif Pasif Kart
    /// Asenkron olduğu için parantez içinde yapıldı, tüm itemler gelince sıradaki işleme geçer
    /// BaseService.IsLoaded = true; ile veriler yüklendi bilgisi alınıyor ve splashscreen kapanır.
    protected virtual async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new TGetListInput
        {
            Durum = BaseService.IsActiveCards
        }))?.Items.ToList();

        BaseService.IsLoaded = true;

        if (listDataSource != null)
            BaseService.ListDataSource = listDataSource;
    }
    /// <ÖZET>
    /// BaseService.ConfirmMessage(L["DeleteConfirmMessage"] ile silme onayı(localize edilerek)
    /// kullanıcı onaylarsa asenkron olarak kodun içine girer.
    /// 
    /// DeleteAsync çalıştırılır. silinecek kartın id si gönderilir => BaseService.SelectedItem.Id
    /// 
    /// deletedEntityIndex ile silinen kartın tablodaki satır indexi alınır.
    /// GetListDataSourceAsync(); ile geriye silindikten sonraki data döndürülür.
    /// BaseService.HasChanged(); ile ekrana yansıtılır.
    /// 
    /// Eğer listede veri varsa silinen kayıtın altındaki kayıtı işaretleme işlemi yapılır.
    /// <returns></returns>
    protected virtual async Task DeleteAsync()
    {
        BaseService.SelectFirstDataRow = false;

        await BaseService.ConfirmMessage(L["DeleteConfirmMessage"], async () =>
         {
             await DeleteAsync(BaseService.SelectedItem.Id);
             var deletedEntityIndex = BaseService.ListDataSource.FindIndex(x =>
                 x.GetEntityId() == BaseService.SelectedItem.GetEntityId());

             await GetListDataSourceAsync();
             BaseService.HasChanged();

             if (BaseService.ListDataSource.Count > 0)
                 BaseService.SelectedItem = BaseService.ListDataSource.
                                            SetSelectedItem(deletedEntityIndex);

         }, L["DeleteConfirmMessageTitle"]);
    }
    /// <ÖZET>
    /// (3/5) 36. Video 6.DK
    /// Insert işlemi yapmadan hemen önce yapılacak işlemler.
    /// DataSource => TGetOutputDto yani ListDto
    /// kod ve durum doldurulur
    /// kod ve durum eğer boş değilse içleri boş olan DataSource.Kod ve DataSource.Durum doldurulur.
    /// 
    /// <returns></returns>
    protected virtual async Task BeforeInsertAsync()
    {
        BaseService.DataSource = new TGetOutputDto();

        var kod = typeof(TGetOutputDto).GetProperty("Kod");
        var durum = typeof(TGetOutputDto).GetProperty("Durum");

        if (kod != null)
            kod.SetValue(BaseService.DataSource, await GetCodeAsync(
                new TGetCodeInput { Durum = BaseService.IsActiveCards }));

        if (durum != null)
            durum.SetValue(BaseService.DataSource, BaseService.IsActiveCards);

        BaseService.ShowEditPage();
    }
    /// <ÖZET>
    /// (3/5) 36. Video 16.DK
    /// ListDataSource.Count eğer 0 a eşitse hiçbir işlem yapmamayı sağlar.
    /// </summary>
    /// <returns></returns>
    protected virtual async Task BeforeUpdateAsync()
    {
        //if (!BaseService.IsGrantedUpdate)
        //{
        //    BaseService.SelectFirstDataRow = false;
        //    return;
        //}

        if (BaseService.ListDataSource.Count == 0) return;

        BaseService.SelectFirstDataRow = false;
        BaseService.DataSource = await GetAsync(BaseService.SelectedItem.Id);
        BaseService.EditPageVisible = true;
        await InvokeAsync(BaseService.HasChanged);
    }
    /// <ÖZET>
    /// Hem Update Hem Create Islemlerinin Yapıldığı Fonksiyon
    /// 
    /// CREATE
    /// BaseService.DataSource.Id == Guid.Empty ise CREATE işlemi yapılıyor demektir.
    /// Yeni bir createInput oluşturulur. SelectEntityDto yu CreateEntityDto ya Mapleme işlemi yapılır.
    /// CreateAsync result'a tanımlanır.
    /// 
    /// UPDATE
    /// BaseService.DataSource.Id == Guid.Empty değil ise UPDATE işlemi yapılıyor demektir.
    /// Yeni bir updateInput oluşturulur. SelectEntityDto yu CreateEntityDto ya Mapleme işlemi yapılır.
    /// UpdateAsync result'a tanımlanır.
    /// 
    /// Hangi rowundaki entity update ediliyorsa onun indexini bulmak için savedEntityIndex yapıldı
    /// HideEditPage ile sayfayı gizler.
    /// Eğer Create işlemiyse Datasource nin id si oluşturulur. update ise zaten vardır.
    /// savedEntityIndex > -1 ise yani update işlemi ise seçili satırı bulup onu işaretleme işlemi yapılır.
    ///     else içine girerse create işlemidir ve id ye göre geriye entity döndürür.
    /// </summary>
    /// <returns></returns>
    protected virtual async Task OnSubmit()
    {
        TGetOutputDto result;

        if (BaseService.DataSource.Id == Guid.Empty)
        {
            var createInput = ObjectMapper.Map<TGetOutputDto, TCreateInput>(
                BaseService.DataSource);

            result = await CreateAsync(createInput);
        }
        else
        {
            var updateInput = ObjectMapper.Map<TGetOutputDto, TUpdateInput>(
                BaseService.DataSource);

            result = await UpdateAsync(BaseService.DataSource.Id, updateInput);
        }

        if (result == null) return;

        var savedEntityIndex = BaseService.ListDataSource.FindIndex(
            x => x.Id == BaseService.DataSource.Id);

        await GetListDataSourceAsync();
        BaseService.HideEditPage();

        if (BaseService.DataSource.Id == Guid.Empty)
            BaseService.DataSource.Id = result.Id;

        if (savedEntityIndex > -1)
            BaseService.SelectedItem = BaseService.ListDataSource.
                SetSelectedItem(savedEntityIndex);
        else
            BaseService.SelectedItem = BaseService.ListDataSource.
                GetEntityById(BaseService.DataSource.Id);
    }

    //protected virtual async Task PrintAsync()
    //{
    //    if (BaseService.ListDataSource.Count == 0) return;

    //    BaseService.SelectFirstDataRow = false;
    //    BaseService.DataSource = await GetAsync(BaseService.SelectedItem.Id);

    //    BaseService.ShowReportSelectBox = true;
    //    await InvokeAsync(BaseService.HasChanged);
    //}
}