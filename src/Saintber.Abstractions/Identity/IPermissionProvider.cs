namespace Saintber.Abstractions.Identity
{
    /// <summary>
    /// 提供目前登入使用者的權限代碼清單存取功能。
    /// </summary>
    public interface IPermissionProvider
    {
        /// <summary>
        /// 擷取目前登入使用者的權限代碼清單。
        /// </summary>
        /// <param name="cancellationToken">取消作業的通知權杖。</param>
        /// <returns>由多組權限代碼組成的唯讀集合。</returns>
        Task<IReadOnlyList<IReadOnlyList<string>>> GetPermissionCodesAsync(CancellationToken cancellationToken = default);
    }
}
