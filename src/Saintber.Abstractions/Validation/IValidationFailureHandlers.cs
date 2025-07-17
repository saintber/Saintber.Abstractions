using FluentValidation.Results;

namespace Saintber.Abstractions.Validation
{
    /// <summary>
    /// 驗證失敗處理器介面，定義處理各類驗證失敗情境的非同步方法。
    /// </summary>
    public interface IValidationFailureHandlers
        : IMissingFieldHandler
        , IDuplicateDataHandler
        , IDataConcurrencyHandler
    {
    }

    /// <summary>
    /// 欄位遺漏處理器介面。
    /// </summary>
    public interface IMissingFieldHandler
    {
        /// <summary>
        /// 當欄位遺漏時執行的非同步處理行為，例如必要欄位未填寫。
        /// </summary>
        /// <returns>表示非同步處理程序的工作，通常會拋出例外或執行其他自定義行為。</returns>
        Func<ValidationResult, Task<Exception>> OnMissingFieldsAsync { get; }
    }

    /// <summary>
    /// 資料重複處理器介面。
    /// </summary>
    public interface IDuplicateDataHandler
    {
        /// <summary>
        /// 當資料重複時執行的非同步處理行為，例如主鍵或唯一欄位重複。
        /// </summary>
        /// <returns>表示非同步處理程序的工作，通常會拋出例外或執行其他自定義行為。</returns>
        Func<ValidationResult, Task<Exception>> OnDuplicateDataAsync { get; }
    }

    /// <summary>
    /// 資料併發衝突處理器介面。
    /// </summary>
    public interface IDataConcurrencyHandler
    {
        /// <summary>
        /// 當資料併發衝突時執行的非同步處理行為，例如資料在存取期間已被其他使用者修改。
        /// </summary>
        /// <returns>表示非同步處理程序的工作，通常會拋出例外或執行其他自定義行為。</returns>
        Func<ValidationResult, Task<Exception>> OnDataConcurrencyConflictAsync { get; }
    }
}
