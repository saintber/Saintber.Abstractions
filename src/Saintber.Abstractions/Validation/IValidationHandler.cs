using FluentValidation;
using FluentValidation.Results;

namespace Saintber.Abstractions.Validation
{
    /// <summary>
    /// 定義具有驗證失敗處理機制的驗證器基底類別。
    /// </summary>
    /// <typeparam name="T">要驗證的物件型別。</typeparam>
    public abstract class ValidationHandler<T> : AbstractValidator<T>
    {
        /// <summary>
        /// 取得或設定驗證失敗時的處理程序。
        /// 當驗證失敗時，該方法應回傳應拋出的例外，若未回傳則會預設拋出 <see cref="ValidationException"/>。
        /// </summary>
        public virtual Func<ValidationResult, Task<Exception?>> OnFailuresHandler { get; set; }
            = (result) => Task.FromResult((Exception?)new ValidationException(result.Errors));
    }
}
