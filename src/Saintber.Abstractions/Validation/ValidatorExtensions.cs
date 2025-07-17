using FluentValidation;

namespace Saintber.Abstractions.Validation
{
    /// <summary>
    /// 提供驗證器的擴充方法，簡化驗證流程並統一錯誤處理方式。
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// 非同步驗證指定的物件，若驗證失敗則執行錯誤處理，然後拋出例外。
        /// </summary>
        /// <typeparam name="T">要驗證的物件型別。</typeparam>
        /// <param name="validator">驗證器實例。</param>
        /// <param name="instance">要進行驗證的物件。</param>
        /// <param name="cancellationToken">（可選）取消標記，用於取消非同步操作。</param>
        /// <returns>表示驗證過程的非同步作業。</returns>
        /// <exception cref="ValidationException">
        /// 當驗證失敗時，執行 <see cref="ValidationHandler{T}.OnFailuresHandler"/>，然後拋出包含所有錯誤訊息的 <see cref="ValidationException"/>。
        /// </exception>
        public static async Task ValidateAndThrowAsync<T>(this ValidationHandler<T> validator
            , T instance, CancellationToken cancellationToken = default)
        {
            var result = await validator.ValidateAsync(instance, cancellationToken);
            if (!result.IsValid)
            {
                throw await validator.OnFailuresHandler(result) ?? new ValidationException(result.Errors);
            }
        }
    }
}
