namespace Saintber.Abstractions.Identity
{
    /// <summary>
    /// 提供取得目前使用者名稱的功能。
    /// </summary>
    public interface IUserNameProvName : IUserNameProviderName<string>
    {
    }

    /// <summary>
    /// 提供取得目前使用者名稱的功能。
    /// </summary>
    /// <typeparam name="TKey">使用者名稱型別。</typeparam>
    public interface IUserNameProviderName<TKey>
    {
        /// <summary>
        /// 取得目前使用者的名稱。
        /// </summary>
        /// <returns>目前使用者的名稱。</returns>
        Task<TKey> GetUserNameAsync();
    }
}
