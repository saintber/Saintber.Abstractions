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
        /// <param name="request">權限查詢的篩選條件。</param>
        /// <param name="cancellationToken">取消作業的通知權杖。</param>
        /// <returns>由多組權限代碼組成的唯讀集合。</returns>
        Task<IReadOnlyList<IReadOnlyList<string>>> GetPermissionCodesAsync(GetPermissionCodesRequest request, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// 定義擷取權限代碼清單時所需的查詢參數。
    /// </summary>
    public class GetPermissionCodesRequest
    {
        /// <summary>
        /// 欲查詢的系統代碼。若為 null，表示查詢所有系統的權限。
        /// </summary>
        public string? SystemCode { get; set; }
    }

    public static class PermissionProviderExtensions
    {
        /// <summary>
        /// 擷取目前登入使用者的權限代碼清單，使用預設查詢條件。
        /// </summary>
        /// <param name="permissionProvider">權限提供者。</param>
        /// <param name="cancellationToken">取消作業的通知權杖。</param>
        /// <returns>由多組權限代碼組成的唯讀集合。</returns>
        public static Task<IReadOnlyList<IReadOnlyList<string>>> GetPermissionCodesAsync(
            this IPermissionProvider permissionProvider, CancellationToken cancellationToken = default)
            => permissionProvider.GetPermissionCodesAsync(new GetPermissionCodesRequest { }, cancellationToken);
    }
}
