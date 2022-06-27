using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glipotions.Blazor.Core.Models;
using Glipotions.Blazor.Core.Services;
using DevExpress.XtraReports.UI;
using Microsoft.Extensions.Localization;

namespace Glipotions.Blazor.Core.Helpers;

public class Functions
{
    /// <ÖZET>
    /// 
    /// Combobox doldurma fonksiyonudur***
    /// TEnum tipinde veri alır, OfType ile TEnum tipine çevirir, liste olarak veriyi alır.
    /// 
    /// Value= hangi enum değeri geliyorsa onu alır
    /// DisplayName= localizerden gelen Enum adını alır.
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="localizer"></param>
    /// <returns></returns>
    public static List<ComboBoxEnumItem<TEnum>> FillEnumToComboBox<TEnum>(
        IStringLocalizer localizer) where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .OfType<TEnum>()
            .Select(t => new ComboBoxEnumItem<TEnum>
            {
                Value = t,
                DisplayName = localizer[$"Enum:{ typeof(TEnum).Name }:{ t.To<byte>() }"]
            }).ToList();
    }
    /// <ÖZET>
    /// Satır Yüksekliğini Ayarlayan Fonksiyon
    public static string[] RowHeights(params string[] rowHeights)
    {
        return rowHeights;
    }
    /// <ÖZET>
    /// Kolon Genişliğini Ayarlayan Fonksiyon
    public static string[] ColumnWidths(params string[] columnWidths)
    {
        return columnWidths;
    }

    public static string CreateId()//19 haneli kod üretiliyor.
    {
        string AddZero(string value, bool threeDigits = false)
        {
            if (threeDigits)
                return value.Length switch
                {
                    1 => "00" + value,
                    2 => "0" + value,
                    _ => value,
                };

            return value.Length switch
            {
                1 => "0" + value,
                _ => value,
            };
        }

        string Id()//2022010112122000335
        {
            var year = DateTime.Now.Date.Year.ToString();//2022
            var month = AddZero(DateTime.Now.Date.Month.ToString());//01
            var day = AddZero(DateTime.Now.Date.Day.ToString());//01
            var hour = AddZero(DateTime.Now.Hour.ToString());//12
            var minute = AddZero(DateTime.Now.Minute.ToString());//12
            var second = AddZero(DateTime.Now.Second.ToString());//20
            var millisecond = AddZero(DateTime.Now.Millisecond.ToString(), true);//003
            var random = AddZero(new Random().Next(0, 99).ToString());//35

            return year + month + day + hour + minute + second + millisecond + random;
        }

        return Id();
    }

    //public static XtraReport GetReport(Assembly assembly, ICoreListPageService service)
    //{
    //    var assemblyName = assembly.FullName.Split(',')[0];
    //    var reportFolder = service.ReportFolder == null ? service.BaseReportFolder :
    //        $"{service.BaseReportFolder}.{service.ReportFolder.Replace('\\', '.')}";

    //    return (XtraReport)assembly.CreateInstance($"{assemblyName}.{reportFolder}." +
    //        $"{service.SelectedReportName}".Replace(' ', '_'));
    //}
}