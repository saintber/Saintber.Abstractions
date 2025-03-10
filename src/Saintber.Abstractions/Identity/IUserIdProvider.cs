namespace Saintber.Abstractions.Identity
{
    /// <summary>
    /// 提供取得目前使用者識別碼的功能。
    /// </summary>
    public interface IUserIdProvider : IUserIdProvider<string>
    {
    }

    /// <summary>
    /// 提供取得目前使用者識別碼的功能。
    /// </summary>
    /// <typeparam name="TKey">使用者識別碼型別。</typeparam>
    public interface IUserIdProvider<TKey>
    {
        /// <summary>
        /// 取得目前使用者的識別碼。
        /// </summary>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>目前使用者的識別碼。</returns>
        Task<TKey> GetUserIdAsync(CancellationToken cancellationToken = default);
    }
}
