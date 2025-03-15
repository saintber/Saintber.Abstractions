namespace Saintber.Abstractions.Identity
{
    /// <summary>
    /// 提供取得目前登入使用者的角色識別碼清單的功能。
    /// </summary>
    public interface IRoleIdsProvider
    {
        /// <summary>
        /// 取得目前登入使用者的角色識別碼清單。
        /// </summary>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>目前登入使用者的角色識別碼清單。</returns>
        Task<IEnumerable<string>> GetRoleIdsAsync(CancellationToken cancellationToken = default);
    }
}
