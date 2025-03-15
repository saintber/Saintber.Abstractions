namespace Saintber.Abstractions.Identity
{
    /// <summary>
    /// 提供取得目前使用者名稱的功能。
    /// </summary>
    public interface IUserNameProvider : IUserNameProvider<string>
    {
    }

    /// <summary>
    /// 提供取得目前使用者名稱的功能。
    /// </summary>
    /// <typeparam name="TKey">使用者名稱型別。</typeparam>
    public interface IUserNameProvider<TKey>
    {
        /// <summary>
        /// 取得目前使用者的名稱。
        /// </summary>
        /// <param name="cancellationToken">取消權杖。</param>
        /// <returns>目前使用者的名稱。</returns>
        Task<TKey> GetUserNameAsync(CancellationToken cancellationToken = default);
    }
}
