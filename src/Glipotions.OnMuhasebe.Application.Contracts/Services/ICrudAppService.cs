using System;
using Volo.Abp.Application.Services;

namespace Glipotions.OnMuhasebe.Services;

/// <Özet>
/// Tanım C.R.U.D. işlemlerini yapmamızı sağlayan interface
///
/// <typeparam name="TGetOutputDto"></typeparam>        - Geriye 1 tane Entity Döndürür..
///                                                     ..SelectDTO ile yaptığımız tüm işlemleri bu karşılar.
/// <typeparam name="TGetListOutputDto"></typeparam>    - Geriye List Döndürür. ListDTO ları karşılayan parametre
/// <typeparam name="TGetListInput"></typeparam>        - Veri çekerken hangi parametrelere göre verilerin geleceğini burda belirtiriz.
///                                                     .. ListParametre Classlarımızı bu karşılar. veri girişi olduğu için başında in var
/// <typeparam name="TCreateInput"></typeparam>         - CreateDTO işlemlerini karşılar. veri girişi olduğu için başına in koyuldu
/// <typeparam name="TUpdateInput"></typeparam>         - UpdateDTO ile yaptığımız işlemleri karşılayacak class.
/// 
/// Üst Kısımda Tanımladık ancak bunların Kalıtımını yapmadık henüz, Aşağıdaki kodlar aracılığıyla yapıyoruz.
/// <t name="IReadOnlyAppService"></t>                  - GetAsync ve GetListAsync'i çalıştırır.
/// ..GetAsync olarak kullandığımızda TGetOutputDto olarak geri döndürür.
/// ..GetListAsync olarak kullandığımızda TGetListOutputDto olarak geri döndürür.
/// 
/// <t name="ICreateAppService"></t>                    - CreateAsync'i çalıştırır.
/// ..TCreateInput olarak gönderdiğimiz veriyi alır Create eder ve TGetOutputDto olarak geri döndürür.
/// 
/// <t name="IUpdateAppService"></t>                    - UpdateAsync'i çalıştırır
/// ..TUpdateInput olarak gönderdiğimiz veriyi alır Update eder ve TGetOutputDto olarak geri döndürür.
public interface ICrudAppService<TGetOutputDto, TGetListOutputDto, in TGetListInput,
    in TCreateInput, in TUpdateInput> :
    IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, Guid, TGetListInput>,
    ICreateAppService<TGetOutputDto, TCreateInput>,
    IUpdateAppService<TGetOutputDto, Guid, TUpdateInput>
{
}

/// <Özet>
/// Tanım C.R.U.D. işlemlerini yapmamızı sağlayan interface
///
/// <typeparam name="TGetOutputDto"></typeparam>        - Geriye 1 tane Entity Döndürür..
///                                                     ..SelectDTO ile yaptığımız tüm işlemleri bu karşılar.
/// <typeparam name="TGetListOutputDto"></typeparam>    - Geriye List Döndürür. ListDTO ları karşılayan parametre
/// <typeparam name="TGetListInput"></typeparam>        - Veri çekerken hangi parametrelere göre verilerin geleceğini burda belirtiriz.
///                                                     .. ListParametre Classlarımızı bu karşılar. veri girişi olduğu için başında in var
/// <typeparam name="TCreateInput"></typeparam>         - CreateDTO işlemlerini karşılar. veri girişi olduğu için başına in koyuldu
/// <typeparam name="TUpdateInput"></typeparam>         - UpdateDTO ile yaptığımız işlemleri karşılayacak class.
/// <typeparam name="TGetCodeInput"></typeparam>        - Code olan Entityler için yapacağımız işlemi karşılayacak class.
/// 
/// Üst Kısımda Tanımladık ancak bunların Kalıtımını yapmadık henüz, Aşağıdaki kodlar aracılığıyla yapıyoruz.
/// <t name="ICrudAppService"></t>                      - Yukarıda yaptığımız ICrudAppService i Çalıştırır.
///
/// <t name="IDeleteAppService"></t>                    - DeleteAsync'i çalıştırır.
/// 
/// <t name="ICodeAppService"></t>                      - Kendi oluşturduğumuz GetCodeAsync yi çalıştırır.
/// ..TGetCodeInput u alır ve geriye yeni bir kod oluşturur.
public interface ICrudAppService<TGetOutputDto, TGetListOutputDto, in TGetListInput,
    in TCreateInput, in TUpdateInput, in TGetCodeInput> :
    ICrudAppService<TGetOutputDto, TGetListOutputDto, TGetListInput,
    TCreateInput, TUpdateInput>,
    IDeleteAppService<Guid>,
    ICodeAppService<TGetCodeInput>
{
}